using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * This Class should be attached to a prefab Text box.
 * This controls the movement of the text box and
 * takes care of killing it's self
 *
 */

public class InsultBox : MonoBehaviour {

	private string word;
	private Text textObj;
	private InsultManager insultManagerRef;
	public int canvasHeight; // I need to find this in the code. Hard coding is bad.
	public float speed = 60f;
	public WordType type;

	// Use this for initialization
	void Start () {
		if (type == WordType.NOUN) {
			word = TextManager.getRandomNoun ();
		} else {
			word = TextManager.getRandomAjective ();
		}

		// Getting the Text object this script is attached to.
		textObj = this.GetComponent<Text> ();

		textObj.text = word;

		// Finding the Canvas (cause there should only be one) and getting the InsultManager class from it
		insultManagerRef = GameObject.FindObjectOfType<Canvas>().GetComponent ("InsultManager") as InsultManager;

		//Debug.Log ("igRef name = " + igRef.name);
	}
	
	// Update is called once per frame
	void Update () {
		// Moving the object
		if (type == WordType.NOUN) {
			transform.position += Vector3.up * Time.deltaTime * speed;
			// Check if this obj has left the top of the screen.
			if (transform.position.y >= canvasHeight + textObj.rectTransform.sizeDelta.y) {
				//Debug.Log("A " + type + " is being destoryed");
				//Giving back the noun and adjective to be reused by the TextManager
				if (type == WordType.NOUN){
					TextManager.addNoun(word);
				} else {
					TextManager.addAdjective(word);
				}
				// Telling the InsultManager that this obj is getting destoryed.
				insultManagerRef.insultBoxDestoryed(this.textObj, this.type);
				Destroy(this.gameObject);
			}
		} else {
			transform.position -= Vector3.up * Time.deltaTime * speed;
			// Check if this obj has left the top of the screen.
			if (transform.position.y <= -textObj.rectTransform.sizeDelta.y) {
				//Debug.Log("A " + type + " is being destoryed");
				//Giving back the noun and adjective to be reused by the TextManager
				if (type == WordType.NOUN){
					TextManager.addNoun(word);
				} else {
					TextManager.addAdjective(word);
				}
				// Telling the InsultManager that this obj is getting destoryed.
				insultManagerRef.insultBoxDestoryed(this.textObj, this.type);
				Destroy(this.gameObject);
			}
		}



	}
}
