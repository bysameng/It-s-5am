using UnityEngine;
using System.Collections;

public class StreamStare : MonoBehaviour {

	private bool playerInZone = false;
	private GameObject player;
	public float stareTimer = 5f;

	public float zoomFov = 40f;
	//private float originalFov;

	private MLGeffects effects;

	private AudioSource audioS;


	private float timer;

	protected bool Actioned = false;

	public GameObject before;
	public GameObject after;

	// Use this for initialization
	void Start () {
		timer = stareTimer;
		//originalFov = 70f;
		audioS = GetComponent<AudioSource>();
		//StartCoroutine(TheStreamStare());
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone && !Actioned && Mathf.Abs(player.transform.rotation.y)< .15f){
			if ((Input.anyKey || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && !Actioned){
				//LevelScript.effects.ZoomFov(originalFov, stareTimer-timer, true);
				timer = stareTimer;
				audioS.volume = audioS.volume / 2f;
				return;
			}

			if (timer > 0 && !Actioned){ 
				timer -= Time.deltaTime;
				audioS.volume = Mathf.SmoothStep(0f, .6f, (stareTimer-timer) / stareTimer);
				LevelScript.effects.ShakeScreen(Mathf.SmoothStep(0f, 2f, (stareTimer-timer) / stareTimer));
				//LevelScript.effects.ZoomFov(zoomFov, stareTimer);
			}
			else if (timer <= 0 && !Actioned) {

				StartCoroutine (TheStreamStare());
			}

		}
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "AdvancedPlayer"){
			player = other.gameObject;
			Debug.Log ("Player Entered StreamStare");
			playerInZone = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "AdvancedPlayer"){
			Debug.Log ("Player exited StreamStare");
			playerInZone = false;
			if (!Actioned)
				audioS.volume = 0f;
			//LevelScript.effects.ZoomFov(originalFov, stareTimer-timer, true);
		}
	}

	IEnumerator TheStreamStare(){
		before.SetActive(false);
		after.SetActive(true);
		GameObject.Find("CarHighway").SetActive(false);
		Actioned = true;
		audioS.volume = .6f;

		GameObject msg = new GameObject();
		PlotTrigger pt_msg = msg.AddComponent<PlotTrigger>();
		pt_msg.LoadFile("Messages/BeforeStare/streamstare");
		pt_msg.ShowMessage();
		LevelScript.effects.IncreaseLights(12f, 30f);
		Light l = LevelScript.player.AddComponent<Light>();
		l.type = LightType.Point;
		l.color = Color.white;
		l.range = 30f;
		l.intensity = 0f;
		yield return new WaitForSeconds(5f);
		for (float i = 0; i < 50f; i+= Time.deltaTime){
			l.intensity = Random.Range (0f, Mathf.SmoothStep(0, 5f, i/50f));
			LevelScript.effects.ShakeScreen(Random.Range (0f, Mathf.SmoothStep(0, 10f, i/50f)));
			yield return null;
		}
		LevelScript.effects.ShakeScreen(10f);
		while (true && !LevelScript.endgame) {l.intensity = Random.Range (0f, 5f);
			LevelScript.effects.DisplayRandomWords();
			yield return null;}
		LevelScript.effects.ShakeScreen(0);


	}

}
