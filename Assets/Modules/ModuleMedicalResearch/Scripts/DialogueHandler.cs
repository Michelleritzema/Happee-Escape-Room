using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {
    public Text text;
    public bool diaDone= false;

    public void UpdateText()
    {
        var dialoguePatient = GameObject.Find("DialoguePatient").GetComponent<Text>();
       
        Text t = GetComponentInChildren<Text>();
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => {
            dialoguePatient.text = "Ik voel mij niet zo goed..";
            t.text = "Dat is niet zo fijn waar heeft u last van?";
            diaDone = true;
        });
    }

}