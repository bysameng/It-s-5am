using UnityEngine;
using System.Collections;

public class RandomShifting : MonoBehaviour {

	public float maxDist = 10f;
	
	public float interval = 5f;

	private float timer;

	private bool moving;



	// Use this for initialization
	void Start () {
		moving = false;
		interval = Random.Range (1f, interval);
		timer = interval;

	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.renderer.isVisible){
			if (timer > 0) timer -= Time.deltaTime;
			if (timer <= 0){
				if (!moving){
					StartCoroutine (Shift (Random.Range (-maxDist, maxDist), Random.Range (1f, 5f), Random.Range (.1f, interval)));
				}
			}
		}
	}

	IEnumerator Shift(float distance, float seconds, float stay){
		moving = true;
		float originalY = transform.position.y;
		float newY = transform.position.y + distance;

		for (float i = 0; i < seconds; i += Time.deltaTime){
			float curr = Mathf.SmoothStep(originalY, newY, i/seconds);
			Vector3 currPos = new Vector3(transform.position.x, curr, transform.position.z);
			transform.position = currPos;
			yield return null;
		}
		yield return null;
		if (stay == 0) {
			timer = Random.Range (0f, interval);
			moving = false;
			yield break;
		}

		yield return new WaitForSeconds(stay);
		StartCoroutine(Shift (-distance, seconds, 0));
	}
}
