using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public GameObject displayWordPrefab;

    private Dictionary<char, List<string>> dictionary = new Dictionary<char, List<string>>();
    private Dictionary<char, GameObject> displayWords = new Dictionary<char, GameObject>();
    private List<char> availableCharacters = new List<char>();
    private List<char> unavailableCharacters = new List<char>();
    private DisplayWord currentWord = null;
    private char currentWordCharacter;

    void Start()
    {
        StreamReader reader = new StreamReader("Assets/Dictionary/words.txt");

        string line;

        do
        {
            line = reader.ReadLine();

            if (line != null)
            {
                char firstCharacter = line[0];

                if (dictionary.ContainsKey(firstCharacter))
                {
                    if (!dictionary[firstCharacter].Contains(line))
                    {
                        dictionary[firstCharacter].Add(line);
                    }
                }
                else
                {
                    List<string> words = new List<string>();

                    words.Add(line);

                    dictionary.Add(firstCharacter, words);
                }
            }
        } while (line != null);

        reader.Close();

        availableCharacters = new List<char>(dictionary.Keys);
    }

    public void SpawnWord()
    {
        if (availableCharacters.Count > 0)
        {
            char character = availableCharacters[Random.Range(0, availableCharacters.Count)];

            List<string> words = dictionary[character];

            DisableCharacter(character);

            string word = words[Random.Range(0, words.Count)];

            GameObject displayWord = Instantiate(displayWordPrefab, Vector3.zero, Quaternion.identity);

            displayWord.GetComponent<DisplayWord>().Setup(word, this);

            displayWords.Add(character, displayWord);
        }
        else
        {
            Debug.Log("No more words available");
        }
    }

    void DisableCharacter(char character)
    {
        availableCharacters.Remove(character);
        unavailableCharacters.Add(character);
    }

    void EnableCharacter(char character)
    {
        unavailableCharacters.Remove(character);
        availableCharacters.Add(character);
    }

    void Update()
    {
        foreach (char character in Input.inputString)
        {
            if (currentWord == null)
            {
                if (unavailableCharacters.Contains(character))
                {
                    currentWord = displayWords[character].GetComponent<DisplayWord>();
                    currentWord.Type(character);
                    currentWordCharacter = character;
                }
                else
                {
                    TypingError(character);
                }
            }
            else
            {
                currentWord.Type(character);
            }
        }
    }

    public void TypingError(char character)
    {
        Debug.Log("Typed '" + character + "' by mistake");
    }

    public void DisposeWord()
    {
        currentWord = null;
        EnableCharacter(currentWordCharacter);
        StartCoroutine(DestroyWord(displayWords[currentWordCharacter]));
        displayWords.Remove(currentWordCharacter);
    }

    IEnumerator DestroyWord(GameObject wordObject)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(wordObject);
    }
}
