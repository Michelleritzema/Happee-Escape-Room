using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that represents the password terminal that opens the escape door.
 * Unlocking the door can be done by entering the correct password.
 */
public class PasswordTerminal : MonoBehaviour {

    public Game game;

    private Dictionary<int, string> letterTranslator = new Dictionary<int, string>();
    private readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private string passwordTerminal = "default";
    private GameObject[] letterPanelPositions = new GameObject[10];

    public GameObject letter1, letter2, letter3, letter4, letter5, 
        letter6, letter7, letter8, letter9, letter10;

    /*
     * Initializes the password terminal.
     */
    public void Start () {
        passwordTerminal = File.ReadAllText(@Application.dataPath + "/Settings/password.txt");
        //passwordTerminal = game.GetComponent<Settings>().GetPassword();
        Debug.Log(GameObject.Find("Game").GetComponent<Settings>().GetPassword());
        InitializePanel();
        InitializeLetterDictionary();
        PreparePasswordPanel();
    }

    private void InitializePanel()
    {
        letterPanelPositions[0] = letter1;
        letterPanelPositions[1] = letter2;
        letterPanelPositions[2] = letter3;
        letterPanelPositions[3] = letter4;
        letterPanelPositions[4] = letter5;
        letterPanelPositions[5] = letter6;
        letterPanelPositions[6] = letter7;
        letterPanelPositions[7] = letter8;
        letterPanelPositions[8] = letter9;
        letterPanelPositions[9] = letter10;
    }

    private void InitializeLetterDictionary()
    {
        letterTranslator = new Dictionary<int, string>();
        letterTranslator[1] = "a";
        letterTranslator[2] = "b";
        letterTranslator[3] = "c";
        letterTranslator[4] = "d";
        letterTranslator[5] = "e";
        letterTranslator[6] = "f";
        letterTranslator[7] = "g";
        letterTranslator[8] = "h";
        letterTranslator[9] = "i";
        letterTranslator[10] = "j";
        letterTranslator[11] = "k";
        letterTranslator[12] = "l";
        letterTranslator[13] = "m";
        letterTranslator[14] = "n";
        letterTranslator[15] = "o";
        letterTranslator[16] = "p";
        letterTranslator[17] = "q";
        letterTranslator[18] = "r";
        letterTranslator[19] = "s";
        letterTranslator[20] = "t";
        letterTranslator[21] = "u";
        letterTranslator[22] = "v";
        letterTranslator[23] = "w";
        letterTranslator[24] = "x";
        letterTranslator[25] = "y";
        letterTranslator[26] = "z";
    }

    private void PreparePasswordPanel()
    {
        for (int i = 0; i < letterPanelPositions.Length; i++)
        {
            GameObject letter = letterPanelPositions[i];
            if (i < passwordTerminal.Length)
                letter.GetComponent<LetterChanger>().InitializeImage(1);
            else
                letter.GetComponent<LetterChanger>().InitializeImage(0);
        }
    }
	
	public string FetchSubmittedPassword() {
        GameObject letterPanel;
        string passwordAttempt = "";
        for (int i = 0; i < letterPanelPositions.Length; i++)
        {
            letterPanel = letterPanelPositions[i];
            int letterIndicator = letterPanel.GetComponent<LetterChanger>().GetCurrentLetter();
            if (!letterIndicator.Equals(0))
                passwordAttempt = GetPassword(passwordAttempt, GetLetter(letterIndicator));
        }
        return passwordAttempt;
    }

    /*
     * Determines the password that the user has submitted and compares this to the predefined one.
     * If it matches, the game will execute the actions .
     */
    public void CheckPassword () {
        string passwordAttempt = FetchSubmittedPassword();
        if (passwordAttempt.Equals(passwordTerminal))
        {
            game.GetComponent<Game>().Escaping();
        }
	}

    private int GetLetterIndicator(string letter)
    {
        return letterTranslator.FirstOrDefault(x => x.Value == letter).Key;
    }

    private string GetLetter(int letterIndicator)
    {
        return letterTranslator[letterIndicator];
    }

    private string GetPassword(string passwordAttempt, string letter)
    {
        return passwordAttempt += letter;
    }

    /*private byte[] GetHash(string input)
    {
        HashAlgorithm algorithm = MD5.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
    }

    private string GetHashString(byte[] input)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in input)
            sb.Append(b.ToString("X2"));
        return sb.ToString();
    }*/

}