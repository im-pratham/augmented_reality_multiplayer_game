﻿using System.Collections;
using UnityEngine;

public class SinglePlayerCollisionScript : MonoBehaviour {

	ScoreManagement scoreMan = new ScoreManagement();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// for this to work both need colliders, one must have rigidbody (spacecraft), the other must have 'is trigger' checked
	void OnTriggerEnter(Collider col) {
		GameObject explosion = Instantiate (Resources.Load ("FlareMobile", typeof(GameObject))) as GameObject;
		explosion.transform.position = transform.position;
		Destroy (col.gameObject);
		Destroy (explosion, 2);

		if(GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			GameObject enemy = Instantiate (Resources.Load ("enemy", typeof(GameObject))) as GameObject;
			GameObject enemy1 = Instantiate (Resources.Load ("enemy1", typeof(GameObject))) as GameObject;
			GameObject enemy2 = Instantiate (Resources.Load ("enemy2", typeof(GameObject))) as GameObject;
			GameObject enemy3 = Instantiate (Resources.Load ("enemy3", typeof(GameObject))) as GameObject;
		}
		scoreMan.SetCount ();
		Destroy (gameObject);
	}
	void SetScoreGame(){
		GameObject explosion = Instantiate (Resources.Load ("FlareMobile", typeof(GameObject))) as GameObject;
		explosion.transform.position = transform.position;
		Destroy (explosion, 2);

		if(GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			GameObject enemy = Instantiate (Resources.Load ("enemy", typeof(GameObject))) as GameObject;
			GameObject enemy1 = Instantiate (Resources.Load ("enemy1", typeof(GameObject))) as GameObject;
			GameObject enemy2 = Instantiate (Resources.Load ("enemy2", typeof(GameObject))) as GameObject;
			GameObject enemy3 = Instantiate (Resources.Load ("enemy3", typeof(GameObject))) as GameObject;
		}
		scoreMan.SetCount ();
		Destroy (gameObject);
	}
}
