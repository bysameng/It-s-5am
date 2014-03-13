using UnityEngine;
using System.Collections;

public class CarShaker : MonoBehaviour {

	public float strength = 5.0f;
	public float duration = 0.3f;

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "AdvancedPlayer")
			other.gameObject.GetComponentInChildren<MLGeffects>().ShakeScreen(strength, duration);
	}
}
