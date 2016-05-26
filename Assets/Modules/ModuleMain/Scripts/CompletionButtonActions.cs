using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the button actions of the room completion button.
 */

public class CompletionButtonActions : MonoBehaviour
{

    public Game game;
    public GameObject button;
    public Material active, inactive, clicked;

    private int room;

    /*
     * Changes the button's texture when the mouse hovers over the button.
     */
    public void OnMouseEnter()
    {
        Debug.Log("On mouse enter");
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture when the mouse presses the button.
     * Sets the room status to completed and updates the unlocked password letters;
     */
    public void OnMouseDown()
    {
        Debug.Log("On mouse down");
        button.GetComponent<Renderer>().material = clicked;
        game.GetComponent<Game>().SetRoomStatus(room, true);
        game.GetComponent<Game>().updateAmountOfUnlockedLetters();
        button.SetActive(false);
    }

    /*
     * Changes the button's texture when the mouse no longer hovers over the button.
     */
    public void OnMouseExit()
    {
        Debug.Log("On mouse exit");
        button.GetComponent<Renderer>().material = inactive;
    }

    /*
     * Stores the supplied room integer;
     */
    public void SetRoom(int room)
    {
        this.room = room;
        Debug.Log("Setting room integer: " + room);
    }

}