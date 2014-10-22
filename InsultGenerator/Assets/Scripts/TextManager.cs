using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class TextManager : MonoBehaviour {

	private static ArrayList nouns;
	private static ArrayList adjectives;

	enum WordType {
		NOUN,
		ADJECTIVE
	};

	// Use this for initialization
	void Start () {
		nouns = new ArrayList ();
		adjectives = new ArrayList ();

		parseWords ("Assets/Scripts/nouns.txt", WordType.NOUN);
		parseWords ("Assets/Scripts/adjectives.txt", WordType.ADJECTIVE);

	}

	public static string getRandomNoun(){
		return getRandomWord(WordType.NOUN);
	}

	public static string getRandomAjective(){
		return getRandomWord (WordType.ADJECTIVE);
	}

	public static void addAdjective(string word){
		adjectives.Add (word);
	}

	public static void addNoun(string word){
		nouns.Add (word);
	}

	private static string getRandomWord(WordType type){
		ArrayList words = new ArrayList();
		switch (type) {
			case (WordType.NOUN):
				words = nouns;
				break;
			case (WordType.ADJECTIVE):
				words = adjectives;
				break;
			default:
				words = nouns;
				break;
		}

		int choice = Random.Range (0, words.Count - 1);
		string word = (string) words [choice];
		words.RemoveAt(choice);
		return word;
	}

	private bool parseWords(string fileName, WordType type){
		try {
			string line;

			StreamReader reader = new StreamReader(fileName, Encoding.Default);

			using(reader) {
				do {
					line = reader.ReadLine();

					if (line != null && line != ""){
						if (type == WordType.NOUN){
							nouns.Add(line);
						} else {
							adjectives.Add(line);
						}
					}
				} while(line != null);

				reader.Close();
				return true;
			}
		} catch (IOException e){
			Debug.Log(e.Message);
			return false;
		}
	}


}
