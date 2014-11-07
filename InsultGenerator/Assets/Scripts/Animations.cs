using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Animations : MonoBehaviour {

	public GameObject startMenu;
	public int speed = 100;
	public GameObject startBtn;
	public bool isMoving = false;

	void Start(){
		startBtn.GetComponent<Button> ().onClick.AddListener(() => { clicked(); });
	}

	void clicked(){
		isMoving = !isMoving;
	}

	void Update(){
		if (isMoving){
			if (startMenu.transform.position.x > -735){
				startMenu.transform.Translate(Vector3.left * Time.deltaTime * speed);
			}
		}
	}
}
