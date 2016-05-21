using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the general actions of the game.
 * Also knows which modules are loaded into the game.
 */

public class Game : MonoBehaviour {

    private string[] modules = new string[4];

    public GameObject escapeDoorIndicatorGlass;
    public Light escapeDoorIndicatorLight;
    public Door escapeDoor, puzzleDoor;

    /*
     * Initializes the game.
     */
    public void Start()
    {
        escapeDoor.DoorMovable(false);
    }

    /*
     * Triggers when the user entered the correct password.
     * The escape door is opened and the indicator light changes.
     */
    public void Escaping()
    {
        escapeDoorIndicatorLight.color = Color.green;
        escapeDoorIndicatorGlass.GetComponent<Renderer>().material.color = Color.green;
        escapeDoor.GetComponent<Door>().DoorMovable(true);
        TriggerDoorAnimation(escapeDoor);
    }

    /*
     * Triggers when the user has escaped the room.
     * All doors are closed and the puzzle rooms are removed.
     */
    public void Escaped()
    {
        if (escapeDoor.GetComponent<Door>().GetOpened())
            TriggerDoorAnimation(escapeDoor);
        if (puzzleDoor.GetComponent<Door>().GetOpened())
            TriggerDoorAnimation(puzzleDoor);
        modules[0] = GameObject.Find("Game").GetComponent<Settings>().GetModule1();
        modules[1] = GameObject.Find("Game").GetComponent<Settings>().GetModule2();
        modules[2] = GameObject.Find("Game").GetComponent<Settings>().GetModule3();
        modules[3] = GameObject.Find("Game").GetComponent<Settings>().GetModule4();
        for(int i = 0; i < 1; i++)
            GameObject.Find(modules[i]).SetActive(false);
        escapeDoor.DoorMovable(false);
        puzzleDoor.DoorMovable(false);
    }

    /*
     * Triggers the supplied door's animation, if it is not already running.
     * If it is closed, the door is opened. If it is opened, the door is closed.
     */
    public void TriggerDoorAnimation(Door door)
    {
        if (door.GetRunning() == false)
            StartCoroutine(door.Open());
    }

    /*
     * Create a GUI style for the text display box and return this object.
     */
    public GUIStyle GetStandardBoxStyle()
    {
        GUIStyle boxStyle = new GUIStyle(GUI.skin.button);
        Font font = (Font)Resources.Load("Fonts/comic", typeof(Font));
        boxStyle.font = font;
        boxStyle.fontSize = 20;
        return boxStyle;
    }

}