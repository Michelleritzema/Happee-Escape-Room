using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the general actions of the game.
 * Also knows which modules are loaded into the game.
 */

public class Game : MonoBehaviour {

    private static System.Random random = new System.Random();
    private string[] modules = new string[4];
    private bool[] roomsStatus = new bool[4];
    private int totalMinutes = 60;

    private DateTime startTime, endTime, currentTime;
    private TimeSpan finishTime;
    private string go, password, scrambledPassword;
    private bool started, escaped, room1Completed, room2Completed, room3Completed, room4Completed;
    private double unlockAmount, unlockedLettersAmount;

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
        SetStarted(false);
        setRoom1Completed(false);
        setRoom2Completed(false);
        setRoom3Completed(false);
        setRoom4Completed(false);
        escapeDoor.DoorMovable(false);
        SetRoomStatus(1, false);
        SetRoomStatus(2, false);
        SetRoomStatus(3, false);
        SetRoomStatus(4, false);
        password = GetComponent<Settings>().GetPassword();
        totalMinutes = GetComponent<Settings>().GetTotalMinutes();
        Debug.Log("Game duration: " + totalMinutes + " minutes");
        PreparePasswordDistribution();
        go = GetComponent<Settings>().go;
    }

    /*
     * Stores the room status of the specified room.
     */
    public void SetRoomStatus(int room, bool completed)
    {
        roomsStatus[room] = completed;
    }

    /*
     * Prepares the password to be distibuted to the team in fragments.
     * For every room that the team completes, a part of the scrambled password is shown.
     * The amount of visible letters is determined by the unlockedLetterAmount value.
     */
    public void PreparePasswordDistribution()
    {
        scrambledPassword = ScramblePassword(password);
        Debug.Log("Scrambled password: " + scrambledPassword);
        unlockAmount = scrambledPassword.Length / 4.0;
        Debug.Log("Unlock letters amount: " + unlockAmount);
        unlockedLettersAmount = 0;
    }

    /*
     * Displays the countdown time to the user. If the user escapes within the given time, 
     * the countdown is stopped. Also shows the password display where all the letters are shown 
     * that the team unlocked.
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
            DrawPasswordGUI();
        }
    }

    /*
     * Draws the amount of unlocked password letters on the screen. The display string is created by
     * checking the amount of unlocked letters. The rest if filled with underscores.
     */
    private void DrawPasswordGUI()
    {
        string message = "Password letters:\n";
        int unlockedLetters = (int)Math.Floor(unlockedLettersAmount);
        for(int i = 0; i < unlockedLetters; i++)
            message = message + scrambledPassword[i] + " ";
        int amountLeft = scrambledPassword.Length - unlockedLetters;
        for(int i = 0; i < amountLeft; i++)
            message = message + "_ ";
        GUI.Box(new Rect(Screen.width - 260, 10, 400, 120), message, GetStyle(30, GUIType.Label));
    }

    /*
     * Calculates the amount of unlocked letters by 
     */
    /*public int GetAmountOfUnlockedLetters()
    {
        double unlocked = unlockedLettersAmount;
        int letterAmount = 0;
        while (unlocked >= 1)
        {
            letterAmount++;
            unlocked = unlocked - 1;
        }
        return letterAmount;
    }*/

    public void updateAmountOfUnlockedLetters()
    {
        double newAmount = unlockedLettersAmount + unlockAmount;
        if (newAmount > scrambledPassword.Length)
            unlockedLettersAmount = scrambledPassword.Length;
        else
            unlockedLettersAmount = unlockedLettersAmount + unlockAmount;
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
     * Changes the supplied light's color, depending on the open boolean value.
     */
    public void ChangeLight(Light light, GameObject lamp, bool open)
    {
        if(open)
        {
            light.color = Color.green;
            lamp.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            light.color = Color.red;
            lamp.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    /*
     * Determines the color of the room indicator depending on whether or not the room has been completed.
     */
    public void SetRoomIndicatorLight(Light light, GameObject lamp, int roomIndicator)
    {
        switch(roomIndicator)
        {
            case 0:
                if (room1Completed)
                    ChangeLight(light, lamp, true);
                else
                    ChangeLight(light, lamp, false);
                break;
            case 1:
                if (room2Completed)
                    ChangeLight(light, lamp, true);
                else
                    ChangeLight(light, lamp, false);
                break;
            case 2:
                if (room3Completed)
                    ChangeLight(light, lamp, true);
                else
                    ChangeLight(light, lamp, false);
                break;
            case 3:
                if (room4Completed)
                    ChangeLight(light, lamp, true);
                else
                    ChangeLight(light, lamp, false);
                break;
            default:
                if (room1Completed)
                    ChangeLight(light, lamp, true);
                else
                    ChangeLight(light, lamp, false);
                break;
        }
    }

    /*
     * Triggers when the user entered the correct password.
     * The escape door is opened and the indicator light changes.
     */
    public void Escaping()
    {
        escaped = true;
        finishTime = endTime - currentTime;
        ChangeLight(escapeDoorIndicatorLight, escapeDoorIndicatorGlass, true);
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
     * Scrambles the supplied password and returns the new string.
     */
    public string ScramblePassword(string password)
    {
        StringBuilder scrambledString = new StringBuilder();
        scrambledString.Append(password);
        int stringLength = scrambledString.Length;
        for (int i = 0; i < stringLength; ++i)
        {
            int index1 = (random.Next() % stringLength);
            int index2 = (random.Next() % stringLength);
            Char temp = scrambledString[index1];
            scrambledString[index1] = scrambledString[index2];
            scrambledString[index2] = temp;
        }
        return scrambledString.ToString();
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

    /*
     * Fetches the stored get room 1 completed boolean.
     */
    public bool getRoom1Completed()
    {
        return room1Completed;
    }

    /*
     * Stores the get room 1 completed boolean.
     */
    public void setRoom1Completed(bool room1Completed)
    {
        this.room1Completed = room1Completed;
    }

    /*
     * Fetches the stored get room 2 completed boolean.
     */
    public bool getRoom2Completed()
    {
        return room2Completed;
    }

    /*
     * Stores the get room 2 completed boolean.
     */
    public void setRoom2Completed(bool room2Completed)
    {
        this.room2Completed = room2Completed;
    }

    /*
     * Fetches the stored get room 3 completed boolean.
     */
    public bool getRoom3Completed()
    {
        return room3Completed;
    }

    /*
     * Stores the get room 3 completed boolean.
     */
    public void setRoom3Completed(bool room3Completed)
    {
        this.room3Completed = room3Completed;
    }

    /*
     * Fetches the stored get room 4 completed boolean.
     */
    public bool getRoom4Completed()
    {
        return room4Completed;
    }

    /*
     * Stores the get room 4 completed boolean.
     */
    public void setRoom4Completed(bool room4Completed)
    {
        this.room4Completed = room4Completed;
    }

}