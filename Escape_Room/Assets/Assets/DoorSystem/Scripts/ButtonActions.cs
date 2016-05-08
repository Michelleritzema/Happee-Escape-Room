using UnityEngine;
using System.Collections;


public class ButtonActions : MonoBehaviour {

    private Color normalColour, focusColour;
    public GameObject letterPanel;
    public GameObject button;
    public bool up;

    void Start () {
        normalColour = new Color32(10, 10, 10, 1);
        focusColour = new Color32(75, 75, 75, 1);
        button.GetComponent<Renderer>().material.color = normalColour;
    }

    void OnMouseEnter()
    {
        button.GetComponent<Renderer>().material.color = focusColour;
    }

    void OnMouseDown()
    {
        button.GetComponent<Renderer>().material.color = Color.white;
        if(up)
            letterPanel.GetComponent<LetterChanger>().moveUp();
        else
            letterPanel.GetComponent<LetterChanger>().moveDown();
    }

    void OnMouseUp()
    {
        button.GetComponent<Renderer>().material.color = normalColour;
    }

    void OnMouseExit()
    {
        button.GetComponent<Renderer>().material.color = normalColour;
    }

}