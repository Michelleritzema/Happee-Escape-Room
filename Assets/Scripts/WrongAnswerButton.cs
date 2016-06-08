using UnityEngine;
using System.Collections;
using System;

public class WrongAnswerButton : MonoBehaviour {

    public Game game;
    public GameObject button;
    public TimeSpan timeReduce;
    private int room;

    /*
     * Changes the button's texture to active.
     */
 
    /*
     * Changes the button's texture to clicked.
     * Sets the room status to completed and updates the unlocked password letters;
     */
 
    public void ClickOnObject()
    {
        game.GetComponent<Game>().SubtractTime();
        button.SetActive(false);
    }

}
