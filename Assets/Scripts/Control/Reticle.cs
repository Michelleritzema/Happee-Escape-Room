using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that creates and updates the reticle of the game to indicate where 
 * the player is looking. Interaction with certain objects is handled here.
 */

public class Reticle : MonoBehaviour {

    private string tagDoor = "Door";
    private string tagPasswordButton = "PasswordButton";
    private string tagPasswordButtonCheck = "PasswordButtonCheck";
    private string tagRoomSelectorButton = "RoomSelectorButton";
    private string tagRoomDoneButton = "DoneButton";
	private string tagDialogeButton = "DialogueTag";
    private string tagLaptop = "PC";
    private string tagPcScreen = "PcScreen";
    private string tagAnsw = "antw4";
    private string tagWrongAnsw = "antw1";
    private string tagWrongAnsw1 = "antw2";
    private string tagWrongAnsw2 = "antw3";


    public Game game;
    private GameObject lastObject;
    private Vector3 originalScale;
    private Door visibleDoor;
    private bool inReach;

    public Camera playerCamera, PcCamera;
    

    /*
     * Stores the original reticle scale;
     */
    public void Start () {
        originalScale = transform.localScale;
        lastObject = null;
    }

    /*
     * Called every time the screen is updated. Determines whether or not the user's field of vision is colliding with a door object. 
     * A ray is cast from the center of the screen. Updates the reticle and the game object that the user is interacting with. 
     * The reticle is always placed in the center of the screen. The size of the reticle is changed according to the distance 
     * of the object the user is looking at.
     */
    public void Update () {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.rotation * Vector3.forward);
        RaycastHit hit;
        float reticleDistance;
        if (Physics.Raycast(ray, out hit))
        {
            reticleDistance = hit.distance;
            HandleHit(hit);
        }
        else
            reticleDistance = playerCamera.farClipPlane * 0.95f;
        SetReticle(reticleDistance);
    }

    /*
     * Sets the reticle position, rotation and scale, based on the object the user is looking at.
     */
    private void SetReticle(float reticleDistance)
    {
        transform.position = playerCamera.transform.position +
            playerCamera.transform.rotation * Vector3.forward * reticleDistance;
        transform.LookAt(playerCamera.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        transform.localScale = originalScale * reticleDistance;
    }

    /*
     * Calls the appropriate methods if a certain object has been hit.
     * Also resets the object the user previously looked at to its initial state.
     * Objects should only be regarded as hit if the player has a distance less or equal to 3.
     */
    private void HandleHit(RaycastHit hit)
    {
            GameObject hitObject = hit.transform.gameObject;
            ResetInteractedObject(hitObject);
            if (hit.distance <= 3)
                HandleObjectInteraction(hitObject);
            lastObject = hitObject;
    }

    /*
     * Resets the object the user just interacted with to its original state.
     * This is done by checking the game object tag.
     */
    public void ResetInteractedObject(GameObject hitObject)
    {
        if (lastObject != null && hitObject.tag != lastObject.tag)
        {
            if (lastObject.tag == tagDoor)
                UnsetDoor();
            else if (lastObject.tag == tagPasswordButton)
                lastObject.GetComponent<ButtonActions>().SetToInactive();
            else if (lastObject.tag == tagPasswordButtonCheck)
                lastObject.GetComponent<CheckButtonActions>().SetToInactive();
            else if (lastObject.tag == tagRoomSelectorButton)
                lastObject.GetComponent<RoomButtons>().SetToInactive();
            else if (lastObject.tag == tagRoomDoneButton)
                lastObject.GetComponent<CompletionButtonActions>().SetToInactive();
        }
    }

    /*
     * Handles object interaction by checking the game object tag and user input. 
     * The appropriate method is called accordingly.
     */
    public void HandleObjectInteraction(GameObject hitObject)
    {
        if (hitObject.tag == tagDoor)
        {
            SetDoor(hitObject);
            if (Input.GetKey(KeyCode.E) && visibleDoor.GetRunning() == false)
                StartCoroutine(visibleDoor.Open());
        }
        else if (hitObject.tag == tagPasswordButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<ButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<ButtonActions>().SetToActive();
        }
        else if (hitObject.tag == tagPasswordButtonCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<CheckButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<CheckButtonActions>().SetToActive();
        }
        else if (hitObject.tag == tagRoomSelectorButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<RoomButtons>().ClickOnObject();
            else
                hitObject.GetComponent<RoomButtons>().SetToActive();
        }
        else if (hitObject.tag == tagRoomDoneButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<CompletionButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<CompletionButtonActions>().SetToActive();
        }
        else if (hitObject.tag == tagLaptop)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<PcButton>().ClickOnObject();
            else
                hitObject.GetComponent<PcButton>().SetToActive();
        }
        else if(hitObject.tag == tagDialogeButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<dialogueHandler>().UpdateText();
        }
        else if (hitObject.tag == tagAnsw)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<CompletionButtonActions>().ClickOnObject();
        }
        else if (hitObject.tag == tagWrongAnsw || hitObject.tag == tagWrongAnsw1 || hitObject.tag == tagWrongAnsw2)
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<WrongAnswerButton>().ClickOnObject();
        }
    }
    
    /*
     * Sets the visible door.
     */
    private void SetDoor(GameObject hitObject)
    {
        inReach = true;
        visibleDoor = hitObject.GetComponent<Door>();
    }

    /*
     * Unsets the visible door.
     */
    private void UnsetDoor()
    {
        inReach = false;
        visibleDoor = null;
    }

    /*
     * Returns the visible door object.
     */
    public Door GetVisibleDoor()
    {
        return visibleDoor;
    }

    /*
     * Returns the in reach boolean.
     */
    public bool GetInReach()
    {
        return inReach;
    }

}