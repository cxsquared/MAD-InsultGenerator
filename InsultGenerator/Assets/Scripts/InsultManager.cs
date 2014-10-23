using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsultManager : MonoBehaviour {

	public Text prefab;
	public GoogleTextToSpeech textToSpeech;

	private ArrayList textArray;

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
		foreach (Text txtObj in textArray) {
			InsultBox ib = txtObj.GetComponent("InsultBox") as InsultBox;
			ib.speed = this.speed;
		}

		if (rolling) {
			// When rolling set the speed and reset the bools.
			speed = 600;
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
		textArray = new ArrayList ();
		// Adds all the starting text objects to the stage
		for (int i = (int)-prefab.rectTransform.sizeDelta.y; i < canvasHeight + prefab.rectTransform.sizeDelta.y; i += (int) prefab.rectTransform.sizeDelta.y) {
			addNewBox(i);
		}
	}

	private void addNewBox(int y) {
		// Instantiat new text object
		Text newTxt = Instantiate (prefab) as Text;
		// Setting parent as the gameObject attached to this script
		newTxt.transform.parent = this.transform;
		// This is a temp fix for weird anchor things happening
		newTxt.rectTransform.sizeDelta = new Vector2 (800, 75);
		// this centers the text object
		newTxt.rectTransform.position = new Vector3 (400, y);
		// this makes sure that the text object is rendered behind the button
		newTxt.transform.SetSiblingIndex (0);
		// Adding to array so we can keep track of it
		textArray.Add (newTxt);
	}

	public void insultBoxDestoryed(Text txtObj){
		textArray.Remove (txtObj);
		//Adds a text box to the bottom of the stage
		addNewBox ((int)-prefab.rectTransform.sizeDelta.y);
	}

	private void buttonClicked(){
		// toggles rolling. Sets rolling to the oposite of what it was
		rolling = !rolling;
	}

	private void talk(){
		// Finding which text obj is mostly in the middle and saying it.
		// Using the talked bool to make sure the program only says one insult
		foreach (Text txt in textArray) {
			float yPos = txt.rectTransform.position.y;
			if (yPos >= canvasHeight/2 - txt.rectTransform.sizeDelta.y && yPos <= canvasHeight/2 + txt.rectTransform.sizeDelta.y){
				textToSpeech.say(txt.text);
				talked = true;
				return;
			}
		}
	}
}
