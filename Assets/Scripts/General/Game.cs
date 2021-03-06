﻿using UnityEngine;
using System;
using System.Text;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that contains crucial values and handles the general actions of the game.
 * Knows which modules are loaded into the game.
 */

public class Game : MonoBehaviour {

    private static System.Random random = new System.Random();
    private bool[] roomsStatus = new bool[4];
    private int totalMinutes = 60;

    private DateTime startTime, endTime, currentTime;
    private TimeSpan finishTime, timeLeft;
    private string go, password, scrambledPassword;
    private bool started, completed, escaped;
    private double unlockAmount, unlockedLettersAmount;

    public GameObject roomSelector, escapeDoorIndicatorGlass, DataSender;
    public Camera initialCamera, playerCamera, PcCamera;
    public Light escapeDoorIndicatorLight;
    public Door escapeDoor, puzzleDoor;
    public TimeSpan penalty = TimeSpan.FromSeconds(30), totalPenalty;

    /*
     * Initializes the game by setting all the necessary values.
     */
    public void Start()
    {
        SetStarted(false);
        SetCompleted(false);
        SetEscaped(false);
        SetRoomStatus(0, false);
        SetRoomStatus(1, false);
        SetRoomStatus(2, false);
        SetRoomStatus(3, false);
        escapeDoor.DoorMovable(false);
        password = GetComponent<Settings>().GetPassword();
        totalMinutes = GetComponent<Settings>().GetTotalMinutes();
        go = GetComponent<Settings>().go;
        SwitchToInitialCamera();
        PreparePasswordDistribution();
    }

    /*
     * Executes the specified methods on every frame update.
     */
    public void Update()
    {
        CursorStatus();
        IsGameCompleted();
    }

    /*
     * Hides or shows the cursor during the game, determined by certain conditions.
     */
    private void CursorStatus()
    {
        if (started && playerCamera.gameObject.activeSelf == true)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    /*
     * Sets the completed boolean to true if all rooms have been completed.
     */
    private void IsGameCompleted()
    {
        if (roomsStatus[0] && roomsStatus[1] && roomsStatus[2] && roomsStatus[3])
        {
            completed = true;
        }
    }

    /*
     * Prepares the password to be distibuted to the team in fragments.
     * For every room that the team completes, a part of the scrambled password is shown.
     * The amount of visible letters is determined by the unlockedLetterAmount value.
     */
    public void PreparePasswordDistribution()
    {
        scrambledPassword = ScramblePassword(password);
        unlockAmount = scrambledPassword.Length / 4.0;
        unlockedLettersAmount = 0;
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
            Char temporary = scrambledString[index1];
            scrambledString[index1] = scrambledString[index2];
            scrambledString[index2] = temporary;
        }
        return scrambledString.ToString();
    }

    /*
     * Handles all GUI displays and messages. Displays the countdown time to the user. 
     * If the user escapes within the given time,  the countdown is stopped. 
     * Also shows the password display where all the letters are shown that the team unlocked.
     */
    public void OnGUI()
    {
        if(started)
        {
            currentTime = DateTime.Now;
            if (!escaped)
            {
                timeLeft = endTime - currentTime - totalPenalty;
            }
            else
            {
                timeLeft = finishTime - totalPenalty;
            }
            if((currentTime - startTime).Seconds < 5 && currentTime.Minute.Equals(startTime.Minute))
            {
                GUI.Label(new Rect((Screen.width / 2) - 80, (Screen.height / 2) - 100, 200, 100), go, GetStyle(80, GUIType.Label));
            }
            DrawCountdownGUI(timeLeft);
            DrawPasswordGUI();
        }
    }

    /*
     * Draws the amount of time left on the clock. In the final minute the timer is red. 
     * If the user completes the escape room the time is displayed in green.
     */
    private void DrawCountdownGUI(TimeSpan timeLeft)
    {
        GUI.color = Color.red;
        string message = string.Format("{0:D2}", timeLeft.Hours) + ":" + string.Format("{0:D2}", timeLeft.Minutes) + 
            ":" + string.Format("{0:D2}", timeLeft.Seconds);
        if (escaped)
        {
            GUI.color = Color.green;
        }
        else if (timeLeft.Ticks > 0 && timeLeft.Minutes > 0)
        {
            GUI.color = Color.white;
        }
        else if (timeLeft.Ticks <= 0)
        {
            GUI.color = Color.red;
            message = "00:00:00";
        }
        GUI.Box(new Rect(((Screen.width / 2) - 60), 10, 180, 60), message, GetStandardBoxStyle(40));
    }

    /*
     * Draws the amount of unlocked password letters on the screen. The display string is created by
     * checking the amount of unlocked letters. The rest is filled with underscores.
     */
    private void DrawPasswordGUI()
    {
        string message = "Password letters:\n";
        int unlockedLetters = (int)Math.Floor(unlockedLettersAmount);
        for(int i = 0; i < unlockedLetters; i++)
        {
            message = message + scrambledPassword[i] + " ";
        }
        int amountLeft = scrambledPassword.Length - unlockedLetters;
        for(int i = 0; i < amountLeft; i++)
        {
            message = message + "_ ";
        }
        GUI.color = Color.white;
        GUI.Box(new Rect(Screen.width - 260, 10, 400, 120), message, GetStyle(30, GUIType.Label));
    }

    /*
     * Updates the unlockedLettersAmount so that more letters become uncovered.
     */
    public void UpdateAmountOfUnlockedLetters()
    {
        double newAmount = unlockedLettersAmount + unlockAmount;
        if (newAmount > scrambledPassword.Length)
        {
            unlockedLettersAmount = scrambledPassword.Length;
        }
        else
        {
            unlockedLettersAmount = unlockedLettersAmount + unlockAmount;
        }
    }

    /*
     * Fetches the current time, and stores the start time and maximum end time.
     */
    public void SetTime()
    {
        startTime = DateTime.Now;
        GetComponent<Settings>().SetStartTime(startTime.ToString());
        endTime = startTime.AddMinutes(totalMinutes);
    }

    /*
     * Subtracts penalty time from the timer when the user makes an error.
     */
    public void SubtractTime()
    { 
        totalPenalty = totalPenalty + penalty;
    }

    /*
     * Activates the initial camera and deactivates the other cameras.
     */
    public void SwitchToInitialCamera()
    {
        initialCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        PcCamera.gameObject.SetActive(false);
    }

    /*
     * Activates the player camera and deactivates the other cameras.
     */
    public void SwitchToPlayerCamera()
    {
        initialCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        PcCamera.gameObject.SetActive(false);
    }

    /*
     * Activates the laptop camera and deactivates the other cameras.
     */
    public void SwitchToLaptopCamera()
    {
        PcCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        initialCamera.gameObject.SetActive(false);
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
        if (roomsStatus[roomIndicator])
        {
            ChangeLight(light, lamp, true);
        }
        else
        {
            ChangeLight(light, lamp, false);
        }
    }

    /*
     * Stores the finish time for the specified puzzle room.
     */
    public void SetRoomTime(int room)
    {
        currentTime = DateTime.Now;
        TimeSpan timeLeft = endTime - currentTime - totalPenalty;
        string message = string.Format("{0:D2}", timeLeft.Hours) + ":" + string.Format("{0:D2}", timeLeft.Minutes) + 
            ":" + string.Format("{0:D2}", timeLeft.Seconds);
        switch (room)
        {
            case 0:
                GetComponent<Settings>().SetModule1Time(message);
                break;
            case 1:
                GetComponent<Settings>().SetModule2Time(message);
                break;
            case 2:
                GetComponent<Settings>().SetModule3Time(message);
                break;
            case 3:
                GetComponent<Settings>().SetModule4Time(message);
                break;
            default:
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
        finishTime = endTime - currentTime - totalPenalty;
        string timeString = string.Format("{0:D2}", finishTime.Hours) + ":" + string.Format("{0:D2}", finishTime.Minutes) + 
            ":" + string.Format("{0:D2}", finishTime.Seconds);
        GetComponent<Settings>().SetFinishTime(timeString);
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
        DataSender.GetComponent<SendData>().POST();
    }

    /*
     * Triggers the supplied door's animation, if it is not already running.
     * If it is closed, the door is opened. If it is opened, the door is closed.
     */
    public void TriggerDoorAnimation(Door door)
    {
        if (door.GetRunning() == false)
        {
            StartCoroutine(door.Open());
        }
    }

    /*
     * Closes the supplied door if it is open.
     */
    public void CloseDoor(Door door)
    {
        if (door.GetComponent<Door>().GetOpened())
        {
            TriggerDoorAnimation(door);
        }
    }

    /*
     * Locks or unlocks the door, depending on the supplied boolean value.
     */
    public void LockDoor(Door door, bool lockDoor)
    {
        if (lockDoor)
        {
            door.GetComponent<Door>().DoorMovable(false);
        }
        else
        {
            door.GetComponent<Door>().DoorMovable(true);
        }
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
     * Fetches the stored room status boolean of the specified room.
     */
    public bool GetRoomStatus(int room)
    {
        return roomsStatus[room];
    }

    /*
     * Stores the supplied room status boolean of the specified room.
     */
    public void SetRoomStatus(int room, bool completed)
    {
        roomsStatus[room] = completed;
    }

    /*
     * Fetches the stored started boolean.
     */
    public bool GetStarted()
    {
        return started;
    }

    /*
     * Stores the supplied started boolean.
     */
    public void SetStarted(bool started)
    {
        this.started = started;
    }

    /*
     * Fetches the stored completed boolean.
     */
    public bool GetCompleted()
    {
        return completed;
    }

    /*
     * Stores the supplied completed boolean.
     */
    public void SetCompleted(bool completed)
    {
        this.completed = completed;
    }

    /*
     * Fetches the stored escaped boolean.
     */
    public bool GetEscaped()
    {
        return escaped;
    }

    /*
     * Stores the supplied escaped boolean.
     */
    public void SetEscaped(bool escaped)
    {
        this.escaped = escaped;
    }

    public enum GUIType
    {
        Window,
        Button,
        Label
    };

}