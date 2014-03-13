using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubtitleText{
	
	public List<string> messages;

	public bool next = true;
	public int count = 0;

	public SubtitleText(string message){
		messages = new List<string>();
		int lastSubStr = 0;
		for (int i = 0; i < message.Length; i++){
			//using @ as delimiter
			if (message[i] == '@'){
				messages.Add(message.Substring(lastSubStr, i-lastSubStr-1));
				lastSubStr = i+2;
			}
			else if (i == message.Length-1){
				messages.Add(message.Substring(lastSubStr, i-lastSubStr+1));
				break;
			}
		}
	}

	public string GetNext(){
		if (count < messages.Count){
			return messages[count];
		}
		else next = false;
		return "";
	}

	public void Increment(){
		count = count + 1;
		if (count >= messages.Count)
			next = false;
	}

	public string Next(){
		if (count != messages.Count){
			return messages[count++];
		}
		if (count >= messages.Count) next = false;
		return "";
	}

}
