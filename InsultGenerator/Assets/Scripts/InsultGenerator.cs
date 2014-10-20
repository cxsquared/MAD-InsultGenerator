using UnityEngine;
using System.Collections;

public class InsultGenerator : MonoBehaviour {

	/*
	public GameObject listItem;
	public GameObject itemSpawn;
	*/
	public Transform m_prefab;
	private Transform t;
	public bool tActive = true;

	// Use this for initialization
	void Start () {

		/*
		Instantiate(listItem);
		listItem.transform.position = itemSpawn.transform.position;
		listItem.transform.parent = itemSpawn.transform;
		*/


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
			t.transform.position += Vector3.up * Time.deltaTime * 60;
		}
	}

}
