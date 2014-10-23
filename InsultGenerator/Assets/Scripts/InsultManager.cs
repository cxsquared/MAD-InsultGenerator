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

		GameObject.FindObjectOfType<Button> ().onClick.AddListener(() => { buttonClicked(); });
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Text txtObj in textArray) {
			InsultBox ib = txtObj.GetComponent("InsultBox") as InsultBox;
			ib.speed = this.speed;
		}

		if (rolling) {
			speed = 1000;
			done = false;
			talked = false;
		} else if (speed <= 20) {
			speed = 0;
			done = true;
		} else {
			speed *= 0.99f;
		}

		if (done && !talked) {
			talk ();
		}

		//Debug.Log (speed);
	}

	private void init(){
		textArray = new ArrayList ();
		for (int i = (int)-prefab.rectTransform.sizeDelta.y; i < canvasHeight + prefab.rectTransform.sizeDelta.y; i++) {
			addNewBox(i);
			i += (int) prefab.rectTransform.sizeDelta.y;
		}
	}

	private void addNewBox(int y) {
		Text newTxt = Instantiate (prefab) as Text;
		newTxt.transform.parent = this.transform;
		newTxt.rectTransform.sizeDelta = new Vector2 (800, 75);
		newTxt.rectTransform.position = new Vector3 (400, y);
		newTxt.transform.SetSiblingIndex (0);
		textArray.Add (newTxt);
	}

	public void insultBoxDestoryed(Text txtObj){
		textArray.Remove (txtObj);
		addNewBox ((int)-prefab.rectTransform.sizeDelta.y);
	}

	private void buttonClicked(){
		rolling = !rolling;
	}

	private void talk(){
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
