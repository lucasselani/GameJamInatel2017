using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {
	GameObject cloud1;
	GameObject cloud2;
	public float speed = 5;

	// Use this for initialization
	void Start () {
		cloud1 = this.transform.Find ("nuvem1").gameObject;
		cloud2 = this.transform.Find ("nuvem2").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (cloud1.transform.position.x > 30)
			cloud1.transform.Translate(new Vector3 (-34f, 0, 0) * 120 * Time.deltaTime);
		if (cloud2.transform.position.x > 30)
			cloud2.transform.Translate(new Vector3 (-34f, 0, 0) * 120 * Time.deltaTime);
		cloud1.transform.Translate(new Vector3 (1, 0, 0) * speed * Time.deltaTime);
		cloud2.transform.Translate(new Vector3 (1, 0, 0) * speed/2 * Time.deltaTime);
	}
}
