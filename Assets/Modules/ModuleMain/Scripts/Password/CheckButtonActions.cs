using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the actions of a password panel check button.
 * The password terminal script is used to check the submitted password.
 */

public class CheckButtonActions : MonoBehaviour {

    public GameObject passwordPanel, checkButton;
    public Texture active, inactive, clicked;

    /*
     * Changes the button's texture when the mouse hovers over the button.
     */
    public void OnMouseEnter()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = active;
    }

    /*
     * Changes the button's texture when the mouse presses the button.
     */
    public void OnMouseDown()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = clicked;
        passwordPanel.GetComponent<PasswordTerminal>().CheckPassword();
    }

    /*
     * Changes the button's texture when the mouse is done pressing the button.
     */
    public void OnMouseUp()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = active;
    }

    /*
     * Changes the button's texture when the mouse no longer hovers over the button.
     */
    public void OnMouseExit()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = inactive;
    }

}