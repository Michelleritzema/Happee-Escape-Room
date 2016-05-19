using UnityEngine;
using System.Collections;

public class CheckPassword : MonoBehaviour {

    public GameObject passwordPanel, checkButton;
    public Texture active, inactive;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = active;
        passwordPanel.GetComponent<PasswordTerminal>().CheckPassword();
    }

    void OnMouseUp()
    {
        checkButton.GetComponent<Renderer>().material.mainTexture = inactive;
    }

}
