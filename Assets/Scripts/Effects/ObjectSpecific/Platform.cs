using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public struct ControlledObject{
		GameObject obj;
		float yOffset;
		float xOffset;
	};

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			other.transform.parent = this.transform;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.name == "AdvancedPlayer"){
			other.transform.parent = this.transform;
		}
}
}