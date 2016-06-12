using UnityEngine;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the introduction of the game.
 * Here a team name is entered and the team gets to see how everything works.
 */

public class Register : MonoBehaviour
{

    private string teamName = "";
    private bool displayEmptyWarning = false;
    private int width = 1400, height = 800;

    public Game game;
    public Texture texture;
    public Rect windowRectangle;
    public string titleIntroduction, welcome, enterTeamName, emptyTeamNameWarning, next;

    /*
     * Initializes all the introduction messages.
     */
    public void Start()
    {
        windowRectangle = new Rect((Screen.width / 2) - 700, (Screen.height / 2) - 400, width, height);
        titleIntroduction = GetComponent<Settings>().titleIntroduction;
        welcome = GetComponent<Settings>().welcome;
        enterTeamName = GetComponent<Settings>().enterTeamName;
        emptyTeamNameWarning = GetComponent<Settings>().emptyTeamNameWarning;
        next = GetComponent<Settings>().next;
    }

    /*
     * Shows certain GUI elements if the conditions are met.
     */
    public void OnGUI()
    {
        if(!game.GetComponent<Game>().GetStarted())
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 700, (Screen.height / 2) - 400, width, height), texture, ScaleMode.StretchToFill, true, 10.0F);
            GUI.Window(0, windowRectangle, IntroductionScreen, titleIntroduction, game.GetComponent<Game>().GetStyle(14, Game.GUIType.Window));
        }
        if(displayEmptyWarning)
        {
            GUI.color = Color.red;
            GUI.Box(new Rect((Screen.width / 2) - 140, 280, 300, 60), emptyTeamNameWarning, game.GetComponent<Game>().GetStyle(18, Game.GUIType.Label));
        }
    }

    /*
     * Draws the different introduction screens. The user can read the instructions and edit some settings.
     * If all windows have been visited, the game is started by switching to the player camera.
     */
    private void IntroductionScreen(int windowID)
    {
        GUI.color = Color.white;
        teamName = GUI.TextField(new Rect((Screen.width / 2) - 420, 280, 300, 80), teamName, 20, game.GetComponent<Game>().GetStyle(22, Game.GUIType.Button));
        if (GUI.Button(new Rect(Screen.width - 800, Screen.height - 220, 160, 50), next, game.GetComponent<Game>().GetStyle(25, Game.GUIType.Button)))
        {
            if(teamName.Equals(""))
            {
                displayEmptyWarning = true;
            }
            else
            {
                GetComponent<Settings>().SetTeamName(teamName);
                game.GetComponent<Game>().SwitchToPlayerCamera();
                game.GetComponent<Game>().SetStarted(true);
                displayEmptyWarning = false;
                game.GetComponent<Game>().SetTime();
            }
        }
        GUI.Label(new Rect((Screen.width / 2) - 420, 200, width - 400, 200), enterTeamName, game.GetComponent<Game>().GetStyle(25, Game.GUIType.Label));
        GUI.Label(new Rect((Screen.width / 2) - 500, 80, width - 400, 200), welcome, game.GetComponent<Game>().GetStyle(25, Game.GUIType.Label));
    }

}