﻿using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the activation and deactivation of all the puzzle rooms.
 */

public class RoomSelector : MonoBehaviour {

    private GameObject[] rooms = new GameObject[4];
    private Texture logo1, logo2, logo3, logo4;
    private string tagHideableRoom = "HideableRoom";
    private int roomIndicator = 0;

    public Game game;
    public GameObject displayScreen, lightbulb;
    public new Light light;
    public Door puzzleDoor;

	/*
     * Stores all the modules that have been specified in the settings.
     * Also fetches all the room logo textures to use on the display.
     */
	public void Start () {
        InitializeRoom1();
        InitializeRoom2();
        InitializeRoom3();
        InitializeRoom4();
        InitializeRoomFinishers();
        MakeRoomActive(roomIndicator);
    }

    /*
     * Updates the room indicator light.
     */
    public void Update()
    {
        game.GetComponent<Game>().SetRoomIndicatorLight(light, lightbulb, roomIndicator);
    }

    /*
     * Initializes room 1.
     */
    private void InitializeRoom1()
    {
        string room1 = game.GetComponent<Settings>().GetModule1();
        rooms[0] = GameObject.Find(room1);
        string logo1Texture = "Logos/" + room1;
        logo1 = Resources.Load<Texture>(logo1Texture);
    }

    /*
     * Initializes room 2.
     */
    private void InitializeRoom2()
    {
        string room2 = game.GetComponent<Settings>().GetModule2();
        rooms[1] = GameObject.Find(room2);
        string logo2Texture = "Logos/" + room2;
        logo2 = Resources.Load<Texture>(logo2Texture);
    }

    /*
     * Initializes room 3.
     */
    private void InitializeRoom3()
    {
        string room3 = game.GetComponent<Settings>().GetModule3();
        rooms[2] = GameObject.Find(room3);
        string logo3Texture = "Logos/" + room3;
        logo3 = Resources.Load<Texture>(logo3Texture);
    }

    /*
     * Initializes room 4.
     */
    private void InitializeRoom4()
    {
        string room4 = game.GetComponent<Settings>().GetModule4();
        rooms[3] = GameObject.Find(room4);
        string logo4Texture = "Logos/" + room4;
        logo4 = Resources.Load<Texture>(logo4Texture);
    }

    /*
     * Initializes the room finishers.
     */
    private void InitializeRoomFinishers()
    {
        for (int i = 0; i < 4; i++)
        {
            if (game.GetComponent<Settings>().GetModule(i).Equals("ModuleMedicalResearch"))
            {
                rooms[i].transform.Find("AnswerButtons").transform.Find("Answer 3").GetComponent<CompletionButtonActions>().SetRoom(i);
            }
            else
            {
                rooms[i].transform.Find("DoneButton").transform.Find("Button").GetComponent<CompletionButtonActions>().SetRoom(i);
            }
        }
    }

    /*
     * Changes the room selector screen to display the correct room logo.
     */
    private void ChangeDisplay()
    {
        switch(roomIndicator)
        {
            case 0:
                displayScreen.GetComponent<Renderer>().material.mainTexture = logo1;
                break;
            case 1:
                displayScreen.GetComponent<Renderer>().material.mainTexture = logo2;
                break;
            case 2:
                displayScreen.GetComponent<Renderer>().material.mainTexture = logo3;
                break;
            case 3:
                displayScreen.GetComponent<Renderer>().material.mainTexture = logo4;
                break;
            default:
                displayScreen.GetComponent<Renderer>().material.mainTexture = logo1;
                break;
        }
    }

    /*
     * Starts the room changing method, if the puzzledoor is not currently doing an animation.
     */
    public void ChangeRooms(bool up)
    {
        if(!puzzleDoor.GetComponent<Door>().GetRunning())
        {
            StartCoroutine(CheckDoorClosed(up));
        }  
    }

    /*
     * Closes and locks the puzzledoor, and waits for the animation to finish. If the door is closed, 
     * the room behind it gets changed. What room depends on whether the user has pressed the up or the down key. 
     * If the room has been changed, the door is unlocked again.
     */
    IEnumerator CheckDoorClosed(bool up)
    {
        game.GetComponent<Game>().CloseDoor(puzzleDoor);
        game.GetComponent<Game>().LockDoor(puzzleDoor, true);
        while (puzzleDoor.GetComponent<Door>().GetRunning())
            yield return new WaitForSeconds(2);
        if (up)
        {
            MoveRoomUp();
        }
        else
        {
            MoveRoomDown();
        }
        game.GetComponent<Game>().LockDoor(puzzleDoor, false);
    }

    /*
     * Activates the room that is stored above the one currently active.
     */
    public void MoveRoomUp()
    {
        if (roomIndicator == 3)
        {
            roomIndicator = 0;
        }
        else
        {
            roomIndicator++;
        }
        MakeRoomActive(roomIndicator);
    }

    /*
     * Activates the room that is stored below the one currently active.
     */
    public void MoveRoomDown()
    {
        if (roomIndicator == 0)
        {
            roomIndicator = 3;
        } 
        else
        {
            roomIndicator--;
        }
        MakeRoomActive(roomIndicator);
    }

    /*
     * Disables all the puzzle rooms except for the one which number is supplied.
     * Also changes the room indicator screen to display the correct room logo.
     */
    private void MakeRoomActive(int roomNumber)
    {
        HideAllRooms();
        rooms[roomNumber].SetActive(true);
        ChangeDisplay();
    }

    /*
     * Hides all the hideable rooms, the game objects that have the tag 'HideableRoom'.
     */
    public void HideAllRooms()
    {
        foreach (Transform child in GameObject.Find("Game").transform)
        {
            GameObject childObject = child.gameObject;
            if (childObject.tag == tagHideableRoom)
            {
                childObject.SetActive(false);
            } 
        }
    }

    /*
     * Fetches the room indicator integer.
     */
    public int GetRoomIndicator()
    {
        return roomIndicator;
    }

}