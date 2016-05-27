using UnityEngine;
using System.Collections;

public class Reticle : MonoBehaviour {

    public Camera playerCamera;
    private Vector3 originalScale;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float distance;
        if (Physics.Raycast (new Ray(playerCamera.transform.position, playerCamera.transform.rotation * Vector3.forward), out hit)) {
            distance = hit.distance;
        } else {
            distance = playerCamera.farClipPlane * 0.95f;
        }
        transform.position = playerCamera.transform.position +
            playerCamera.transform.rotation * Vector3.forward * distance;
        transform.LookAt(playerCamera.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        transform.localScale = originalScale * distance;
    }
}
