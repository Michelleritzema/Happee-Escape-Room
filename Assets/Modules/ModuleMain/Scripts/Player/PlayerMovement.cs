using UnityEngine;
using System.Collections;

/*
 * Original by Yurowm.
 * Edited by Michelle Ritzema.
 * 
 * Class that handles the player movement.
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private bool jump;

    public Vector3 Gravity = Vector3.down * 9.81f;
    public bool escaped = false;
    public float rotationRate = 0.1f;
    public float velocity = 8;
    public float groundControl = 1.0f;
    public float airControl = 0.2f;
    public float jumpVelocity = 5;
    public float groundHeight = 1.1f;

    public Game game;
    public Transform LookTransform;
	
    /*
     * Sets the player's rotation and gravity settings.
     */
	void Start() { 
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
	}

    /*
     * Checks whether the player is jumping or if the player has escaped the building.
     */
    void Update() {
        jump = jump || Input.GetKey(KeyCode.Space);
        if (GetComponent<Rigidbody>().position.x > 11 && escaped == false)
        {
            game.GetComponent<Game>().Escaped();
            escaped = true;
        }
    }
	
    /*
     * The player object is updated by checking or changing certain values. To check if the player is grounded, 
     * a ray is cast towards the ground. The body is rotated to stay upright. For movement the velocity change 
     * is added on the local horizontal plane.
     */
	void FixedUpdate() {
		bool grounded = Physics.Raycast(transform.position, Gravity.normalized, groundHeight);
		Vector3 gravityForward = Vector3.Cross(Gravity, transform.right);
		Quaternion targetRotation = Quaternion.LookRotation(gravityForward, -Gravity);
		GetComponent<Rigidbody>().rotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, rotationRate);
		Vector3 forward = Vector3.Cross(transform.up, -LookTransform.right).normalized;
		Vector3 right = Vector3.Cross(transform.up, LookTransform.forward).normalized;
		Vector3 targetVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * velocity;
		Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity) - localVelocity;
		velocityChange = Vector3.ClampMagnitude(velocityChange, grounded ? groundControl : airControl);
		velocityChange.y = jump && grounded ? -localVelocity.y + jumpVelocity : 0;
		velocityChange = transform.TransformDirection(velocityChange);
		GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
		GetComponent<Rigidbody>().AddForce(Gravity * GetComponent<Rigidbody>().mass);
		jump = true;
	}
	
}