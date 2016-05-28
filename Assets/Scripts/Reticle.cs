using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that creates the reticle of the game.
 * Here interaction with certain objects is handled.
 */

public class Reticle : MonoBehaviour {

    private GameObject lastObject;
    private Vector3 originalScale;

    public Camera playerCamera;

    /*
     * Stores the original reticle scale;
     */
    void Start () {
        originalScale = transform.localScale;
        lastObject = null;
    }

	/*
     * Updates the reticle and the game object that the user is interacting with.
     */
	void Update () {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            GameObject hitObject = hit.transform.gameObject;
            ResetInteractedObject(hitObject);
            HandleObjectInteraction(hitObject);
            lastObject = hitObject;
        }
        float distance;
        if (Physics.Raycast (new Ray(playerCamera.transform.position, playerCamera.transform.rotation * Vector3.forward), out hit)) {
            distance = hit.distance;
        else {
            distance = playerCamera.farClipPlane * 0.95f;
        transform.position = playerCamera.transform.position +
            playerCamera.transform.rotation * Vector3.forward * distance;
        transform.LookAt(playerCamera.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        transform.localScale = originalScale * distance;
    }

    /*
     * Resets the object the user just interacted with to its original state.
     * This is done by checking the game object tag.
     */
    public void ResetInteractedObject(GameObject hitObject)
    {
        if (lastObject != null && hitObject.tag != lastObject.tag)
        {
            if (lastObject.tag == "PasswordButton")
                lastObject.GetComponent<ButtonActions>().SetToInactive();
            if (lastObject.tag == "PasswordButtonCheck")
                lastObject.GetComponent<CheckButtonActions>().SetToInactive();
            if (lastObject.tag == "RoomSelectorButton")
                lastObject.GetComponent<RoomButtons>().SetToInactive();
            if (lastObject.tag == "DoneButton")
                lastObject.GetComponent<CompletionButtonActions>().SetToInactive();
        }
    }

    /*
     * Handles object interaction by checking the game object tag and user input. 
     * The appropriate method is called accordingly.
     */
    public void HandleObjectInteraction(GameObject hitObject)
    {
        if (hitObject.tag == "PasswordButton")
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<ButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<ButtonActions>().SetToActive();
        }
        else if (hitObject.tag == "PasswordButtonCheck")
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<CheckButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<CheckButtonActions>().SetToActive();
        else if (hitObject.tag == "RoomSelectorButton")
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<RoomButtons>().ClickOnObject();
            else
                hitObject.GetComponent<RoomButtons>().SetToActive();
        }
        else if (hitObject.tag == "DoneButton")
        {
            if (Input.GetKeyDown(KeyCode.E))
                hitObject.GetComponent<CompletionButtonActions>().ClickOnObject();
            else
                hitObject.GetComponent<CompletionButtonActions>().SetToActive();
        }
    }

}