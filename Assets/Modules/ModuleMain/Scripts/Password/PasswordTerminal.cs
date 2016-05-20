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
        passwordTerminal = game.GetComponent<Settings>().GetPassword();
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
        for(int i = 0; i < alphabet.Length; i++)
        {
            letterTranslator[i+1] = alphabet[i].ToString();
        }
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

}