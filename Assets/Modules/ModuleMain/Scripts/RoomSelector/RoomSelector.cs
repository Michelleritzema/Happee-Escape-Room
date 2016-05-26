using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the activation and deactivation of all the puzzle rooms.
 */

public class RoomSelector : MonoBehaviour {

    private GameObject[] rooms = new GameObject[4];
    private Texture logo1, logo2, logo3, logo4;
    private int roomIndicator = 0;

    public Game game;
    public GameObject displayScreen, lightbulb;
    public Light light;
    public Door puzzleDoor;

	/*
     * Stores all the modules that have been specified in the settings.
     * Also fetches all the room logo textures to use on the display.
     */
	public void Start () {
        string room1 = game.GetComponent<Settings>().GetModule1();
        string room2 = game.GetComponent<Settings>().GetModule2();
        string room3 = game.GetComponent<Settings>().GetModule3();
        string room4 = game.GetComponent<Settings>().GetModule4();
        string logo1Texture = "Assets/Modules/" + room1 + "/Logo/" + room1 + ".png";
        string logo2Texture = "Assets/Modules/" + room2 + "/Logo/" + room2 + ".png";
        string logo3Texture = "Assets/Modules/" + room3 + "/Logo/" + room3 + ".png";
        string logo4Texture = "Assets/Modules/" + room4 + "/Logo/" + room4 + ".png";
        rooms[0] = GameObject.Find(room1);
        rooms[1] = GameObject.Find(room2);
        rooms[2] = GameObject.Find(room3);
        rooms[3] = GameObject.Find(room4);
        rooms[0].transform.Find("DoneButton").transform.Find("Button").GetComponent<CompletionButtonActions>().SetRoom(0);
        rooms[1].transform.Find("DoneButton").transform.Find("Button").GetComponent<CompletionButtonActions>().SetRoom(1);
        rooms[2].transform.Find("DoneButton").transform.Find("Button").GetComponent<CompletionButtonActions>().SetRoom(2);
        rooms[3].transform.Find("DoneButton").transform.Find("Button").GetComponent<CompletionButtonActions>().SetRoom(3);
        logo1 = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath(logo1Texture, typeof(Texture));
        logo2 = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath(logo2Texture, typeof(Texture));
        logo3 = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath(logo3Texture, typeof(Texture));
        logo4 = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath(logo4Texture, typeof(Texture));
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
        //game.GetComponent<Game>().SetRoomIndicatorLight(light, lightbulb, roomIndicator);
    }

    /*
     * Starts the room changing method, if the puzzledoor is not currently doing an animation.
     */
    public void ChangeRooms(bool up)
    {
        if(!puzzleDoor.GetComponent<Door>().GetRunning())
            StartCoroutine(CheckDoorClosed(up));
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
            MoveRoomUp();
        else MoveRoomDown();
        game.GetComponent<Game>().LockDoor(puzzleDoor, false);
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
     * Also changes the room indicator screen to display the correct room logo.
     */
    private void MakeRoomActive(int roomNumber)
    {
        HideAllRooms();
        rooms[roomNumber].SetActive(true);
        ChangeDisplay();
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