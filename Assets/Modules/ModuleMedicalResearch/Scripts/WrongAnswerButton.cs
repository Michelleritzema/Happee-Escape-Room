using UnityEngine;
using System.Collections;
using System;

public class WrongAnswerButton : MonoBehaviour {

    public Game game;
    public GameObject button;
    private int room;
    public bool laptopDone, diaDone;

    /*
     * Changes the button's texture to clicked.
     * Subtracts time from the remaining time.
     */
    public void ClickOnObject()
    {
        GameObject pc = GameObject.Find("PcButton");
        GameObject dh = GameObject.Find("DialogueButton");
        PcButton pcButton = pc.transform.Find("Button").GetComponent<PcButton>();
        DialogueHandler dialogueHandler = dh.GetComponent<DialogueHandler>();
        laptopDone = pcButton.laptopDone;
        diaDone = dialogueHandler.diaDone;
        if (laptopDone == true && diaDone == true)
        {
            game.GetComponent<Game>().SubtractTime();
            button.SetActive(false);
        }
    }

}