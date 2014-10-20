using UnityEngine;
using System.Collections;

public class destroyItem : MonoBehaviour {

	public GameObject insultGenParent;
	private InsultGenerator igRef;
	private bool isMoving;


	void Start (){
		igRef = insultGenParent.GetComponent<InsultGenerator>();
	}

	void OnTriggerEnter (Collider other){
		Destroy(this.gameObject);
		isMoving = igRef.tActive;
		Debug.Log ("Shoulda flipped das switch");
		Debug.Log (isMoving);
	}
}
