using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCameraPosition : MonoBehaviour {

	public static GameObject cam;
	public Text debugText;
	// Use this for initialization
	void Start () {

		cam = GameObject.Find("ARCamera");
		Debug.Log( cam.transform.position.ToString() );
		// you can simply use transform.position if the script is attached to the ARCamera

	}

	// Update is called once per frame
	void Update () {

	}
	int i = 0;
	void OnGUI(){

		if( GUI.Button(new Rect(20, 20, 100, 200),"ARCamera Position") ){
			Debug.Log( cam.transform.position.ToString() );
			debugText.text = i++ + " " + cam.transform.position.ToString () + " " + cam.transform.rotation;
		}
	}
}
