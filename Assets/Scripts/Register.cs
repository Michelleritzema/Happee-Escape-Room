using UnityEngine;
using System.Collections;

public class Register : MonoBehaviour
{

    private string teamName = "";
    private Rect windowRectangle = new Rect((Screen.width/2) - 700, (Screen.height / 2) - 400, 1400, 800);

    private string titleIntroduction;

    public void Start()
    {
        titleIntroduction = GetComponent<Setup>().titleIntroduction;
        Debug.Log("in game: " + titleIntroduction);
    }

    public void OnGUI()
    {
        GUI.Window(0, windowRectangle, WindowFunction, titleIntroduction);
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

    /*
     * Create a GUI style for the text display box and return this object.
     */
    public GUIStyle GetStyle(int fontSize)
    {
        GUIStyle style = new GUIStyle();
        //Font font = (Font)Resources.Load("Fonts/comic", typeof(Font));
        //style.font = font;
        style.fontSize = fontSize;
        return style;
    }

}