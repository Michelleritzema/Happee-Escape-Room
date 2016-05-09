using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PasswordTerminal : MonoBehaviour {

    private string passwordTerminal = "bb";
    private Dictionary<int, string> letterTranslator;
    public GameObject[] letterPanelPositions = new GameObject[10];
    public GameObject letter1, letter2, letter3, letter4, letter5, 
        letter6, letter7, letter8, letter9, letter10, escapeDoor;
    public Light indicatorLight;


    // Use this for initialization
    void Start () {
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
        preparePasswordPanel();
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