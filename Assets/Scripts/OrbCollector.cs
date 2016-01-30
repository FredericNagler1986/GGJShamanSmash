using UnityEngine;
using System.Collections.Generic;

public class OrbCollector : MonoBehaviour
{
    private List<OrbType> CollectedOrbs;

    private void Start()
    {
        CollectedOrbs = new List<OrbType>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            CollectOrb(OrbType.Fire, other.gameObject);
        }
        else if (other.tag == "Shield")
        {
            CollectOrb(OrbType.Shield, other.gameObject);
        }
        else if(other.tag == "Melee")
        {
            CollectOrb(OrbType.Melee,other.gameObject);
        }
    }

    private void CollectOrb(OrbType type, GameObject go)
    {
        if(CollectedOrbs.Count < 3)
        {
            CollectedOrbs.Add(type);
        }

        OrbManager.Instance.CollectOrb(go);
        Destroy(go);
    }

    public List<OrbType> GetCollectedOrbs()
    {
        return CollectedOrbs;
    }

    public void ClearCollectedOrbs()
    {
        CollectedOrbs.Clear();
    }
}