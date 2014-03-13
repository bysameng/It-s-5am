using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompanionText : MonoBehaviour {

	private int messagelength;
	public bool writing;
	private Queue <string> messagequeue;
	private int queuesize;
	private CompSmoothFollow controls;
	private float delay = .55f;

	private SpeechBubble bubble;

	public bool usable = true;

	private bool skip;

	public AudioClip continuesound;
	public AudioClip keypresssound;

	// Use this for initialization
	void Start () {
		skip = false;
		bubble = gameObject.transform.parent.parent.GetComponent<SpeechBubble>();
		controls = GameObject.Find ("Companion").GetComponent<CompSmoothFollow>();
		if (controls == null) Debug.Log ("cannot find companion");
		writing = false;
		messagelength = 16;
		//Display("Why do you create meaning where there is none? You should stop lest someone break your skull open.");
		messagequeue = new Queue<string>();
		queuesize = 0;

	}
	
	public void Hide(){
		controls.Hide();
	}

	public void Show(){
		controls.Show();
	}

	// Update is called once per frame
	void Update () {
		if (writing) return;
		if (usable){
			if (queuesize > 0 && !writing && Input.GetButtonDown ("Advance Plot")){
				if (continuesound != null)
					AudioSource.PlayClipAtPoint(continuesound, this.transform.position);
				queuesize--;
				string lastmsg = messagequeue.Dequeue();
				if (delay > 0) StartCoroutine(Displayer(lastmsg, delay));
				else StartCoroutine(Displayer(lastmsg));
				if (queuesize == 0) Display(lastmsg);
			}
			if (writing && Input.GetButtonDown ("Advance Plot"))
				skip = true;
		}
	}


	public void Display(string message){
		messagequeue.Enqueue (message);
		queuesize++;
	}

	public void Display(string message, float seconds){
		delay = seconds;
		messagequeue.Enqueue (message);
		queuesize++;
	}


	private string Format(string message){
		string formatted = message;
		int timesinserted = 1;

		for (int i = messagelength; i < message.Length; i+= messagelength){
			int insertionposition = timesinserted * messagelength;
			bool inserted = false;
			for (int j = 0; j < messagelength; j++){
				if (formatted[insertionposition - j] == ' '){
					char[] array = formatted.ToCharArray();
					array[insertionposition - j] = '\n';
					formatted = new string(array);
					inserted = true;
					break;
				}
			}
			if (!inserted)
				formatted = formatted.Insert(insertionposition, "\n");
			timesinserted++;
		}

		return formatted;
	}


	public void ClearQueue(){
		messagequeue.Clear ();
	}

	public void Clear(){
		gameObject.GetComponent<TextMesh>().text = "";
	}


	public bool isClear(){
		return queuesize <= 0;
	}

	IEnumerator Displayer(string message, float delay){
		writing = true;
		bubble.MakeVisible();
		yield return new WaitForSeconds(delay);
		StartCoroutine(Displayer (message));
	}

	IEnumerator Displayer(string message){
		writing = true;
		skip = false;
		message = Format(message);
		for (int i = 1; i < message.Length+1; i++){

			gameObject.GetComponent<TextMesh>().text = message.Substring(0, i);
			if (keypresssound != null)
				AudioSource.PlayClipAtPoint(keypresssound, this.transform.position);
			if (message[i-1] == '.' || message[i-1] == '?' || message[i-1] == '!') yield return new WaitForSeconds(.1f);

			/*
			if (skip){
				yield return new WaitForSeconds(.005f);
			}
			*/

			else yield return new WaitForSeconds(.04f);
		}
		yield return new WaitForSeconds(.1f);
		writing = false;
		skip = false;

	}
}
