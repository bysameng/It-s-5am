using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {


	public float delay = 10.0f;


	void Start () {
		StartCoroutine (Despawn (delay));
	}

	IEnumerator Despawn(float seconds){
			yield return new WaitForSeconds(seconds);
			Destroy (this.gameObject);
	}
}

