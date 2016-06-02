using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    public string[] PatientText;
    public string[] Questions;
    bool DisplayText = false;
	// Use this for initialization
	void Start () {
		onGUI ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 100, 100));
        GUILayout.Button("Click me");
        GUILayout.Button("Or me");
        GUILayout.EndArea();
    }

    void onTriggerEnter()
    {
        DisplayText = true;
    }

    void OnTriggerQuit()
    {
        DisplayText = false;
    }
}
