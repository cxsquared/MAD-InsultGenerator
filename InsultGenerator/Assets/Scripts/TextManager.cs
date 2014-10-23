using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

/*
 * This class is used to create a list of nouns and adjectives
 * and give them out to other objects when needed.
 * 
 */

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

		// You can use these two files to add more nouns or adjectives.
		// Just go to the files and each line is treated as a word.
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
		// The words Array and switch are being used to tell the function
		// which ArrayList should be used to find the random word.
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

		// Generating a random number between 0 and the # of words in Array
		int choice = Random.Range (0, words.Count - 1);
		// Getting random word and storing it so we can remove it from Array
		string word = (string) words [choice];
		// Removing from Array to prevent duplicates
		words.RemoveAt(choice);
		return word;
	}

	private bool parseWords(string fileName, WordType type){
		try {
			string line;

			// StreamReader is what actually reads the text files
			StreamReader reader = new StreamReader(fileName, Encoding.Default);

			// While we still have a line to read in the text file
			using(reader) {
				do {
					// Get the current line
					line = reader.ReadLine();

					// Check if the line actually exists
					if (line != null && line != ""){
						if (type == WordType.NOUN){
							nouns.Add(line);
						} else {
							adjectives.Add(line);
						}
					}
				} while(line != null);

				// clean up so we don't break the program.
				reader.Close();
				return true;
			}
		} catch (IOException e){
			Debug.Log(e.Message);
			return false;
		}
	}


}
