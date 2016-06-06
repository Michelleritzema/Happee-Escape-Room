using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the button actions of the room completion button.
 */

public class PcButton : MonoBehaviour
{
    public GameObject button;
    public Material active, inactive, clicked;
    public Game game;


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
        game.GetComponent<Game>().SwitchToLaptopCamera();
        button.SetActive(false);
    }

    /*
     * Changes the button's texture to inactive.
     */
    public void SetToInactive()
    {
        button.GetComponent<Renderer>().material = inactive;
    }

}