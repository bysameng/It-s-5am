using UnityEngine;
using System.Collections;

public class CompSmoothFollow : MonoBehaviour {

	public Vector3 position = new Vector3(0, -.2f, .6f);
	public Vector3 hidePosition = new Vector3(0, -1f, .6f);
	private Vector3 realpos;
	private float springconstant = .4f;
	public float damping = .007f;
	private GameObject target;
	private Vector3 targetPosition;
	public bool hidden = false;
	private bool buttonpad = true;
	private bool holdingbutton = false;
	private float holdthreshold = 2f;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("FPSCamera");
		hidden = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonUp("Companion")){
			if (holdthreshold < 0)
				hidden = !hidden;
			holdingbutton = false;
			holdthreshold = 2f;
		}

		if(Input.GetButton("Companion")){
		   holdthreshold-=Time.deltaTime;
				if (buttonpad && !holdingbutton)
					Call ();
		}

		if(Input.GetButtonDown("Companion")){
			holdingbutton = true;
		}

		if(hidden) realpos = hidePosition;
		else realpos = position;

		float dist = Vector3.Distance (transform.position, target.transform.position);
		float velocity = .5f * springconstant * dist * dist;
		targetPosition = target.transform.TransformPoint(realpos);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, damping*velocity);
		transform.rotation = target.transform.rotation;
	}

	public void Call(){
		StartCoroutine(Switch ());
	}

	public void Show(){
		hidden = false;
	}

	public void Hide(){
		if (!hidden)
			Switch ();
	}


	IEnumerator Switch(){
		if (buttonpad){
			buttonpad = false;
			hidden = !hidden;
		}
		yield return new WaitForSeconds (.2f);
		buttonpad = true;
	}
}
