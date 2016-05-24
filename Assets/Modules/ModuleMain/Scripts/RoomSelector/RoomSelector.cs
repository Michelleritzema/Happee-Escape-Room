using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the activation and deactivation of all the puzzle rooms.
 */

public class RoomSelector : MonoBehaviour {

    private GameObject[] rooms = new GameObject[4];
    private int roomIndicator = 0;

    public Game game;

	/*
     * Stores all the modules that have been specified in the settings.
     */
	public void Start () {
	    rooms[0] = GameObject.Find(game.GetComponent<Settings>().GetModule1());
        rooms[1] = GameObject.Find(game.GetComponent<Settings>().GetModule2());
        rooms[2] = GameObject.Find(game.GetComponent<Settings>().GetModule3());
        rooms[3] = GameObject.Find(game.GetComponent<Settings>().GetModule4());
        MakeRoomActive(roomIndicator);
    }

    /*
     * Activates the room that is stored above the one currently active.
     */
    public void MoveRoomUp()
    {
        if (roomIndicator == 3)
            roomIndicator = 0;
        else
            roomIndicator++;
        MakeRoomActive(roomIndicator);
    }

    /*
     * Activates the room that is stored below the one currently active.
     */
    public void MoveRoomDown()
    {
        if (roomIndicator == 0)
            roomIndicator = 3;
        else
            roomIndicator--;
        MakeRoomActive(roomIndicator);
    }

    /*
     * Disables all the puzzle rooms except for the one which number is supplied.
     */
    private void MakeRoomActive(int roomNumber)
    {
        HideAllRooms();
        rooms[roomNumber].SetActive(true);
    }

    /*
     * Hides all the rooms.
     */
    public void HideAllRooms()
    {
        rooms[0].SetActive(false);
        rooms[1].SetActive(false);
        rooms[2].SetActive(false);
        rooms[3].SetActive(false);
    }

}