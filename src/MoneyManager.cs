using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyManager : MonoBehaviour {

	public float moneyGeneratingSpeed;
	private int money;

	Text textMaster;

	void Awake(){
		textMaster = GetComponent<Text> ();
		money = 100;
		StartCoroutine (moneyGenerator ());
	}

	void Update(){
		textMaster.text = "$ " + money.ToString();
	}

	IEnumerator moneyGenerator(){
		while(true){
			 money += 1;
			yield return new WaitForSeconds (moneyGeneratingSpeed);
		}
	}

	public int getMoney(){
		return money;
	}

	public void setMoney(int amount){
		money = amount;
	}

	public void addMoney(int amount){
		Debug.Log ("amount: " + amount);
		money += amount;
		StartCoroutine (animateMoney(amount));
		StopCoroutine (animateMoney (amount));
	}

	IEnumerator animateMoney(int amount) {
		//Debug.Log ("textMoney: " + textMoney);
		string textMoney;
		if (amount > 0) {
			textMoney = "+ $" + amount;
			GameObject.FindGameObjectWithTag ("addMoney").GetComponent<Text> ().enabled = true;
			GameObject.FindGameObjectWithTag ("addMoney").GetComponent<Text> ().text = textMoney;
			Debug.Log (GameObject.FindGameObjectWithTag ("addMoney").GetComponent<Text> ().text.ToString ());
			yield return new WaitForSeconds (1.5f); //wait
			GameObject.FindGameObjectWithTag ("addMoney").GetComponent<Text> ().enabled = false;//disable
		} else if (amount < 0) {
			textMoney = "- $" + (amount*(-1));
			GameObject.FindGameObjectWithTag ("minusMoney").GetComponent<Text> ().enabled = true;
			GameObject.FindGameObjectWithTag ("minusMoney").GetComponent<Text> ().text = textMoney;
			Debug.Log (GameObject.FindGameObjectWithTag ("minusMoney").GetComponent<Text> ().text.ToString ());
			yield return new WaitForSeconds (1.5f); //wait
			GameObject.FindGameObjectWithTag ("minusMoney").GetComponent<Text> ().enabled = false;//disable
		}
	}

}
