using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public static string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject spikePrefab;
    public GameObject polePrefab;
    public Transform environmentRoot;

    public TextAsset file1;
    public TextAsset file2;
    public static bool loadSecondLevel;

    // --------------------------------------------------------------------------
    void Start()
    {
        filename ??= "Test";
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        //string fileToParse = $"{Application.dataPath}{"/Asset/"}{filename}.txt";
        //Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // // Get each line of text representing blocks in our level
        // using (StreamReader sr = new StreamReader(fileToParse))
        // {
        //     string line = "";
        //     while ((line = sr.ReadLine()) != null)
        //     {
        //         levelRows.Push(line);
        //     }
        //
        //     sr.Close();
        // }

        TextAsset text = loadSecondLevel ? file2 : file1;

        string[] lines = text.text.Split('\n');

        foreach (var line in lines)
        {
            levelRows.Push(line);
        }

        int row = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            { 
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                Vector3 position = environmentRoot.position +  new Vector3(column, row);
                GameObject block;
                switch (letter)
                {
                    case 'x':
                        block = Instantiate(rockPrefab,position,Quaternion.identity,environmentRoot);
                        break;
                    case 'b':
                        block = Instantiate(brickPrefab,position,Quaternion.identity,environmentRoot);
                        break;
                    case '?':
                        block = Instantiate(questionBoxPrefab,position,Quaternion.identity,environmentRoot);
                        break;
                    case 's':
                        block = Instantiate(stonePrefab,position,Quaternion.identity,environmentRoot);
                        break;
                    case 'k':
                        block = Instantiate(spikePrefab,position,Quaternion.identity,environmentRoot);
                        break;
                    case 'p':
                        block = Instantiate(polePrefab,position,Quaternion.identity,environmentRoot);
                        break;
                }
                
            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
