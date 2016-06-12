using UnityEngine;
using System.Collections;

/*
 * Original by Alexander Ameye (11/11/2015).
 * Edited by Michelle Ritzema.
 * 
 * Class that contains all the behaviour a door should have.
 * Doors can be opened or closed with a special animation sequence.
 */

public class Door : MonoBehaviour {

    private bool running = false;
    private bool opened = false;
    private float startAngle = 0.0F;
	private float endAngle = 90.0F;
    private float speed = 3F;
    private int state;
    private int moveAmount = 0;
    private int moveIterator = 0;

    private GameObject hinge;
    private Quaternion endRotation, startRotation;
    private SideOfHinge hingeSide;

    public Game game;

    public enum SideOfHinge
	{
		Left,
		Right,
	}

    /*
     * Initiates the door object.
     */
	public void Start ()
	{
		hinge = new GameObject();
		hinge.name = "hinge";
        hingeSide = SideOfHinge.Left;
        SetupDoor();
    }

    /*
     * Sets up the door to open and close correctly. For this the cosine and sine of the initial angle 
     * are calculated. Also the position, rotation and scale of the door are gathered.
     * The hinge is set correctly depending on the hingeSide value.
     */
    private void SetupDoor()
    {
        float cosineAngle = Mathf.Cos((transform.eulerAngles.y * Mathf.PI) / 180);
        float sineAngle = Mathf.Sin((transform.eulerAngles.y * Mathf.PI) / 180);
        float positionDoorX = transform.position.x;
        float positionDoorY = transform.position.y;
        float positionDoorZ = transform.position.z;
        float rotationDoorX = transform.localEulerAngles.x;
        float rotationDoorZ = transform.localEulerAngles.z;
        float scaleDoorX = transform.localScale.x;
        float scaleDoorZ = transform.localScale.z;
        Vector3 hingePositionCopy = hinge.transform.position;
        Vector3 hingeRotationCopy = hinge.transform.localEulerAngles;
        if (hingeSide == SideOfHinge.Left)
        {
            if (transform.localScale.x > transform.localScale.z)
            {
                hingePositionCopy.x = (positionDoorX - (scaleDoorX / 2 * cosineAngle));
                hingePositionCopy.z = (positionDoorZ + (scaleDoorX / 2 * sineAngle));
                hingePositionCopy.y = positionDoorY;
                hingeRotationCopy.x = rotationDoorX;
                hingeRotationCopy.y = -startAngle;
                hingeRotationCopy.z = rotationDoorZ;
            }
            else
            {
                hingePositionCopy.x = (positionDoorX + (scaleDoorZ / 2 * sineAngle));
                hingePositionCopy.z = (positionDoorZ + (scaleDoorZ / 2 * cosineAngle));
                hingePositionCopy.y = positionDoorY;
                hingeRotationCopy.x = rotationDoorX;
                hingeRotationCopy.y = -startAngle;
                hingeRotationCopy.z = rotationDoorZ;
            }
        }
        if (hingeSide == SideOfHinge.Right)
        {
            if (transform.localScale.x > transform.localScale.z)
            {
                hingePositionCopy.x = (positionDoorX + (scaleDoorX / 2 * cosineAngle));
                hingePositionCopy.z = (positionDoorZ - (scaleDoorX / 2 * sineAngle));
                hingePositionCopy.y = positionDoorY;
                hingeRotationCopy.x = rotationDoorX;
                hingeRotationCopy.y = -startAngle;
                hingeRotationCopy.z = rotationDoorZ;
            }
            else
            {
                hingePositionCopy.x = (positionDoorX - (scaleDoorZ / 2 * sineAngle));
                hingePositionCopy.z = (positionDoorZ - (scaleDoorZ / 2 * cosineAngle));
                hingePositionCopy.y = positionDoorY;
                hingeRotationCopy.x = rotationDoorX;
                hingeRotationCopy.y = -startAngle;
                hingeRotationCopy.z = rotationDoorZ;
            }
        }
        SetDoorHinge(hingePositionCopy, hingeRotationCopy);
    }

    /*
     * Setting the door hinge values. The startRotation is set to be the rotation when the door
     * has not moved yet and the endRotation is set to be the rotation when the door has moved.
     */
    private void SetDoorHinge(Vector3 hingePositionCopy, Vector3 hingeRotationCopy)
    {
        hinge.transform.position = hingePositionCopy;
        transform.parent = hinge.transform;
        hinge.transform.localEulerAngles = hingeRotationCopy;
        startRotation = Quaternion.Euler(0, -startAngle, 0);
        endRotation = Quaternion.Euler(0, -endAngle, 0);
    }

    /*
     * Sets the moveAmount to either 0 (infinite) or -1 (none)
     */
    public void DoorMovable(bool movable)
    {
        if (movable)
        {
            moveAmount = 0;
        }
        else
        {
            moveAmount = -1;
        }
    }

	/*
     * Opens the door, but only if the moveAmount is higher than the moveIterator or if it is set to infinite (0).
     * The state is changed from 1 to 0, or the other way around; depending on the orientation.
     * While the animation is going, the running boolean will be set to true.
     */
	public IEnumerator Open()
    {
		if (moveIterator < moveAmount || moveAmount == 0)
		{
            if (hinge.transform.rotation == (state == 0 ? endRotation : startRotation))
            {
                state ^= 1;
            }
            Quaternion finalRotation = ((state == 0) ? endRotation : startRotation);
    	while (Mathf.Abs(Quaternion.Angle(finalRotation, hinge.transform.rotation)) > 0.01f)
    	{
			running = true;
			hinge.transform.rotation = Quaternion.Lerp (hinge.transform.rotation, finalRotation, Time.deltaTime * speed);
      		yield return new WaitForEndOfFrame();
    	}
            running = false;
            SetOpened();
            if (moveAmount == 0)
            {
                moveIterator = 0;
            }
            else
            {
                moveIterator++;
            }
		}
	}

    /*
     * Sets the opened value to true if it is false, and vice versa.
     */
    private void SetOpened()
    {
        if (opened)
        {
            opened = false;
        }
        else
        {
            opened = true;
        }
    }

	/*
     * Displays an indication to the user that the door can be interacted with.
     * If the door is not movable the user is shown a locked door message.
     */
	public void OnGUI ()
	{
		Reticle reticle = GameObject.Find("Reticle").GetComponent<Reticle>();
		if (reticle.GetInReach() == true)
		{
			GUI.color = Color.white;
            if (reticle.GetVisibleDoor() != null)
            {
                if (reticle.GetVisibleDoor().GetMoveAmount() == -1)
                {
                    GUI.Box(new Rect(40, 40, 300, 60), "Deze deur is vergrendeld", 
                        game.GetComponent<Game>().GetStandardBoxStyle(20));
                }
                else
                {
                    GUI.Box(new Rect(40, 40, 400, 60), "Druk op 'E' om de deur te openen/sluiten", 
                        game.GetComponent<Game>().GetStandardBoxStyle(20));
                }  
            }
		}
	}

    /*
     * Fetches the state integer.
     */
    public int GetState()
    {
        return state;
    }

    /*
     * Fetches the move amount integer.
     */
    public int GetMoveAmount()
    {
        return moveAmount;
    }

    /*
     * Fetches the running boolean.
     */
    public bool GetRunning()
    {
        return running;
    }

    /*
     * Fetches the opened boolean.
     */
    public bool GetOpened()
    {
        return opened;
    }

}