using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {

	public static MLGeffects effects;
	public static GameObject player;
	public static Camera playercam;
	public static vp_FPCamera playerfpcam;

	public static bool endgame = false;

	public static Queue<string> subtitleQueue;

	private Vector2 subtitleLocation;
	private int subtitleSize;
	private CompanionText guy1;
	private CompanionText lonesomestreamwatcher;

	private bool started;

	// Use this for initialization
	void Start () {
		AudioListener.volume = 1;
		effects = GameObject.Find("MLGeffectsObject").GetComponent<MLGeffects>();
		player = GameObject.Find ("AdvancedPlayer");
		playercam = GameObject.Find ("FPSCamera").camera;
		playerfpcam = GameObject.Find ("FPSCamera").GetComponent<vp_FPCamera>();
		guy1 = GameObject.Find ("cyclinman").GetComponentInChildren<CompanionText>();
		lonesomestreamwatcher = GameObject.Find ("LonesomeStreamWatcher").GetComponentInChildren<CompanionText>();
		subtitleLocation = new Vector2(0, Screen.height/3);
		subtitleSize = Screen.width/40;
		subtitleQueue = new Queue<string>();
		started = false;
		StartCoroutine(SubtitleEngine());

	}
	
	// Update is called once per frame
	void Update () {
		//effects.DisplayRandomWords();
		if (!started){
			started = true;
			StartCoroutine(Level ());
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();
	}

	IEnumerator Level(){
		started = true;
		yield return new WaitForSeconds(1f);
		//effects.DisplayWords ("HELLO.", .06f);


		//train is moving from the start
		//shake screen when train gets near the platform
		yield return null;
	}

	IEnumerator SubtitleEngine(){
		while (true){
			if (subtitleQueue.Count != 0){
				effects.DisplayWords(subtitleQueue.Dequeue(), 3f, subtitleLocation, subtitleSize);
				yield return new WaitForSeconds(3f);
			}
			yield return null;
		}
		
	}
}
