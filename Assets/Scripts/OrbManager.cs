using UnityEngine;
using System.Collections.Generic;
using System;

public enum OrbType
{
    None,
    Fire,
    Shield,
    Melee
}

class Orb
{
    public OrbType Type;
    public GameObject Instance;

    public Orb()
    {
        Type = OrbType.None;
    }
}

public class OrbManager : SingletonMonoBehaviour<OrbManager>
{
    public Content Content;

    private Dictionary<Transform, Orb> Orbs;

    private float spawnTimer;

    public GameObject FirePrefab;
    public GameObject ShieldPrefab;
    public GameObject MeleePrefab;

    public Transform OrbParent;
    
    // Use this for initialization
    void Start ()
    {
        Orbs = new Dictionary<Transform, Orb>();
	    foreach(Transform child in transform)
        {
            Orbs.Add(child, new Orb());
        }

        spawnTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            if(OrbParent.childCount < Content.MaxOrbs)
            {
                SpawnOrb();
            }
            spawnTimer = Content.SpawnTimeOrbs;
        }
	}

    void SpawnOrb()
    {
        var listOfFreeTransforms = new List<Transform>();
        foreach(var orb in Orbs)
        {
            if(orb.Value.Type == OrbType.None)
            {
                listOfFreeTransforms.Add(orb.Key);
            }
        }

        if (listOfFreeTransforms.Count == 0)
            return;

        var freeTransForm = listOfFreeTransforms[UnityEngine.Random.Range(0, listOfFreeTransforms.Count)];
        var newType = (OrbType)UnityEngine.Random.Range(1, Enum.GetNames(typeof(OrbType)).Length);

        GameObject orbInstance = null;
        switch(newType)
        {
            case OrbType.Fire:
                orbInstance = Instantiate(FirePrefab);
                break;
            case OrbType.Melee:
                orbInstance = Instantiate(MeleePrefab);
                break;
            case OrbType.Shield:
                orbInstance = Instantiate(ShieldPrefab);
                break;
        }
        orbInstance.transform.SetParent(OrbParent);
        orbInstance.transform.position = freeTransForm.position;
        Orbs[freeTransForm].Type = newType;
        Orbs[freeTransForm].Instance = orbInstance;

        SoundManager.Instance.PlaySpawnOrbSound();
    }

    public void CollectOrb(GameObject orbInstance)
    {
        foreach(var orb in Orbs)
        {
            if (orb.Value == null)
                continue;
            
            if(orb.Value.Instance != orbInstance)
                continue;

            SoundManager.Instance.PlayCollectOrbSound();

            orb.Value.Type = OrbType.None;
            orb.Value.Instance = null;
        }
    }
}
