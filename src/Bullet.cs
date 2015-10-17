using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	public Transform target;
	public GameObject bulletExplosion, enemyExplosion;
	TurretAttack turretAttack;
	
	private GameObject enemy;
	EnemyController enemyController;
	MoneyManager moneyManager;

	public float attackDamage;
	private bool speedSet;
	private float freezeTime = 0.75f, slowTime = 1.0f;
	private float slowSpeed;

	void Awake(){
		moneyManager = GameObject.FindGameObjectWithTag ("MoneyManager").GetComponent<MoneyManager> ();
		speedSet = false;
	}

	void FixedUpdate(){
		if (target) { // Fly towards the target        
			Vector3 dir = target.position - transform.position;
			GetComponent<Rigidbody>().velocity = dir.normalized * speed;
		} else { // Otherwise destroy self
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		enemy = other.gameObject;

		if (other.tag == "enemySphere" || other.tag == "enemyCube" || other.tag == "enemyBossSphere") {
			enemyController = enemy.GetComponent<EnemyController> ();
			if(gameObject.tag == "slowBullet" || gameObject.tag == "slowBullet1" || gameObject.tag == "slowBullet2" || gameObject.tag == "slowBullet3"){
				GetComponent<MeshRenderer>().enabled = false;//disable meshRenderer
				GetComponent<SphereCollider>().enabled = false;
				if (gameObject.tag == "slowBullet" || gameObject.tag == "slowBullet1" || gameObject.tag == "slowBullet2"){ //slowBullets
					if (gameObject.tag == "slowBullet") { //slowBullet
						slowSpeed = 1.0f;
					}
					if (gameObject.tag == "slowBullet1") { //slowBullet1
						slowSpeed = 1.5f;
					}
					if (gameObject.tag == "slowBullet2") { //slowBullet2
						slowSpeed = 3.0f;
					}

					if(!speedSet){
						StartCoroutine(slowEnemy());
						StopCoroutine(slowEnemy());
					}

				}
				else if (gameObject.tag == "slowBullet3") { //slowBullet

					enemyController.enemyHealth -= attackDamage;
					//Debug.Log ("slowBullet3");
					if(enemyController.nav.speed > 0){
						Debug.Log ("Frozen");
						StartCoroutine(freezeEnemy()); //freeze for a brief time
						StopCoroutine(freezeEnemy());
					}
				}
			}
			else { //destroy bullet and subtract health
				enemyController.enemyHealth -= attackDamage;
				Destroy(gameObject);
			}
	

			//Debug.Log ("enemyHealth: " + enemyController.enemyHealth);
			//Debug.Log ("attackDamage: " + attackDamage);

			if (enemyController.enemyHealth <= 0) { //destroy enemy
				//Debug.Log ("Enemy dead");
				Destroy(other.gameObject);
				Instantiate(enemyExplosion, transform.position, transform.rotation);
				moneyManager.addMoney(enemyController.moneyValue);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator freezeEnemy(){
		speed = enemyController.nav.speed;
		enemyController.nav.speed = 0;
		Debug.Log ("speed: " + speed);
		yield return new WaitForSeconds (freezeTime);
		enemyController.nav.speed = speed; //return back to normal speed
		//Debug.Log ("Returned to normal speed");
		//speedSet = true;
		Destroy (gameObject);
	}

	IEnumerator slowEnemy(){
		speed = enemyController.nav.speed;
		enemyController.nav.speed -= slowSpeed; //for brief time
		if (enemyController.nav.speed < 1.0) {
			enemyController.nav.speed = 1.0f;
		}
		speedSet = true;
		yield return new WaitForSeconds (slowTime);
		enemyController.nav.speed = speed; //return back to normal speed
		//Debug.Log ("Returned to normal speed");
		//speedSet = true;
		Destroy (gameObject);
	}
}
