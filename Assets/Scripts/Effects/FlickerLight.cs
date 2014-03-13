using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour {
	
	private bool flickered = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (flickered)
			StartCoroutine(Flicker());
	
	}
	
	void SetIntensity(float rand, Transform thing){
		if (thing.GetComponent("Light"))
			thing.light.intensity = (rand/2);
		if (thing.transform.renderer)
			thing.transform.renderer.material.color = new Color ((rand/10), 0f, 0f);
	}
	
	IEnumerator Flicker (){
		flickered = false;
		float rand = Random.value;
		if (transform.GetComponent<Light>())
			light.intensity = (rand/2);
		else if (transform.renderer)
			transform.renderer.material.color = new Color ((rand/10), 0f, 0f);
		foreach (Transform child in transform){
			SetIntensity(rand, child);
		}
		yield return new WaitForSeconds(.1f);
		flickered = true;
	}
}
