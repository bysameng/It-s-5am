using UnityEngine;
using System.Collections;

public class RainPrefabs : MonoBehaviour {

	public GameObject prefab;
	public bool raining = false;
	public float density = 1.0f;
	public float radius = 50f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (raining){
			if (Random.Range ( 0f, 10f) <= density){
				GameObject x;
				x = (GameObject)Instantiate (prefab, this.transform.position, Quaternion.identity);
				x.transform.Rotate (new Vector3(Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360)));				
				x.transform.Translate (new Vector3(Random.Range(-radius, radius), 0, Random.Range (-radius, radius)));
				x.AddComponent<Rigidbody>();
				x.GetComponent<MeshCollider>();
				x.AddComponent<DestroyAfterTime>();
				x.AddComponent<CarShaker>();
			}
		}
	
	}
}
