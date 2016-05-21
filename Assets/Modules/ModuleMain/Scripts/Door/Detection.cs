using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Original by Alexander Ameye (11/11/2015).
 * Edited by Michelle Ritzema.
 * 
 * Class that enables the player to open and close doors. For this the player needs to be inside 
 * a certain radius. Only objects that have the tag "Door" can be interacted with.
 */

public class Detection : MonoBehaviour
{

    private string triggerTag = "Door";
    private Door visibleDoor;
    private float radius = 4F;
    private bool inReach;

    /*
     * Called every time the screen is updated. Determines whether or not the field of vision 
     * is colliding with a door object. The origin of the ray is set to the center of the screen 
     * and the direction of the ray to cameraview. Then the ray is cast from the center of the screen.
     */
    public void Update()
	{
		Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5F, 0.5F, 0F));
		RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius))
        {
            if (hit.collider.tag == triggerTag)
            {
                inReach = true;
                visibleDoor = hit.transform.gameObject.GetComponent<Door>();
                if (Input.GetKey(KeyCode.E) && visibleDoor.GetRunning() == false)
                    StartCoroutine(hit.collider.GetComponent<Door>().Open());
            }
            else
            {
                inReach = false;
                visibleDoor = null;
            }
        }
        else
        {
            inReach = false;
            visibleDoor = null;
        }
	}

    /*
     * Returns the in reach boolean.
     */
    public bool GetInReach()
    {
        return inReach;
    }

    /*
     * Returns the visible door object.
     */
    public Door GetVisibleDoor()
    {
        return visibleDoor;
    }

}