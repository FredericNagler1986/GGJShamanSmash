using UnityEngine;
using System.Collections;

public class CheckOut : MonoBehaviour
{
	public Vector2 Area;
	private float radius;
	private Rigidbody2D rigid;

	public GameObject[] DisableOnMove;

	void Start ()
	{
		radius = GetComponentInChildren<CircleCollider2D> ().radius;
		rigid = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
        if (rigid == null)
            return;

		var velocity = rigid.velocity;
		var temp = transform.position;

		if ( velocity.x > 0 && (temp.x + radius) > Area.x )
		{
			SetObjectState ( false );
			temp.x = radius - Area.x;
			transform.position = temp;
			SetObjectState ( true );
		}
		else if ( velocity.x < 0 && (temp.x - radius) < -Area.x )
		{
			SetObjectState ( false );
			temp.x = Area.x - radius;
			transform.position = temp;
			SetObjectState ( true );
		}
		else if ( (temp.y - radius) < -Area.y )
		{
			SetObjectState ( false );
			temp.y = Area.y - radius;
			transform.position = temp;
			SetObjectState ( true );
		}
	}

	public void SetObjectState (bool state)
	{
		if ( DisableOnMove != null )
		{
			foreach ( var item in DisableOnMove )
			{
				if ( item != null )
					item.SetActive ( state );
			}
		}
	}

	public void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;

		var p1 = new Vector3 ( Area.x, Area.y, 0 );
		var p2 = new Vector3 ( -Area.x, Area.y, 0 );
		var p3 = new Vector3 ( -Area.x, -Area.y, 0 );
		var p4 = new Vector3 ( Area.x, -Area.y, 0 );

		Gizmos.DrawLine ( p1, p2 );
		Gizmos.DrawLine ( p2, p3 );
		Gizmos.DrawLine ( p3, p4 );
		Gizmos.DrawLine ( p4, p1 );
	}
}
