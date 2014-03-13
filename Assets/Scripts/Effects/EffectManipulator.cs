using UnityEngine;
using System.Collections;

public class EffectManipulator : MonoBehaviour {
	
	private float rotationSpeed;
	private GameObject FPSCamera;
	
	private float lastFov = 90;
	private float currentFov = 90;
	
	
	
	// Use this for initialization
	void Start () {
		FPSCamera = GameObject.Find("FPSCamera");
		transform.parent = FPSCamera.transform;
	}
	
	
	
	public void Rotate(float seconds, float speed){
		
		rotationSpeed = speed;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (rotationSpeed != 0){
			transform.Rotate (0, 0, rotationSpeed * Time.deltaTime);
		}
		currentFov = FPSCamera.GetComponent<Camera>().fieldOfView;
		if (currentFov < lastFov){
			transform.position += new Vector3(0, 0, 1/Mathf.Tan(currentFov) - 1/Mathf.Tan(lastFov));
		}
		else if (currentFov > lastFov){
			transform.position += new Vector3(0, 0, -1/Mathf.Tan(currentFov) + 1/Mathf.Tan(lastFov));
		}
		lastFov = currentFov;
	
	}
	
	
	
	IEnumerator Rotator(float seconds, float speed){
		rotationSpeed = speed;
		yield return new WaitForSeconds(seconds);
		rotationSpeed = 0;
	}
}
