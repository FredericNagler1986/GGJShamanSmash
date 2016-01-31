using UnityEngine;
using System.Collections;

public class ParticleSystemCleanup : MonoBehaviour
{
	public ParticleSystem ps;


	void Start ()
	{
		if ( ps == null )
			ps = GetComponent<ParticleSystem> ();
	}

	void LateUpdate ()
	{
		if ( ps != null && !ps.IsAlive () )
		{
			Destroy ( gameObject );
		}
	}
}
