using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class PlotTrigger : MonoBehaviour {

	public string playername = "AdvancedPlayer";
	//private GameObject player = LevelScript.player;

	public SubtitleText[] messageArray;

	public float messageduration = 3f;
	public float delay = .2f;

	public TextAsset messageFile;


	// Use this for initialization
	void Start () {
		string[] messages = messageFile.text.Split ("\n"[0]);
		messageArray = new SubtitleText[messages.Length];
		for (int i = 0; i < messages.Length; i++){
			messageArray[i] = new SubtitleText(messages[i]);
		}
	}

	public void LoadFile(string path){
		messageFile = (TextAsset) Resources.Load(path, typeof(TextAsset));
		this.Start();
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "AdvancedPlayer"){
			Destroy(this.collider);
			StartCoroutine("Activate");
		}
	}

	public void ShowMessage(){
		StartCoroutine("Activate");
		Destroy(this.collider);
	}

	IEnumerator Activate(){
		int messageIndex = Random.Range(0, messageArray.Length);
		SubtitleText msg = messageArray[messageIndex];
		while (msg.next){
			LevelScript.subtitleQueue.Enqueue(msg.GetNext());
			msg.Increment();
		}
		Destroy(this.gameObject);
		yield return null;
	}


}
