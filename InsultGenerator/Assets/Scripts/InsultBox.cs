using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsultBox : MonoBehaviour {

	private string noun;
	private string adjective;
	private Text textObj;
	private InsultManager igRef;
	public int canvasHeight;
	public float speed = 60f;

	// Use this for initialization
	void Start () {
		noun = TextManager.getRandomNoun ();
		adjective = TextManager.getRandomAjective ();

		textObj = this.GetComponent<Text> ();

		textObj.text = adjective + " " + noun;

		igRef = GameObject.FindObjectOfType<Canvas>().GetComponent ("InsultManager") as InsultManager;

		//Debug.Log ("igRef name = " + igRef.name);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * Time.deltaTime * speed;

		if (transform.position.y >= canvasHeight + textObj.rectTransform.sizeDelta.y) {
			TextManager.addNoun(noun);
			TextManager.addAdjective(adjective);
			igRef.insultBoxDestoryed(this.textObj);
			Destroy(this.gameObject);
		}
	}
}
