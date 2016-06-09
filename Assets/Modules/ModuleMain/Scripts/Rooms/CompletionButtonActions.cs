using UnityEngine;
using System.Collections.Generic;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the button actions of the room completion button.
 */

public class CompletionButtonActions : MonoBehaviour
{

    private string tagMedical = "antw4";

    private Dictionary<string, string> dataPost;
    private string tagButton;
    private int room;

    public Game game;
    public GameObject button;
    public Material active, inactive, clicked;
    public bool laptopDone, diaDone;

    /*
     * Changes the button's texture to active.
     */
    public void SetToActive()
    {
        button.GetComponent<Renderer>().material = active;
    }

    /*
     * Changes the button's texture to clicked.
     * Sets the room status to completed and updates the unlocked password letters.
     * The medical button is only active when both parts are visited.
     */
    public void ClickOnObject()
    {
        GameObject ab = GameObject.Find("AnswerButtons");
        CompletionButtonActions answerbuttons = ab.transform.Find("Answer 3").GetComponent<CompletionButtonActions>();
        if (answerbuttons.clicked)
        {
            GameObject pc = GameObject.Find("PcButton");
            GameObject dh = GameObject.Find("DialogueButton");
            PcButton pcButton = pc.transform.Find("Button").GetComponent<PcButton>();
            DialogueHandler dialogueHandler = dh.GetComponent<DialogueHandler>();
            laptopDone = pcButton.laptopDone;
            diaDone = dialogueHandler.diaDone;
            if (laptopDone == true && diaDone == true)
            {
                button.GetComponent<Renderer>().material = clicked;
                game.GetComponent<Game>().SetRoomStatus(room, true);
                game.GetComponent<Game>().SetRoomTime(room);
                game.GetComponent<Game>().UpdateAmountOfUnlockedLetters();
                button.SetActive(false);
            }
        }
        else
        {
            button.GetComponent<Renderer>().material = clicked;
            game.GetComponent<Game>().SetRoomStatus(room, true);
            game.GetComponent<Game>().SetRoomTime(room);
            game.GetComponent<Game>().UpdateAmountOfUnlockedLetters();
            button.SetActive(false);
        }
    }

    /*
     * Changes the button's texture to inactive.
     */
    public void SetToInactive()
    {
        button.GetComponent<Renderer>().material = inactive;
    }

    /*
     * Stores the supplied room integer;
     */
    public void SetRoom(int room)
    {
        this.room = room;
    }

}