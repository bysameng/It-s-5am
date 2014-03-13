using UnityEngine;
using System.Collections;

public class MoveTrain : MonoBehaviour {

	public float speed = 0;
	public float maxspeed = 30;
	private vp_FPCamera playercam;

	// Use this for initialization
	void Start () {
		playercam = null;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playercam != null){
			playercam.ShakeSpeed = speed / 5f;
		}
		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

	void Accelerate(){
		StartCoroutine(Accelerator (speed, maxspeed));
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			playercam = other.GetComponentInChildren<vp_FPCamera>();
		}
	}

	IEnumerator Accelerator(float s, float ms){
		speed = Mathf.Lerp (speed, maxspeed, 5);
		yield return null;
	}
}
