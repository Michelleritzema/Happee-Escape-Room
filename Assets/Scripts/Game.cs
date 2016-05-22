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

    private DateTime startTime, endTime, currentTime;
    private TimeSpan finishTime;
    private bool escaped;

    public GameObject escapeDoorIndicatorGlass;
    public Light escapeDoorIndicatorLight;
    public Door escapeDoor, puzzleDoor;

    /*
     * Initializes the game.
     */
    public void Start()
    {
        escaped = false;
        escapeDoor.DoorMovable(false);
        startTime = DateTime.Now;
        endTime = startTime.AddMinutes(60);
        Debug.Log("start time: " + startTime);
        Debug.Log("end time: " + endTime);
        Debug.Log(startTime.Hour + ":" + startTime.Minute);
    }

    /*
     * Displays the countdown time to the user. If the user escapes within the given time, 
     * the countdown is stopped.
     */
    public void OnGUI()
    {
        currentTime = DateTime.Now;
        TimeSpan timeLeft;
        if (!escaped)
            timeLeft = endTime - currentTime;
        else
            timeLeft = finishTime;
        int horizontalPosition = (Screen.width / 2) - 150;
        string message = "";
        GUI.color = Color.white;
        if (timeLeft.Ticks > 0 && timeLeft.Minutes > 0)
        {
            message = string.Format("{0:D2}", timeLeft.Hours) + ":" + string.Format("{0:D2}", timeLeft.Minutes) +
                ":" + string.Format("{0:D2}", timeLeft.Seconds);
        }
        else if (timeLeft.Ticks > 0)
        {
            GUI.color = Color.red;
            message = string.Format("{0:D2}", timeLeft.Hours) + ":" + string.Format("{0:D2}", timeLeft.Minutes) +
                ":" + string.Format("{0:D2}", timeLeft.Seconds);
        }
        else
        {
            GUI.color = Color.red;
            message = "00:00:00";
        }
        if (escaped)
            GUI.color = Color.green;
        GUI.Box(new Rect(horizontalPosition, 10, 180, 60), message, GetStandardBoxStyle(40));
    }

    /*
     * Triggers when the user entered the correct password.
     * The escape door is opened and the indicator light changes.
     */
    public void Escaping()
    {
        escaped = true;
        finishTime = endTime - currentTime;
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
    public GUIStyle GetStandardBoxStyle(int fontSize)
    {
        GUIStyle boxStyle = new GUIStyle(GUI.skin.button);
        Font font = (Font)Resources.Load("Fonts/comic", typeof(Font));
        boxStyle.font = font;
        boxStyle.fontSize = fontSize;
        return boxStyle;
    }

}