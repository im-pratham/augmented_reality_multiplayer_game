using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetupScript : MonoBehaviour {
	[SerializeField]
	GameObject scoreBoard;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// for scoreboard
		if(Input.GetKeyDown(KeyCode.Tab)) {
			scoreBoard.SetActive(true);
		} else if(Input.GetKeyUp(KeyCode.Tab)) {
			scoreBoard.SetActive (false);
		}
	}
}
