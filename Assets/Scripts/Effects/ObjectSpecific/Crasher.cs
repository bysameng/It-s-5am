using UnityEngine;
using System.Collections;

public class Crasher : MonoBehaviour {

	public AudioClip smak;
	public bool respawnlevel = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			other.gameObject.GetComponent<vp_FPController>().AddForce(this.transform.right);
			StartCoroutine(CrashDie());
		}
	}

	IEnumerator CrashDie(){
		audio.PlayOneShot(smak);
		if (!respawnlevel) yield break;
		LevelScript.effects.BlankScreen(Color.black);
		yield return new WaitForSeconds(smak.length);
		AudioListener.volume = 0;
		yield return new WaitForSeconds(2f);
		Application.LoadLevel(Application.loadedLevel);
	}
}
