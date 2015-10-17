using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

	public GameObject[] tiles;
	//MoneyManager moneyManager;

	void Awake(){
		//moneyManager = GameObject.FindGameObjectWithTag ("MoneyManager").GetComponent<MoneyManager> ();
		tiles = GameObject.FindGameObjectsWithTag ("TileOpen");
	}

	public void spawnBasic(){ //50
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().slow = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().op = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().basic = true;
			}
		}
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().basicPressed = true;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().slowPressed = false;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().opPressed = false;
	}

	public void spawnSlow(){ //35
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript> ().hasTurret) {
				tile.GetComponent<TileScript> ().basic = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript> ().hasTurret) {
				tile.GetComponent<TileScript> ().op = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if (!tile.GetComponent<TileScript> ().hasTurret) {
				tile.GetComponent<TileScript> ().slow = true;
			}
		}	
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().basicPressed = false;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().slowPressed = true;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().opPressed = false;
	}

	public void spawnOP(){ //100
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().basic = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().slow = false;
			}
		}
		foreach (GameObject tile in tiles) {
			if(!tile.GetComponent<TileScript>().hasTurret){
				tile.GetComponent<TileScript> ().op = true;
			}
		}
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().basicPressed = false;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().slowPressed = false;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().opPressed = true;
	}

	void Start(){
		tiles = GameObject.FindGameObjectsWithTag ("TileOpen");
	}

}
