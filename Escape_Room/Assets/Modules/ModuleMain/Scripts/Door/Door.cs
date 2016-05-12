using UnityEngine;
using UnityEditor;
using System.Collections;


public class Door : MonoBehaviour {

    private int n = 0; //For 'moveAmount' loop.
    [HideInInspector] public bool Running = false;

    public float startAngle = 0.0F;
	public float endAngle = 90.0F;
    public int moveAmount = 0;
    public float speed = 3F;

    public SideOfHinge HingeSide;
    private Quaternion endRotation, startRotation;
    private int State;

    public enum SideOfHinge
	{
		Left,
		Right,
	}

	// Create a hinge.
	private GameObject hinge;

	// START FUNCTION
	void Start ()
	{
		// Create a hinge.
		hinge = new GameObject();
		hinge.name = "hinge";

		// Calculate Cosine and Sine of initial angle (needed for math calculations).
		float CosDeg = Mathf.Cos ((transform.eulerAngles.y * Mathf.PI) / 180);
		float SinDeg = Mathf.Sin ((transform.eulerAngles.y * Mathf.PI) / 180);

		// HINGE POSITIONING
		// Read transform (position/rotation/scale) of door.
		float PosDoorX = transform.position.x;
		float PosDoorY = transform.position.y;
	    float PosDoorZ = transform.position.z;

		float RotDoorX = transform.localEulerAngles.x;
		float RotDoorY = transform.localEulerAngles.y;
		float RotDoorZ = transform.localEulerAngles.z;
	
		float ScaleDoorX = transform.localScale.x;
		float ScaleDoorY = transform.localScale.y;
		float ScaleDoorZ = transform.localScale.z;

		// Make copy of hinge's position/rotation (placeholder).
		Vector3 HingePosCopy = hinge.transform.position;
		Vector3 HingeRotCopy = hinge.transform.localEulerAngles;

		// Set side of hinge left.
		if (HingeSide == SideOfHinge.Left)
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
		if (HingeSide == SideOfHinge.Right)
		{
			// Math. (!RADIANS)
			if(transform.localScale.x > transform.localScale.z)
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
		
		// DEBUGGING
		//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//cube.transform.position = HingePosCopy;
		//cube.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		//cube.GetComponent<Collider> ().tag = "DebugCube";
		//cube.GetComponent<Renderer>().material.color = Color.black;
		
		// USER ERROR CODES
		if (Mathf.Abs(startAngle) + Mathf.Abs(endAngle) == 180 || Mathf.Abs(startAngle) + Mathf.Abs(endAngle) > 180)
		{
			UnityEditor.EditorUtility.DisplayDialog ("Error 001", "Difference between startAngle and endAngle can't be >=180", "Ok", "");
			UnityEditor.EditorApplication.isPlaying = false;
		}

        // Angle defining.
        // Set 'startRotation' to be rotation when door is not yet moved.
        startRotation = Quaternion.Euler (0, -startAngle, 0);
        // Set 'endRotation' to be the rotation when door is moved.
        endRotation = Quaternion.Euler(0, -endAngle, 0);
	}

	// UPDATE FUNCTION
	void Update ()
	{

	}

    public void DoorMovable(bool movable)
    {
        if (movable)
            moveAmount = 0;
        else
            moveAmount = -1;
    }

	// OPEN FUNCTION
	public IEnumerator Open ()
    {
		if (n < moveAmount || moveAmount == 0)
		{
			if (hinge.transform.rotation == (State == 0 ? endRotation : startRotation))
			{
                // Change state from 1 to 0 and back (= change from endRotation to startRotation).
                State ^= 1;
			}

            // Set 'finalRotation' to 'endRotation' when moving and to 'startRotation' when moving back.
            Quaternion finalRotation = ((State == 0) ? endRotation : startRotation);

    	// Make the door rotate until it is fully opened/closed.
    	while (Mathf.Abs(Quaternion.Angle(finalRotation, hinge.transform.rotation)) > 0.01f)
    	{
			Running = true;
			hinge.transform.rotation = Quaternion.Lerp (hinge.transform.rotation, finalRotation, Time.deltaTime * speed);

      		yield return new WaitForEndOfFrame();
    	}

			Running = false;

			if(moveAmount == 0)
			{
				n = 0;
			}

			else n++;

		}
	}

	// GUI FUNCTION
	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("Player");
		Detection detection = Player.GetComponent<Detection>();

		if (detection.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to open/close");
		}
	}
}
