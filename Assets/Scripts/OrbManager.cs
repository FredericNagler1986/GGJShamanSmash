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

public class OrbManager : SingletonMonoBehaviour<OrbManager>
{
    public Content Content;

    private Dictionary<Transform, OrbType> Orbs;

    private float spawnTimer;

    public GameObject FirePrefab;
    public GameObject ShieldPrefab;
    public GameObject MeleePrefab;

    public Transform OrbParent;
    
    // Use this for initialization
    void Start ()
    {
        Orbs = new Dictionary<Transform, OrbType>();
	    foreach(Transform child in transform)
        {
            Orbs.Add(child, OrbType.None);
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
            if(orb.Value == OrbType.None)
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
        Orbs[freeTransForm] = newType;
    }
}
