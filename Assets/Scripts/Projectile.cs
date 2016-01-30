using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float Direction;

    public Content Content;
    
	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().flipX = Direction < 0;
     
        float normalizedDirection = Direction >= 0 ? 1 : -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(normalizedDirection * Content.ProjectileSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        GetComponent<Animator>().SetTrigger("explode");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
