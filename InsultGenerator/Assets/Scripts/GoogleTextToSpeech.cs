using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// <author>Jefferson Reis</author>
/// <explanation>Works only on Android, or platform that supports mp3 files. To test, change the platform to Android.</explanation>
/// </summary>

public class GoogleTextToSpeech : MonoBehaviour
{
	public string words = "Hello";
	public bool french = false;

	[SerializeField]
	private Button MyButton = null; // assign in the editor

	[SerializeField]
	private InputField field = null;
	
	public void Start ()
	{
		//MyButton.onClick.AddListener(() => { onClick();});  

	}
	
	public void setText(string text){
		words = text;
	}

	public void say(string text){
		setText (text);
		StartCoroutine (say ());
	}

	public void onClick() {
		setText (field.value);
		StartCoroutine(say ());
		Debug.Log ("Button clicked");
	}

	public IEnumerator say(){
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");
		// Replace the "spaces" with "% 20" for the link Can be interpreted
		string result = rgx.Replace (words, "%20");
		string url = "";
		if (french) {
			url = "http://translate.google.com/translate_tts?tl=fr&q=" + result;
		} else {
			url = "http://translate.google.com/translate_tts?tl=en&q=" + result;
		}
		WWW www = new WWW (url);
		yield return www;
		audio.clip = www.GetAudioClip (false, false, AudioType.MPEG);
		audio.Play ();
	}

	
	
}//closes the class