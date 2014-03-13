using UnityEngine;
using System.Collections;

public class DyingDoorway : MonoBehaviour {

	public float range = 40f;

	public ParticleSystem bpp;
	public int wordsfreq = 5;

	public AudioClip forwardClip;
	private AudioSource forwardSource;

	public GameObject[] objectsToRemove;
	public GameObject[] objectsToMoveWhenDone;

	public GameObject JumpTriggers;

	private float lastDist;

	public bool effectsActive = true;

	// Use this for initialization
	void Start () {
		forwardSource = this.gameObject.AddComponent<AudioSource>();
		forwardSource.loop = true;
		if (forwardClip != null){
			forwardSource.clip = forwardClip;
		}
		bpp.gameObject.SetActive(true);
		for (int i = 0; i < objectsToRemove.Length; i++){
			Destroy(objectsToRemove[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!effectsActive) return;

		float dist = Vector3.Distance(this.transform.position, LevelScript.player.transform.position);
		for (int i = 0; i < wordsfreq; i++){
			if (Random.Range (0, dist) < 2f) LevelScript.effects.DisplayRandomWords();
			if (Random.Range (0, dist) < 2f) LevelScript.effects.DisplayRandomWords();
		}

		if (lastDist > dist){
			forwardSource.enabled = true;
		}
		else forwardSource.enabled = false;

		float speed = dist / 600f;
		//LevelScript.playercam.fieldOfView = Mathf.SmoothStep (70, 120, (range - dist) / range);
		//LevelScript.player.GetComponent<vp_FPController>().MotorAcceleration = speed;
		//LevelScript.playerfpcam.BobAmplitude.z = 1000f / dist;
		//LevelScript.playerfpcam.BobRate.z = dist / 30f;
		lastDist = dist;
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			StartCoroutine(End());
		}
	}

	IEnumerator End(){
		Debug.Log ("Done.");
		AudioListener.volume = 0;
		LevelScript.endgame = true;
		effectsActive = false;
		LevelScript.effects.IncreaseLights(.32f, 2f);
		Destroy (GameObject.Find("BlackPlaneParticles"));
		JumpTriggers.SetActive(true);
		GameObject g = GameObject.Find("MoveThisUp");
		/*
		Vector3 orgpos = g.transform.position;
		Vector3 currentpos = orgpos;
		for (float t = 0; t < 10f; t+= Time.deltaTime){
			Debug.Log (currentpos);
			currentpos.y = Mathf.SmoothStep(orgpos.y, orgpos.y+30f, t/10f);
			g.transform.position = currentpos;
			yield return null;
		}
		Debug.Log ("Blanked");
		//LevelScript.effects.BlankScreen(Color.black);
		//AudioListener.volume = 0;
		yield return new WaitForSeconds(5f);

		//Application.LoadLevel(Application.loadedLevel);
		*/
		yield return null;
	}
}
