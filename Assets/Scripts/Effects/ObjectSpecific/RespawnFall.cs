using UnityEngine;
using System.Collections;

public class RespawnFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			LevelScript.effects.BlankScreen(Color.black);
			if (LevelScript.endgame) Application.Quit();
			Application.LoadLevel(Application.loadedLevel);
		}
	}


}
