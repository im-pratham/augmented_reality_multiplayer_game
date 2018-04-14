using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinglePlayerWebCamScript : MonoBehaviour {
	//public GameObject webCameraPlane;
	public Button fireButton;
	GameObject cam;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("ARCamera");
		fireButton.onClick.AddListener (onButtonDown);

	}

	void onButtonDown() {
		GameObject bullet = Instantiate (Resources.Load ("bullet", typeof(GameObject))) as GameObject;
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		bullet.transform.rotation = cam.transform.rotation;
		bullet.transform.position = cam.transform.position;
		rb.AddForce (cam.transform.forward * 500f);
		Destroy (bullet, 3);

		GetComponent<AudioSource> ().Play ();
	}
}
