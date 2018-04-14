using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;
	public Text textPaused;
	// Use this for initialization
	void Start () {
		isPaused = false;
	}

	// Update is called once per frame
	void Update () {
		if (isPaused) {
			PauseGame (true);
			textPaused.text = "Cancel";
		} else {
			PauseGame (false);
			textPaused.text = "Pause";
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			SwitchPause ();
		}

	}

	void PauseGame(bool state){
		if (state) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
		pausePanel.SetActive (state);
	}

	public void SwitchPause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}
	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
