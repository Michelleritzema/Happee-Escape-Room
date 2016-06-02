using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    public string[] PatientText;
    public string[] Questions;
    bool DisplayText = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onGUI()
    {
        GUILayout.BeginArea(new Rect(700, 600, 400, 400));
        if (DisplayText)
        {
            GUILayout.Label(PatientText[0]);
            GUILayout.Button(Questions[0]);
            GUILayout.Label(PatientText[1]);

        }
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
