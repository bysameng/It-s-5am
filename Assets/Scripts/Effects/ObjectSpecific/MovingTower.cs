using UnityEngine;
using System.Collections;

public class MovingTower : MonoBehaviour {

	//opens the column
	void OnTriggerEnter(Collider other){
		if (other.name == "Subway"){
			transform.parent.animation["Open"].speed = 1f;
			transform.parent.animation.Play ("Open");
		}
	}


	//play the animation backwards
	void OnTriggerExit(Collider other){
		if (other.name == "Subway"){
			transform.parent.animation["Open"].time = transform.parent.animation["Open"].length;
			transform.parent.animation["Open"].speed = -1f;
			transform.parent.animation.Play ("Open");
		}
	}
}
