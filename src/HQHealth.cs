using UnityEngine;
using System.Collections;

public class HQHealth : MonoBehaviour {

	public int startingHP = 100;
	public int currentHP;
	public GameObject enemyExplosion, HQExplosion;

	void Awake(){
		currentHP = startingHP;
	}

	public void TakeDamage(){
		currentHP -= 10;
		Instantiate(enemyExplosion, transform.position, transform.rotation);
		if(currentHP==0){ //destory HQ here
			Instantiate(HQExplosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		//Debug.Log (currentHP.ToString ());
	}
}
