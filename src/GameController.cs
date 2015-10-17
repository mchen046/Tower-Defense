using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemySphere, enemyCube, enemyBossSphere;
	//public GameObject enemyRotatingCube;

	public int moneyValue;
	private bool startGame;
	public bool basicPressed, slowPressed, opPressed;

	void Start(){
		startGame = false;
		GameObject.FindGameObjectWithTag ("minusMoney").GetComponent<Text> ().enabled = false;
		GameObject.FindGameObjectWithTag ("addMoney").GetComponent<Text> ().enabled = false;
		GameObject.FindGameObjectWithTag ("noMinerals").GetComponent<Text> ().enabled = false;
	}

	void Update(){
		if (!startGame) {
			StartCoroutine (spawnWaves ());
			startGame = true;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>().addMoney(500);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>().setMoney (0);
		}
		GameObject.FindGameObjectWithTag ("Timer").GetComponent<Text> ().text = Time.realtimeSinceStartup.ToString();
	}

	IEnumerator spawnWaves(){
		//3 levels for now
		Vector3 spawnPos = new Vector3 (-20.5f, 0, -7.2f);
		Quaternion spawnRotation = Quaternion.identity;
		int count = 1, waveCount = 1;
		while (GameObject.FindGameObjectWithTag("HQ").GetComponent<HQHealth>().currentHP != 0) { //hq not dead
			for (int i = 0; i < 4; i++) {
				GameObject.FindGameObjectWithTag ("waveText").GetComponent<Text> ().text = ": Wave " + waveCount.ToString();
				for (int j = 0; j < 10 && i < 3; j++) {
					enemySphere.GetComponent<EnemyController>().enemyHealth*=count;
					enemyCube.GetComponent<EnemyController>().enemyHealth*=count;
					enemyBossSphere.GetComponent<EnemyController>().enemyHealth*=count;
					if (i == 0) {
						Instantiate (enemySphere, spawnPos, spawnRotation);
						Debug.Log (enemySphere.GetComponent<EnemyController>().enemyHealth.ToString());
					} else if (i == 1) {
						Instantiate (enemyCube, spawnPos, spawnRotation);
					} else if (i == 2) {
						Instantiate (enemySphere, spawnPos, spawnRotation);
						yield return new WaitForSeconds (1.0f);
						Instantiate (enemyCube, spawnPos, spawnRotation);
					}
					yield return new WaitForSeconds (2.0f); //wait
				}
				if (i == 3) {
					for(int b = 0; b<count; b++){
						Instantiate (enemyBossSphere, spawnPos, spawnRotation);
						yield return new WaitForSeconds (7.0f); //wait
					}
				}
				yield return new WaitForSeconds (5.0f); //wait
				waveCount++;
			}
			count++;
		}
	}
}
