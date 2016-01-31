using UnityEngine;
using System.Collections;

public class CheckOut : MonoBehaviour {

    public Vector2 Area;

    private float radius;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        radius = GetComponentInChildren<CircleCollider2D>().radius;
        rigid = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if (rigid == null)
            return;

        var velocity = rigid.velocity;

        if(velocity.x > 0 && (transform.position.x + radius) > Area.x )
        {
            var temp = transform.position;
            temp.x = radius - Area.x;
            transform.position = temp;
        }
        else if (velocity.x < 0 && (transform.position.x - radius) < -Area.x)
        {
            var temp = transform.position;
            temp.x = Area.x - radius;
            transform.position = temp;
        }
        else if ((transform.position.y - radius) < - Area.y)
        {
            var temp = transform.position;
            temp.y = Area.y - radius;
            transform.position = temp;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var p1 = new Vector3(Area.x, Area.y, 0);
        var p2 = new Vector3(-Area.x, Area.y, 0);
        var p3 = new Vector3(-Area.x, -Area.y, 0);
        var p4 = new Vector3(Area.x, -Area.y, 0);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);    }
}
