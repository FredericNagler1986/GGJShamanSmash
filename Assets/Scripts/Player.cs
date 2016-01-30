using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public string inputPrefix;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		var x = Input.GetAxis ( inputPrefix + "Horizontal" );
		var y = Input.GetAxis ( inputPrefix + "Vertical" );

		var btnX = Input.GetButton ( inputPrefix + "X" );
		var btnY = Input.GetButton ( inputPrefix + "Y" );
		var btnA = Input.GetButton ( inputPrefix + "A" );
		var btnB = Input.GetButton ( inputPrefix + "B" );

		transform.position += new Vector3 ( x, y, 0 );

		Debug.Log ( "X: " + btnX + "; Y: " + btnY + "; A: " + btnA + "; B: " + btnB );
	}
}
