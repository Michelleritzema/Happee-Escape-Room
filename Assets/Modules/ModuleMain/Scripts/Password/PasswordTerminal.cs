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

    private GameObject[] letterPanelPositions = new GameObject[10];
    private Dictionary<int, string> letterTranslator = new Dictionary<int, string>();
    private readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private string password = "default";

    public Game game;
    public GameObject letter1, letter2, letter3, letter4, letter5, 
        letter6, letter7, letter8, letter9, letter10;

    /*
     * Initializes the password terminal.
     * The password string is fetched from the json settings file.
     */
    public void Start () {
        password = game.GetComponent<Settings>().GetPassword();
        Debug.Log("Stored password: " + password);
        InitializePanel();
        InitializeLetterDictionary();
        PreparePasswordPanel();
    }

    /*
     * Fills the letter panel array with all the letter panel game objects.
     */
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

    /*
     * Fills the letter translator dictionary with the correct letter of the alphabet.
     * Each letter panel holds an integer that can thus be mapped to a letter.
     */
    private void InitializeLetterDictionary()
    {
        letterTranslator = new Dictionary<int, string>();
        for(int i = 0; i < alphabet.Length; i++)
        {
            letterTranslator[i+1] = alphabet[i].ToString();
        }
    }

    /*
     * Activates the number of letter panels that are needed to submit the correct password.
     * All other redundant panels are set as inactive.
     */
    private void PreparePasswordPanel()
    {
        for (int i = 0; i < letterPanelPositions.Length; i++)
        {
            GameObject letter = letterPanelPositions[i];
            if (i < password.Length)
                letter.GetComponent<LetterChanger>().InitializePanel(1);
            else
                letter.GetComponent<LetterChanger>().InitializePanel(0);
        }
    }
	
    /*
     * Computes the user submitted password by iterating over each letter panel and gathering 
     * their letter indicator integers. Each integer is mapped to a letter string with the dictionary.
     * The complete attempt string is returned.
     */
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
     * If it matches, the game will call the escaping method in the game script.
     */
    public void CheckPassword () {
        string passwordAttempt = FetchSubmittedPassword();
        if (passwordAttempt.Equals(password))
            game.GetComponent<Game>().Escaping();
	}

    /*
     * Fetches the letter indicator integer that matches the supplied letter string.
     */
    private int GetLetterIndicator(string letter)
    {
        return letterTranslator.FirstOrDefault(x => x.Value == letter).Key;
    }

    /*
     * Fetches the letter string that matches the supplied letter indicator integer.
     */
    private string GetLetter(int letterIndicator)
    {
        return letterTranslator[letterIndicator];
    }

    /*
     * Fetches the password string.
     */
    private string GetPassword(string passwordAttempt, string letter)
    {
        return passwordAttempt += letter;
    }

}