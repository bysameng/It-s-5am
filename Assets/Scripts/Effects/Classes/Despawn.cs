using UnityEngine;
using System.Collections;

public class Despawn : MonoBehaviour {

	public float timer = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyAfter(timer));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		IEnumerator DestroyAfter(float seconds){
			yield return new WaitForSeconds(seconds);
			Destroy(this.gameObject);
		}
}
