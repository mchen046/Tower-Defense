using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileScript : MonoBehaviour 
{
	public GameObject defaultTurret, basicTurret, slowTurret, opTurret, basicTurret1, basicTurret2, basicTurret3, slowTurret1, slowTurret2, slowTurret3, opTurret1, opTurret2, opTurret3;
	private GameObject newTurret;
	public bool basic, slow, op, hasTurret;
	public GameObject[] tiles;
	MoneyManager moneyManager;
	public Material yellowMaterial, greenMaterial, redMaterial, iceBlueMaterial;
	public int upgrade;

	TurretAttack turretAttack;
	
	void Start(){
		basic = slow = op = hasTurret = false;
		tiles = GameObject.FindGameObjectsWithTag ("TileOpen");
		upgrade = 0;
	}

	void Awake(){
		moneyManager = GameObject.FindGameObjectWithTag ("MoneyManager").GetComponent<MoneyManager> ();
	}

	void OnMouseUpAsButton() {
		if ((basic || slow || op) && !hasTurret) {
			newTurret = (GameObject)Instantiate (defaultTurret);
			Destroy (newTurret);
			if (basic && moneyManager.getMoney() >= 50) {
				newTurret = (GameObject)Instantiate (basicTurret); 
				hasTurret = true;
				moneyManager.addMoney (-50);
				//subtract money animation
			} else if (slow && moneyManager.getMoney() >= 35) {
				newTurret = (GameObject)Instantiate (slowTurret); 
				hasTurret = true;
				moneyManager.addMoney (-35);
			} else if (op && moneyManager.getMoney() >= 100) {
				newTurret = (GameObject)Instantiate (opTurret); 
				hasTurret = true;
				moneyManager.addMoney (-100);
			}
			else{
				StartCoroutine(noMinerals());
				StopCoroutine(noMinerals());
			}
			newTurret.transform.position = transform.position + new Vector3 (0, 0.25f, 0); //spawn new turret
		} 
		else if(hasTurret && upgrade <= 2){ //already hasTurret, upgrade! / has not reached max upgrade
			bool upgraded = false, noMoney = false;
			GameObject oldTurret = newTurret;
			if (basic && GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().basicPressed) {
				if(moneyManager.getMoney() >= 50){
					//instantiate new tower at current pos
					if(upgrade==0){ //first upgrade
						newTurret = (GameObject)Instantiate (basicTurret1);
						moneyManager.addMoney (-50);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
					}
					else if(upgrade==1){ //second upgrade
						newTurret = (GameObject)Instantiate (basicTurret2); 
						moneyManager.addMoney (-50);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.f, 0); //spawn new turret
					}
					else if(upgrade==2){ //third upgrade
						newTurret = (GameObject)Instantiate (basicTurret3); 
						moneyManager.addMoney (-50);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.7f, 0); //spawn new turret
					}
					upgraded = true;
				}
				else{
					noMoney = true;
				}
			} else if (slow && GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().slowPressed) {
				if(moneyManager.getMoney() >= 35){
					if(upgrade==0){ //first upgrade
						newTurret = (GameObject)Instantiate (slowTurret1); 
						moneyManager.addMoney (-35);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
					}
					else if(upgrade==1){ //second upgrade
						newTurret = (GameObject)Instantiate (slowTurret2); 
						moneyManager.addMoney (-35);
						///newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
					}
					else if(upgrade==2){ //third upgrade
						newTurret = (GameObject)Instantiate (slowTurret3); 
						moneyManager.addMoney (-35);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
					}
					upgraded = true;
				}
				else{
					noMoney = true;
				}
			} else if (op && GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().opPressed) {
				if(moneyManager.getMoney() >= 100){
					if(upgrade==0){ //first upgrade
						newTurret = (GameObject)Instantiate (opTurret1); 
						moneyManager.addMoney (-100);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
					}
					else if(upgrade==1){ //second upgrade
						newTurret = (GameObject)Instantiate (opTurret2); 
						moneyManager.addMoney (-100);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.6f, 0); //spawn new turret
					}
					else if(upgrade==2){ //third upgrade
						newTurret = (GameObject)Instantiate (opTurret3); 
						moneyManager.addMoney (-100);
						//newTurret.transform.position = transform.position + new Vector3 (0, 0.7f, 0); //spawn new turret
					}
					upgraded = true;
				}
				else{
					noMoney = true;
				}
			}
			if(upgraded){
				Destroy(oldTurret); //destroy previous tower
				newTurret.transform.position = transform.position + new Vector3 (0, 0.45f, 0); //spawn new turret
				newTurret.GetComponent<TurretAttack>().resetFireRate();
				upgrade++;
			}
			else if (noMoney){
				StartCoroutine(noMinerals());
				StopCoroutine(noMinerals());
			}
		}
		else { //not in build mode
			//Debug.Log ("Not placing!");
		}
		//clear booleans
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript>().hasTurret && tile.GetComponent<TileScript>().basic){
				tile.GetComponent<TileScript> ().basic = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript>().hasTurret && tile.GetComponent<TileScript>().slow){
				tile.GetComponent<TileScript> ().slow = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript>().hasTurret && tile.GetComponent<TileScript>().op){
				tile.GetComponent<TileScript> ().op = false;
			}
		}
	}

	IEnumerator noMinerals(){
		GameObject.FindGameObjectWithTag ("noMinerals").GetComponent<Text> ().enabled = true;
		yield return new WaitForSeconds (2.0f); //wait
		GameObject.FindGameObjectWithTag ("noMinerals").GetComponent<Text> ().enabled = false;
	}
	
	void OnMouseEnter() { //change tile color
		if (basic || slow || op || hasTurret) { //only if a turret option is selected
			//change material
			if (basic) {
				GetComponent<Renderer>().sharedMaterial = yellowMaterial;
			}
			else if (slow) {
				//Debug.Log ("upgrade: " + upgrade);
				if(upgrade == 3){
					GetComponent<Renderer>().sharedMaterial = iceBlueMaterial;
				}
				else {
					GetComponent<Renderer>().sharedMaterial = greenMaterial;
				}
			}
			else if (op) {
				GetComponent<Renderer>().sharedMaterial = redMaterial;
			}


			GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	void OnMouseExit() {
		GetComponent<MeshRenderer> ().enabled = false;
	}
}