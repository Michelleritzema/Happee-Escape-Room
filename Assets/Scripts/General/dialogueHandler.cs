using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dialogueHandler : MonoBehaviour {
    public Text text;
    public 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void UpdateText()
    {
        var dialoguePatient = GameObject.Find("DialoguePatient").GetComponent<Text>();
       
        Text t = GetComponentInChildren<Text>();
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => {
            dialoguePatient.text = "Ik voel mij niet zo goed..";
            t.text = "Dat is niet zo fijn waar heeft u last van?";
        });
    }
}
