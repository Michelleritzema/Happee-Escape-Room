using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the button actions of the pc button.
 */

public class PcButton : MonoBehaviour
{

    public GameObject button;
    public Material active, inactive, clicked;
    public Game game;
    public bool laptopDone = false;
    
    /*
     * Changes the button's texture to clicked.
     * Switches the active camera to the laptopscreen
     */
    public void ClickOnObject()
    {
        button.GetComponent<Renderer>().material = clicked;
        game.GetComponent<Game>().SwitchToLaptopCamera();
        laptopDone = true;
    }

    /*
     * Changes the button's texture to active.
     */
    public void SetToActive()
    {
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture to inactive.
     */
    public void SetToInactive()
    {
        button.GetComponent<Renderer>().material = inactive;
    }

}