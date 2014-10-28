﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Enum to keep word types
public enum WordType {
	NOUN,
	ADJECTIVE
};

public class InsultManager : MonoBehaviour {

	public Text prefab;
	public GoogleTextToSpeech textToSpeech;

	private ArrayList nounArray;
	private ArrayList adjectiveArray;

	public int canvasHeight;
	public float speed = 60f;

	public bool rolling = false;
	public bool talked = false;
	public bool done = false;

	// Use this for initialization
	void Start () {
		init ();

		// Finding Button
		GameObject.FindObjectOfType<Button> ().onClick.AddListener(() => { buttonClicked(); });
	}
	
	// Update is called once per frame
	void Update () {
		// Setting the speed for each text obj in the array
		foreach (Text txtObj in nounArray) {
			InsultBox ib = txtObj.GetComponent("InsultBox") as InsultBox;
			ib.speed = this.speed;
		}

		foreach (Text txtObj in adjectiveArray) {
			InsultBox ib = txtObj.GetComponent("InsultBox") as InsultBox;
			ib.speed = this.speed;
		}

		if (rolling) {
			// When rolling set the speed and reset the bools.
			speed = 1000;
			done = false;
			talked = false;
		} else if (speed <= 20) {
			// Stop the rolling
			speed = 0;
			done = true;
		} else {
			// Slowly lowers the speed 
			speed *= 0.99f;
		}

		if (done && !talked) {
			talk ();
		}

		//Debug.Log (speed);
	}

	private void init(){
		nounArray = new ArrayList ();
		adjectiveArray = new ArrayList ();
		// Adds all the starting text objects to the stage
		for (int i = canvasHeight + (int)prefab.rectTransform.sizeDelta.y; i > 0; i -= (int) prefab.rectTransform.sizeDelta.y) {
			addNewBox(i, true, WordType.NOUN);
			addNewBox(i, true, WordType.ADJECTIVE);
		}
	}

	private void addNewBox(int y, bool init, WordType type) {
		// Instantiat new text object
		Text newTxt = Instantiate (prefab) as Text;
		// Setting parent as the gameObject attached to this script
		newTxt.transform.SetParent(this.transform, false);
		// This is a temp fix for weird anchor things happening
		newTxt.rectTransform.sizeDelta = new Vector2 (Screen.width/2, 75);
		// this makes sure that the text object is rendered behind the button
		newTxt.transform.SetSiblingIndex (0);
		// Adding to array so we can keep track of it
		if (type == WordType.NOUN) {
			addNounBox(y, newTxt, init);
		} else {
			addAdjectiveBox(y, newTxt, init);
		}

	}

	private void addAdjectiveBox (int y, Text text, bool init) {
		// this centers the text object
		if (!init) {
			Text lastText = (Text) adjectiveArray[adjectiveArray.Count-1];
			Debug.Log("Last rect at " + lastText.rectTransform.position.y);
			text.rectTransform.position = new Vector3(text.rectTransform.sizeDelta.y, lastText.rectTransform.position.y + lastText.rectTransform.sizeDelta.y);
		} else {
			text.rectTransform.position = new Vector3 (text.rectTransform.sizeDelta.y, y);
		}
		// Adding to array so we can keep track of it
		InsultBox ib = text.GetComponent("InsultBox") as InsultBox;
		ib.type = WordType.ADJECTIVE;
		adjectiveArray.Add (text);

		Debug.Log("Adjective added at " + text.rectTransform.position.y);
	}

	private void addNounBox(int y, Text text, bool init){
		// this centers the text object
		if (!init) {
			Text lastText = (Text) nounArray[nounArray.Count-1];
			text.rectTransform.position = new Vector3(-text.rectTransform.sizeDelta.y + Screen.width, lastText.rectTransform.position.y - lastText.rectTransform.sizeDelta.y);
		} else {
			text.rectTransform.position = new Vector3 (-text.rectTransform.sizeDelta.y + Screen.width, y);
		}
		// Adding to array so we can keep track of it
		InsultBox ib = text.GetComponent("InsultBox") as InsultBox;
		ib.type = WordType.NOUN;
		nounArray.Add (text);
	}

	public void insultBoxDestoryed(Text txtObj, WordType type){
		if (type == WordType.NOUN) {
			nounArray.Remove(txtObj);
			//Adds a text box to the bottom of the stage
			addNewBox ((int)-prefab.rectTransform.sizeDelta.y, false, type);
		} else {
			adjectiveArray.Remove(txtObj);
			//Adds a text box to the bottom of the stage
			addNewBox ((int)(canvasHeight), false, type);
		}
		//Adds a text box to the bottom of the stage
	}

	private void buttonClicked(){
		// toggles rolling. Sets rolling to the oposite of what it was
		rolling = !rolling;
	}

	private void talk(){
		// Finding which text obj is mostly in the middle and saying it.
		// Using the talked bool to make sure the program only says one insult
//		foreach (Text txt in textArray) {
//			float yPos = txt.rectTransform.position.y;
//			if (yPos >= canvasHeight/2 - txt.rectTransform.sizeDelta.y && yPos <= canvasHeight/2 + txt.rectTransform.sizeDelta.y){
//				textToSpeech.say(txt.text);
//				talked = true;
//				return;
//			}
//		}
	}
}
