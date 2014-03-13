using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

	public GameObject car;
	public float width = 4f;
	public float spawnRate = 5f;
	private float timer = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) timer -=Time.deltaTime;
		else if (timer <= 0){
			timer = 1f/spawnRate;
			SpawnCar();

		}

	}

	void SpawnCar(){
		float ypos = 420f;
		Quaternion q = new Quaternion(0, 1, 1, 0);
		int zpos = Random.Range(0, (int)width);
		if (zpos % 2 == 0){
			ypos = -ypos;
			q.Set(1, 0, 0, -1);
		}
		zpos = zpos*2;
		Vector3 pos = this.transform.position + new Vector3(ypos, 0, zpos);
		GameObject c = (GameObject)Instantiate(car, pos, q);
		MoveTrain mtc = c.AddComponent<MoveTrain>();
		mtc.speed = 50f;
	}
}
