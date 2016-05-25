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
    private int totalMinutes = 60;

    private DateTime startTime, endTime, currentTime;
    private TimeSpan finishTime;
    private string go;
    private bool started, escaped;

    public GameObject roomSelector, escapeDoorIndicatorGlass;
    public Camera initialCamera, playerCamera;
    public Light escapeDoorIndicatorLight;
    public Door escapeDoor, puzzleDoor;

    public enum GUIType
    {
        Window,
        Button,
        Label
    };

    /*
     * Initializes the game.
     */
    public void Start()
    {
        SwitchToInitialCamera();
        escaped = false;
        started = false;
        escapeDoor.DoorMovable(false);
        totalMinutes = GetComponent<Settings>().GetTotalMinutes();
        Debug.Log("Game duration: " + totalMinutes + " minutes");
        go = GetComponent<Settings>().go;
    }

    /*
     * Displays the countdown time to the user. If the user escapes within the given time, 
     * the countdown is stopped.
     */
    public void OnGUI()
    {
        if(started)
        {
            currentTime = DateTime.Now;
            TimeSpan timeLeft;
            if (!escaped)
                timeLeft = endTime - currentTime;
            else
                timeLeft = finishTime;
            if((currentTime - startTime).Seconds < 5 && currentTime.Minute.Equals(startTime.Minute))
                GUI.Label(new Rect((Screen.width / 2) - 80, (Screen.height / 2) - 100, 200, 100), go, GetStyle(80, GUIType.Label));
            int horizontalPosition = (Screen.width / 2) - 60;
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
    }

    /*
     * Fetches the current time, and stores the start time and suspected end time.
     */
    public void SetTime()
    {
        startTime = DateTime.Now;
        endTime = startTime.AddMinutes(totalMinutes);
        Debug.Log("Start time: " + startTime);
        Debug.Log("End time: " + endTime);
    }

    /*
     * Activates the initial camera and deactivates the player camera.
     */
    public void SwitchToInitialCamera()
    {
        initialCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
    }

    /*
     * Activates the player camera and deactivates the initial camera.
     */
    public void SwitchToPlayerCamera()
    {
        initialCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
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
        CloseDoor(escapeDoor);
        CloseDoor(puzzleDoor);
        roomSelector.GetComponent<RoomSelector>().HideAllRooms();
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
     * Closes the supplied door if it is open.
     */
    public void CloseDoor(Door door)
    {
        if (door.GetComponent<Door>().GetOpened())
            TriggerDoorAnimation(door);
    }

    /*
     * Locks or unlocks the door, depending on the supplied boolean value.
     */
    public void LockDoor(Door door, bool lockDoor)
    {
        if (lockDoor)
            door.GetComponent<Door>().DoorMovable(false);
        else
            door.GetComponent<Door>().DoorMovable(true);
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

    /*
     * Create a GUI style for the supplied GUI type and return this style object.
     */
    public GUIStyle GetStyle(int fontSize, GUIType type)
    {
        GUIStyle style;
        switch (type)
        {
            case GUIType.Window:
                style = new GUIStyle(GUI.skin.window);
                break;
            case GUIType.Button:
                style = new GUIStyle(GUI.skin.button);
                break;
            case GUIType.Label:
                style = new GUIStyle(GUI.skin.label);
                break;
            default:
                style = new GUIStyle();
                break;
        }
        Font font = (Font)Resources.Load("Fonts/comic", typeof(Font));
        style.font = font;
        style.fontSize = fontSize;
        return style;
    }

    /*
     * Fetches the stored get started boolean.
     */
    public bool GetStarted()
    {
        return started;
    }

    /*
     * Stores the get started boolean.
     */
    public void SetStarted(bool started)
    {
        this.started = started;
    }

}