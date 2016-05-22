using UnityEngine;
using System.Collections;

public class Register : MonoBehaviour
{

    private string teamName = "";
    private Rect windowRectangle = new Rect(0,0,Screen.width, Screen.height);

    public void OnGUI()
    {
        GUI.Window(0, windowRectangle, WindowFunction, "Enter team name");
    }

    private void WindowFunction(int windowID)
    {
        teamName = GUI.TextField(new Rect(Screen.width/3, 2 * Screen.height/5, Screen.width/3, Screen.height/10), teamName, 20);
        if (GUI.Button(new Rect(Screen.width / 2, 4 * Screen.height / 5, Screen.width / 8, Screen.height / 8), "Enter team name"))
        {
            Debug.Log("Welcome " + teamName);
            Application.LoadLevel("MainRoom");
        }
        GUI.Label(new Rect(Screen.width/3, 35 * Screen.height/100, Screen.width/5, Screen.height/8), "Team name");
    }

}