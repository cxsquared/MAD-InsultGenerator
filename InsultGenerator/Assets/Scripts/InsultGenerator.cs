using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsultGenerator : MonoBehaviour {

	/*
	public GameObject listItem;
	public GameObject itemSpawn;
	*/
	public GameObject m_prefab;
	//private Transform t;
	public bool tActive = true;

	public Button btn;
	public Text btnText;

	public GoogleTextToSpeech textToSpeech;

	public int insultBoxHeight;

	// Use this for initialization
	void Start () {

		/*
		Instantiate(listItem);
		listItem.transform.position = itemSpawn.transform.position;
		listItem.transform.parent = itemSpawn.transform;
		*/

		btn.onClick.AddListener(() => { btnClick(); });

		init ();

		/*
		t = Instantiate (m_prefab) as Transform;
		t.parent = transform;
		//t.localPosition = Vector3.zero;
		t.localPosition = new Vector3(0 - 100,btn.transform.localPosition.y - 100, 0);
		t.localRotation = Quaternion.identity;
		t.gameObject.name="My awesome Instance!";
		*/



		//t.transform.Translate (Vector3.up * Time.deltaTime * 20);
		                                                            
	}

	private void init(){

		int startLocaiton = -300;

		for (int i = 0; i < 4; i++) {
			GameObject t = Instantiate (m_prefab) as GameObject;
			Debug.Log("Name = " + m_prefab.name);
			t.transform.parent = this.transform;
			//t.localPosition = Vector3.zero;
			t.transform.localPosition = new Vector3 (0 - 100, startLocaiton + (insultBoxHeight*i), 0);
			t.transform.localRotation = Quaternion.identity;
			//t.name = "My awesome Instance!";
		}
	}

	
	// Update is called once per frame
	void Update () {
		/*if (tActive){
			if (t != null){
				//t.transform.position += Vector3.up * Time.deltaTime * 60;
			}
		}*/
	}

	private void btnClick() {
		string noun = TextManager.getRandomNoun();
		string adjective = TextManager.getRandomAjective();

		btnText.text = adjective + " " + noun;

		textToSpeech.say (adjective + " " + noun);

		TextManager.addNoun (noun);
		TextManager.addAdjective (adjective);

	}

	public void insultBoxDestoryed(){
		addNewBox ();
	}

	private void addNewBox(){
		GameObject t = Instantiate (this.m_prefab) as GameObject;
		Debug.Log (this.m_prefab.name);
		t.transform.parent = this.transform;
		//t.localPosition = Vector3.zero;
		t.transform.localPosition = new Vector3(0 - 100,-300, 0);
		t.transform.localRotation = Quaternion.identity;
		//t.name="My awesome Instance!";
	}

}
