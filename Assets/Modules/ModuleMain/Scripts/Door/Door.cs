using UnityEngine;
using System.Collections;


/*
 * Original by Alexander Ameye (11/11/2015).
 * Edited by Michelle Ritzema.
 * 
 * Class that contains all the behaviour a door should have.
 */
public class Door : MonoBehaviour {

    private SideOfHinge hingeSide = SideOfHinge.Left;
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

    public enum SideOfHinge
	{
		Left,
		Right,
	}

    private void setupDoor()
    {
        // Calculate Cosine and Sine of initial angle (needed for math calculations).
        float CosDeg = Mathf.Cos((transform.eulerAngles.y * Mathf.PI) / 180);
        float SinDeg = Mathf.Sin((transform.eulerAngles.y * Mathf.PI) / 180);

        // HINGE POSITIONING
        // Read transform (position/rotation/scale) of door.
        float PosDoorX = transform.position.x;
        float PosDoorY = transform.position.y;
        float PosDoorZ = transform.position.z;

        float RotDoorX = transform.localEulerAngles.x;
        //float RotDoorY = transform.localEulerAngles.y;
        float RotDoorZ = transform.localEulerAngles.z;

        float ScaleDoorX = transform.localScale.x;
        //float ScaleDoorY = transform.localScale.y;
        float ScaleDoorZ = transform.localScale.z;

        // Make copy of hinge's position/rotation (placeholder).
        Vector3 HingePosCopy = hinge.transform.position;
        Vector3 HingeRotCopy = hinge.transform.localEulerAngles;

        // Set side of hinge left.
        if (hingeSide == SideOfHinge.Left)
        {
            // Math. (!RADIANS)
            if (transform.localScale.x > transform.localScale.z)
            {
                HingePosCopy.x = (PosDoorX - (ScaleDoorX / 2 * CosDeg));
                HingePosCopy.z = (PosDoorZ + (ScaleDoorX / 2 * SinDeg));
                HingePosCopy.y = PosDoorY;

                HingeRotCopy.x = RotDoorX;
                HingeRotCopy.y = -startAngle;
                HingeRotCopy.z = RotDoorZ;
            }

            else
            {
                HingePosCopy.x = (PosDoorX + (ScaleDoorZ / 2 * SinDeg));
                HingePosCopy.z = (PosDoorZ + (ScaleDoorZ / 2 * CosDeg));
                HingePosCopy.y = PosDoorY;

                HingeRotCopy.x = RotDoorX;
                HingeRotCopy.y = -startAngle;
                HingeRotCopy.z = RotDoorZ;
            }
        }

        // Set side of hinge right.
        if (hingeSide == SideOfHinge.Right)
        {
            // Math. (!RADIANS)
            if (transform.localScale.x > transform.localScale.z)
            {
                HingePosCopy.x = (PosDoorX + (ScaleDoorX / 2 * CosDeg));
                HingePosCopy.z = (PosDoorZ - (ScaleDoorX / 2 * SinDeg));
                HingePosCopy.y = PosDoorY;

                HingeRotCopy.x = RotDoorX;
                HingeRotCopy.y = -startAngle;
                HingeRotCopy.z = RotDoorZ;
            }

            else
            {
                HingePosCopy.x = (PosDoorX - (ScaleDoorZ / 2 * SinDeg));
                HingePosCopy.z = (PosDoorZ - (ScaleDoorZ / 2 * CosDeg));
                HingePosCopy.y = PosDoorY;

                HingeRotCopy.x = RotDoorX;
                HingeRotCopy.y = -startAngle;
                HingeRotCopy.z = RotDoorZ;
            }
        }

        // Hinge positioning.
        hinge.transform.position = HingePosCopy;
        transform.parent = hinge.transform;
        hinge.transform.localEulerAngles = HingeRotCopy;
        // Angle defining.
        // Set 'startRotation' to be rotation when door is not yet moved.
        startRotation = Quaternion.Euler(0, -startAngle, 0);
        // Set 'endRotation' to be the rotation when door is moved.
        endRotation = Quaternion.Euler(0, -endAngle, 0);
    }

	public void Start ()
	{
		hinge = new GameObject();
		hinge.name = "hinge";
        setupDoor();
    }

    /*
     * Sets the moveAmount to either 0 (infinite) or -1 (none)
     */
    public void DoorMovable(bool movable)
    {
        if (movable)
            moveAmount = 0;
        else
            moveAmount = -1;
    }

	// OPEN FUNCTION
	public IEnumerator Open()
    {
		if (moveIterator < moveAmount || moveAmount == 0)
		{
            // Change state from 1 to 0 and back (= change from endRotation to startRotation).
            if (hinge.transform.rotation == (state == 0 ? endRotation : startRotation))
                state ^= 1;
            // Set 'finalRotation' to 'endRotation' when moving and to 'startRotation' when moving back.
            Quaternion finalRotation = ((state == 0) ? endRotation : startRotation);
    	// Make the door rotate until it is fully opened/closed.
    	while (Mathf.Abs(Quaternion.Angle(finalRotation, hinge.transform.rotation)) > 0.01f)
    	{
			running = true;
			hinge.transform.rotation = Quaternion.Lerp (hinge.transform.rotation, finalRotation, Time.deltaTime * speed);

      		yield return new WaitForEndOfFrame();
    	}

            running = false;
            if (opened)
                opened = false;
            else
                opened = true;


            if (moveAmount == 0)
			{
                moveIterator = 0;
			}

			else moveIterator++;

		}
	}

	/*
     * Displays an indication to the user that the door can be interacted with.
     */
	void OnGUI ()
	{
		Detection detection = GameObject.Find("Player").GetComponent<Detection>();
		if (detection.GetInReach() == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to open/close");
		}
	}

    /*
     * Returns the state integer.
     */
    public int GetState()
    {
        return state;
    }

    /*
     * Returns the running boolean.
     */
    public bool GetRunning()
    {
        return running;
    }

    /*
     * Returns the opened boolean.
     */
    public bool GetOpened()
    {
        return opened;
    }

}