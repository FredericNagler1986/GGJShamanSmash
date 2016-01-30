using UnityEngine;
using System.Collections;

public class OrbCollector : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire" || other.tag == "Shield" || other.tag == "Melee")
        {
            OrbManager.Instance.CollectOrb(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}