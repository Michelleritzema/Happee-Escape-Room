using UnityEngine;
using System.Collections;

public class LaptopScreen : MonoBehaviour
{
    public Game game;
    private int width = 1400;
    private int height = 800;
    public Texture texture;
    public Rect windowRectangle;
    public Camera PcCamera;
    // Use this for initialization
    void Start()
    {
        windowRectangle = new Rect((Screen.width / 2) - 700, (Screen.height / 2) - 400, width, height);

    }
    /*
     * creates the image of the laptop screen and handles the switching of the cameras
     */
    public void OnGUI()
    {
        if (PcCamera.gameObject.activeSelf == true)
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 700, (Screen.height / 2) - 400, width, height), texture, ScaleMode.StretchToFill, true, 10.0F);
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 120, 200, 50), "Scherm verlaten", game.GetComponent<Game>().GetStyle(25, Game.GUIType.Button)))
            {
                game.GetComponent<Game>().SwitchToPlayerCamera();
            }
        }
        
    }

}
