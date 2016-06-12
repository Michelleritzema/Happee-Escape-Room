using UnityEngine;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the actions of a password panel button.
 * The up boolean determines which method is called to change the corresponding letter panel.
 */

public class ButtonActions : MonoBehaviour {

    private Color normalColour, focusColour, clickedColour;

    public GameObject letterPanel, button;
    public bool up;

    /*
     * Initializes the button by instantiating the possible colours and setting its current colour.
     */
    public void Start () {
        normalColour = new Color32(10, 10, 10, 1);
        focusColour = new Color32(75, 75, 75, 1);
        clickedColour = new Color32(200, 200, 200, 1);
        button.GetComponent<Renderer>().material.color = normalColour;
    }

    /*
     * Changes the button's colour to inactive.
     */
    public void SetToInactive()
    {
        button.GetComponent<Renderer>().material.color = normalColour;
    }

    /*
     * Changes the button's colour to active.
     */
    public void SetToActive()
    {
        button.GetComponent<Renderer>().material.color = focusColour;
    }

    /*
     * Changes the button's colour to clicked. Also calls the corresponding letter panel's 
     * letter update method, depending on whether the up boolean is true or false. 
     */
    public void ClickOnObject()
    {
        button.GetComponent<Renderer>().material.color = clickedColour;
        if(up)
        {
            letterPanel.GetComponent<LetterChanger>().MoveUp();
        }
        else
        {
            letterPanel.GetComponent<LetterChanger>().MoveDown();
        }
    }

}