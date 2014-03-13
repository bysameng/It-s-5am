using UnityEngine;
using System.Collections;

public class Talker : MonoBehaviour {

	private GameObject textObject;
	private MLGText mlgtext;
	private CompanionText comptext;
	public bool usable = true;

	public TextAsset dialoguesFile;

	// Use this for initialization
	void Start () {

		textObject = new GameObject("text");
		mlgtext = textObject.AddComponent<MLGText>();
		comptext = this.gameObject.GetComponentInChildren<CompanionText>();
		mlgtext.drawText("TALK.", 50, new Vector2(0,0));
		mlgtext.displayingText = false;
		if (dialoguesFile != null){
			ProcessDialogue();
		}
	}

	void Load(string path){
		dialoguesFile = (TextAsset) Resources.Load(path, typeof(TextAsset));
	}

	void ProcessDialogue(){
		string[] allDialogues = dialoguesFile.text.Split ('@');
		int choice = Random.Range(0, allDialogues.Length);
		string[] selectedDialogue = allDialogues[choice].Split("\n"[0]);
		for (int i = 0; i < selectedDialogue.Length; i++){
			comptext.Display(selectedDialogue[i]);
		}

	}
	
	// Update is called once per frame
	void Update () {
		mlgtext.displayingText = false;
	}

	void LateUpdate(){
		comptext.usable = mlgtext.displayingText;
	}


	void Use(){
		if (!usable || comptext.writing) return;
		mlgtext.displayingText = false;
	}

	void DisplayPrompt(){
		mlgtext.displayingText = true;
		comptext.usable = true;
	}
}
