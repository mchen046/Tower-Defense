using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed;
	
	void Update () {
		transform.Rotate (new Vector3 (90, 90, 90) * Time.deltaTime * speed);
	}
	
}
