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
     * Changes the button's texture to active.
     */
    public void SetToActive()
    {
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture to clicked.
     * Sets the room status to completed and updates the unlocked password letters;
     */
    public void ClickOnObject()
    {
        button.GetComponent<Renderer>().material = clicked;
        game.GetComponent<Game>().SetRoomStatus(room, true);
        game.GetComponent<Game>().updateAmountOfUnlockedLetters();
        button.SetActive(false);
    }

    /*
     * Changes the button's texture to inactive.
     */
    public void SetToInactive()
    {
        button.GetComponent<Renderer>().material = inactive;
    }

    /*
     * Stores the supplied room integer;
     */
    public void SetRoom(int room)
    {
        this.room = room;
    }

}