using UnityEngine;
using System.Collections;

public class TurretAttack : MonoBehaviour {

	public GameObject enemyExplosion, bullet; 
	private GameObject enemy;
	EnemyController enemyController;

	private bool enemyInRange;

	public float fireRate;
	private float nextFire;
	
	public float rotationSpeed = 35;

	void Update(){
		//transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World); //rotate
		//Debug.Log ("Time.time: " + Time.time);
		//Debug.Log ("nextFire: " + nextFire);
		//Debug.Log (gameObject.tag + "'s enemyInRange: " + enemyInRange);

		if (Time.time > nextFire && enemyInRange && enemy!=null) {
			resetFireRate();
			Attack ();
			//Debug.Log ("Attack");
		}
	}

	public void resetFireRate(){
		nextFire = Time.time + fireRate;
	}

	void Awake(){
		//enemyInRange = false;
	}	

	void Attack(){

		//Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		//play weapon sound
		//GetComponent<AudioSource>().Play();

		GameObject g = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
		g.GetComponent<Bullet> ().target = enemy.transform;
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "enemySphere" || other.tag == "enemyCube" || other.tag == "enemyBossSphere"){
			//Debug.Log("OnTriggerEnter!");
			//Debug.Log (gameObject.tag + "'s other.tag: " + other.tag);
			enemyInRange = true;
			enemy = other.gameObject;
		}
	}
	
	/*void OnTriggerExit (Collider other)
	{
		if(other.tag == "enemySphere" || other.tag == "enemyCube" || other.tag == "enemyBossSphere"){
			enemyInRange = false;
			//Destroy(other.gameObject);
		}
	}*/
}
