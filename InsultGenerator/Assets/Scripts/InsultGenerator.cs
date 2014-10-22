using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InsultGenerator : MonoBehaviour {

	/*
	public GameObject listItem;
	public GameObject itemSpawn;
	*/
	public Transform m_prefab;
	private Transform t;
	public bool tActive = true;

	public Button bnt;
	public Text btnText;

	public GoogleTextToSpeech textToSpeech;

	// Use this for initialization
	void Start () {

		/*
		Instantiate(listItem);
		listItem.transform.position = itemSpawn.transform.position;
		listItem.transform.parent = itemSpawn.transform;
		*/

		bnt.onClick.AddListener(() => { btnClick(); });

		t = Instantiate (m_prefab) as Transform;
		t.parent = transform;
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.gameObject.name="My awesome Instance!";


		/*t.transform.Translate (Vector3.up * Time.deltaTime * 20);*/


	}

	
	// Update is called once per frame
	void Update () {
		if (tActive){
			if (t != null){
				t.transform.position += Vector3.up * Time.deltaTime * 60;
			}
		}
	}

	private void btnClick() {
		string noun = TextManager.getRandomNoun();
		string adjective = TextManager.getRandomAjective();

		btnText.text = adjective + " " + noun;

		textToSpeech.say (adjective + " " + noun);

		TextManager.addNoun (noun);
		TextManager.addAdjective (adjective);

	}

}
