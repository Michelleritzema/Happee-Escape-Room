using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the button actions of the room selector panel.
 */

public class RoomButtons : MonoBehaviour
{

    public GameObject roomSelector, button;
    public Material active, inactive, clicked;
    public bool moveRight;

    /*
     * Changes the button's texture when the mouse hovers over the button.
     */
    public void OnMouseEnter()
    {
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture when the mouse presses the button.
     * The active room is changed, according to which button has been pressed.
     */
    public void OnMouseDown()
    {
        button.GetComponent<Renderer>().material = clicked;
        if(moveRight)
            roomSelector.GetComponent<RoomSelector>().MoveRoomUp();
       else
            roomSelector.GetComponent<RoomSelector>().MoveRoomDown();
    }

    /*
     * Changes the button's texture when the mouse is done pressing the button.
     */
    public void OnMouseUp()
    {
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture when the mouse no longer hovers over the button.
     */
    public void OnMouseExit()
    {
        button.GetComponent<Renderer>().material = inactive;
    }

}