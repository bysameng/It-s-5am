using UnityEngine;
using System.Collections;

public class FogEffects : MonoBehaviour {
	
	private Color oldColor;
	private Color newColor;
	
	private bool fadingColor;
	
	private float startTime;
	
	private float pulseTime;
	private float pulseReturnTime;
	private float pulseSustainTime;
	
	private bool sustaining = false;
	private bool sustained = false;
	

	// Use this for initialization
	void Start () {
		oldColor = RenderSettings.fogColor;
		newColor = oldColor;
		fadingColor = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (newColor != oldColor && fadingColor){
			float t = (Time.time - startTime) / pulseTime;
			float red = Mathf.SmoothStep(oldColor.r, newColor.r, t);
			float green = Mathf.SmoothStep(oldColor.g, newColor.g, t);
			float blue = Mathf.SmoothStep(oldColor.b, newColor.b, t);
			RenderSettings.fogColor = new Color(red, green, blue);
		}

		
	
		
		if (RenderSettings.fogColor == newColor && RenderSettings.fogColor != oldColor && !sustaining && !sustained){
			StartCoroutine(Waitsustain(pulseSustainTime));
		}
		
		if (!fadingColor && RenderSettings.fogColor != oldColor && !sustaining && sustained){
			float t = (Time.time - startTime) / pulseReturnTime;
			float red = Mathf.SmoothStep(newColor.r, oldColor.r, t);
			float green = Mathf.SmoothStep(newColor.g, oldColor.g, t);
			float blue = Mathf.SmoothStep(newColor.b, oldColor.b, t);
			RenderSettings.fogColor = new Color(red, green, blue);
		}
	}
	
	public void Pulse(Color color, float fadein, float sustain, float fadeout){	
		sustaining = false;
		sustained = false;
		newColor = color;
		fadingColor = true;
		pulseTime = fadein;
		pulseSustainTime = sustain;
		pulseReturnTime = fadeout;
		startTime = Time.time;
	}
	
	IEnumerator Waitsustain(float seconds){
		sustaining = true;
		yield return new WaitForSeconds(seconds);
		startTime = Time.time;
		fadingColor = false;
		sustaining = false;
		sustained = true;
	}
	
	
}
