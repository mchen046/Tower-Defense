using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject HQ;
	HQHealth hqHealth;

	public float enemyHealth;
	public int moneyValue;

	public NavMeshAgent nav;
	
	void Awake(){
		HQ = GameObject.FindGameObjectWithTag ("HQ");
		nav = GetComponent<NavMeshAgent> ();
		hqHealth = HQ.GetComponent<HQHealth> ();
	}

	void Start(){

	}

	void Update(){
		if (HQ != null) {
			nav.SetDestination (HQ.transform.position);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "HQ") { //HQ hit!
			Destroy (gameObject);
			hqHealth.TakeDamage ();
		}
	}


}
