using UnityEngine;

/*
 * Original by Alexander Ameye (11/11/2015).
 * Edited by Michelle Ritzema.
 * 
 * Class that handles the camera motions by following the mouse.
 * The characterBody is only set if there's a parent object controlling motion.
 */

public class MouseLook : MonoBehaviour
{
	private Vector2 mouseAbsolute;
	private Vector2 smoothMouse;
	
	public Vector2 sensitivity = new Vector2(2, 2);
	public Vector2 smoothing = new Vector2(2, 2);
	public Vector2 clampInDegrees = new Vector2(360, 180);

	public Vector2 targetDirection;
	public Vector2 targetCharacterDirection;
	public GameObject characterBody;

    /*
     * Sets the target direction to the camera's initial orientation. If a characterBody has been specified, 
     * the target direction is instead set to the character body's initial state.
     */
    public void Start()
	{
		targetDirection = transform.localRotation.eulerAngles;
		if (characterBody)
        {
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
        }
	}
	
    /*
     * Updates the (player) camera position by tracking the mouse position.
     * Fetches the raw mouse input and scales the input against the sensitivity and smoothing settings.
     * The mouse movement is interpolated over time to apply smoothing delta.
     */
	public void Update()
	{
		var targetOrientation = Quaternion.Euler(targetDirection);
		var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);
		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
		mouseAbsolute += smoothMouse;
		if (clampInDegrees.x < 360)
        {
            mouseAbsolute.x = Mathf.Clamp(mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
        }
		var xRotation = Quaternion.AngleAxis(-mouseAbsolute.y, targetOrientation * Vector3.right);
		transform.localRotation = xRotation;
		if (clampInDegrees.y < 360)
        {
            mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
        }
		transform.localRotation *= targetOrientation;
		if (characterBody)
		{
			var yRotation = Quaternion.AngleAxis(mouseAbsolute.x, characterBody.transform.up);
			characterBody.transform.localRotation = yRotation;
			characterBody.transform.localRotation *= targetCharacterOrientation;
		}
		else
		{
			var yRotation = Quaternion.AngleAxis(mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
			transform.localRotation *= yRotation;
		}
	}

}