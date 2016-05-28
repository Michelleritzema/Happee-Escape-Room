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
     * Changes the button's texture to active.
     */
    public void SetToActive()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = active;
    }

    /*
     * Changes the button's texture to inactive.
     */
    public void SetToInactive()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = inactive;
    }

    /*
     * Changes the button's texture to clicked and checks the password.
     */
    public void ClickOnObject()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = clicked;
        passwordPanel.GetComponent<PasswordTerminal>().CheckPassword();
    }

}