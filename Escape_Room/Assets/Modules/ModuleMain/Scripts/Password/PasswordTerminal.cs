using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that represents the password terminal that opens the escape door.
 * Unlocking the door can be done by entering the correct password.
 */

public class PasswordTerminal : MonoBehaviour {

    private Dictionary<int, string> letterTranslator = new Dictionary<int, string>();
    private readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private string passwordTerminal = "default";

    public GameObject[] letterPanelPositions = new GameObject[10];
    public GameObject letter1, letter2, letter3, letter4, letter5, 
        letter6, letter7, letter8, letter9, letter10, escapeDoor;
    public Light indicatorLight;

    /*
     * Initializes the password terminal.
     */
    public void Start () {
        passwordTerminal = File.ReadAllText(@Application.dataPath + "/Settings/password.txt");
        InitializePanel();
        InitializeLetterDictionary();
        preparePasswordPanel();
    }

    /*
     * Initializes each individual letter in the password terminal.
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
     * Initializes the dictionary containing each letter of the alphabet.
     * This dictionary is used to display the correct letter for every panel position.
     */
    private void InitializeLetterDictionary()
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            letterTranslator[i+1] = Convert.ToString(alphabet[i]);
        }
    }

    private void preparePasswordPanel()
    {
        for (int i = 0; i < letterPanelPositions.Length; i++)
        {
            //string singleLetter;
            GameObject letter = letterPanelPositions[i];
            if (i < passwordTerminal.Length)
                letter.GetComponent<LetterChanger>().InitializeImage(1);
            else
                letter.GetComponent<LetterChanger>().InitializeImage(0);
            /*GameObject letter = letterPanelPositions[i];
            if(singleLetter.Equals(""))
                letter.GetComponent<LetterChanger>().SetImage(0);
            else
                letter.GetComponent<LetterChanger>().SetImage(GetLetterIndicator(singleLetter));*/
        }
    }
	
	public void CheckPassword () {
        GameObject letterPanel;
        string passwordAttempt = "";
        for (int i = 0; i < letterPanelPositions.Length; i++)
        {
            letterPanel = letterPanelPositions[i];
            int letterIndicator = letterPanel.GetComponent<LetterChanger>().GetCurrentLetter();
            if (!letterIndicator.Equals(0))
                passwordAttempt = GetPassword(passwordAttempt, GetLetter(letterIndicator));
        }
        if(passwordAttempt.Equals(passwordTerminal))
        {
            letterPanel = letterPanelPositions[0];
            indicatorLight.color = Color.green;
            escapeDoor.GetComponent<Door>().DoorMovable(true);
            TriggerDoorAnimation(escapeDoor);
        }
	}

    /*
     * Triggers the supplied door's animation, if it is not already running.
     * If it is closed, the door is opened. If it is opened, the door is closed.
     */
    private void TriggerDoorAnimation(GameObject target)
    {
        Door door = target.GetComponent<Door>();
        if (door.Running == false)
            StartCoroutine(door.Open());
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