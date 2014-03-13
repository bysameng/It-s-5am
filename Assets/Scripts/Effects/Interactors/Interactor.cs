using UnityEngine;
using System.Collections;


//give this to the player object

public class Interactor : MonoBehaviour {

	public float interactDistance = 1f;
	public RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Physics.Raycast(r, out hit, interactDistance) && hit.collider.gameObject.layer == 8){
			if (Input.GetButtonDown("Advance Plot")){
				hit.collider.gameObject.SendMessage("Use");
			}
			else hit.collider.gameObject.SendMessage("DisplayPrompt");
		}

	}
}
