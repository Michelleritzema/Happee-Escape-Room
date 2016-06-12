using UnityEngine;

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

    /*
     * Changes the button's texture to clicked.
     * The active room is changed, according to which button has been pressed.
     */
    public void ClickOnObject()
    {
        button.GetComponent<Renderer>().material = clicked;
        if(moveRight)
        {
            roomSelector.GetComponent<RoomSelector>().ChangeRooms(true);
        }
       else
        {
            roomSelector.GetComponent<RoomSelector>().ChangeRooms(false);
        }
    }

}