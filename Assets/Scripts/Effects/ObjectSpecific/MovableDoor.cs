using UnityEngine;
using System.Collections;

public class MovableDoor : MonoBehaviour {

	public float speedOpen = 1f;
	private GameObject textObject;
	private MLGText mlgtext;
	public bool usable = true;
	
	// Use this for initialization
	void Start () {
		textObject = new GameObject("text");
		
		mlgtext = textObject.AddComponent<MLGText>();
		mlgtext.drawText("OPEN.", 50, new Vector2(0,0));
		mlgtext.displayingText = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		mlgtext.displayingText = false;
	}

	void DisplayPrompt(){
		mlgtext.displayingText = true;
	}

	void Use(){
		StartCoroutine(Open());
	}

	IEnumerator Open(){
		Vector3 originalpos = this.transform.position;
		Vector3 endpos = originalpos + this.transform.forward * this.transform.localScale.z;
		for (float t = 0; t < speedOpen; t+=Time.deltaTime){
			float x, y, z;
			float smoothT = t/speedOpen;
			x = Mathf.SmoothStep(originalpos.x, endpos.x, smoothT);
			y = Mathf.SmoothStep(originalpos.y, endpos.y, smoothT);
			z = Mathf.SmoothStep(originalpos.z, endpos.z, smoothT);
			Vector3 currPos = new Vector3(x, y, z);
			transform.position = currPos;
			yield return null;
		}
	}

	void OnTriggerExit(){
		Debug.Log ("Closing door");
		transform.RotateAround(transform.position, transform.up, 180f);
		StartCoroutine(Open());
	}
}
