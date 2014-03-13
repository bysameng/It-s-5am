using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {

	public bool visible;

	private bool faded, fading;
	public float fadeInTime = .2f, fadeOutTime = .05f, betweenFadeTime = .01f;

	public Renderer[] renderers;
	private Color[] originalColors;
	private float expire = 7f;


	// Use this for initialization
	void Start () {

		//get all the renderers and then duplicate their materials and store original colors
		renderers = transform.GetComponentsInChildren<Renderer>();
		originalColors = new Color[renderers.Length];
		for (int i = 0; i < renderers.Length; i++){
			renderers[i].material = new Material(renderers[i].material);
			originalColors[i] = renderers[i].material.color;
		}


		faded = false;
		fading = false;
		visible = false;

		StartCoroutine(FadeOut(1f));

	}
	
	// Update is called once per frame
	void Update () {
		if (visible){
			transform.LookAt(LevelScript.playercam.transform.position);
			if (expire > 0) expire -= Time.deltaTime;
			if (expire < 0) {
				StartCoroutine(FadeOut(fadeOutTime));
				expire = 7f;
			}
		}
	}


	//called upon interaction (with Use() )
	public void MakeVisible() {
		if (fading){
			Debug.Log ("It's fading!");
			return;
		}
		visible = true;
		if (faded){
			StartCoroutine (FadeBack (fadeOutTime, fadeInTime, betweenFadeTime));
		}
		else StartCoroutine(FadeIn(fadeInTime));
	}
	

	//used when wanna refresh
	IEnumerator FadeBack(float outTime, float inTime, float waitTime){
		StartCoroutine (FadeOut(outTime));
		yield return new WaitForSeconds(outTime+waitTime);
		StartCoroutine (FadeIn(inTime));
	}


	//fades in the bubble
	IEnumerator FadeIn(float seconds){
		expire = 7f;
		fading = true;
		for (float i = 0; i < seconds; i += Time.deltaTime){
			for (int j = 0; j < renderers.Length; j++){
				Color current = renderers[j].material.color;
				current.a = Mathf.SmoothStep (current.a, originalColors[j].a, i/seconds);
				renderers[j].material.color = current;
			}

			yield return null;
		}
		faded = true;
		fading = false;
	}


	//fades out the bubble
	IEnumerator FadeOut(float seconds){
		fading = true;
		for (float i = 0; i < seconds; i += Time.deltaTime){
			for (int j = 0; j < renderers.Length; j++){
				Color current = renderers[j].material.color;
				current.a = Mathf.SmoothStep (current.a, 0, i/seconds);
				renderers[j].material.color = current;
			}
			yield return null;
		}
		faded = false;
		fading = false;
	}
}
