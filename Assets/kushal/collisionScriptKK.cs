using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class collisionScriptKK : MonoBehaviour {
	//private static int count = 0;
	ScoreMaintain scoreMan = new ScoreMaintain();
	// Use this for initialization
	void Start () {
		//count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// for this to work both need colliders, one must have rigidbody (spacecraft), the other must have 'is trigger' checked
	void OnTriggerEnter(Collider col) {
		GameObject explosion = Instantiate (Resources.Load ("FlareMobile", typeof(GameObject))) as GameObject;
		GameObject bullet = Instantiate (Resources.Load ("bullet", typeof(GameObject))) as GameObject;

		if (col.gameObject.Equals (bullet)) {
			
		} else {
			explosion.transform.position = transform.position;
			Destroy (col.gameObject);
			Destroy (explosion, 2);
		}
		if(GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			GameObject enemy = Instantiate (Resources.Load ("enemy", typeof(GameObject))) as GameObject;
			GameObject enemy1 = Instantiate (Resources.Load ("enemy1", typeof(GameObject))) as GameObject;
			GameObject enemy2 = Instantiate (Resources.Load ("enemy2", typeof(GameObject))) as GameObject;
			GameObject enemy3 = Instantiate (Resources.Load ("enemy3", typeof(GameObject))) as GameObject;
		}
		scoreMan.SetCount ();
		Destroy (gameObject);
		//SetCount ();
	}
}
