using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.storage; 
public class PauseManager : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;
	public Text score;
	StorageService storageService;
	public Text textPaused;
	Constant cons = new Constant ();
	ScoreSaveResponse callBack2 = new ScoreSaveResponse();
	// Use this for initialization
	void Start () {
		isPaused = false;
		App42API.Initialize (cons.apiKey, cons.secretKey);  
		storageService = App42API.BuildStorageService ();
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

	public void SaveScore(){
		StartCoroutine (SaveScoreForUser());
	}
	IEnumerator SaveScoreForUser ()
	{
		App42Log.SetDebug (true);
		string json = "{\"name\":\"Name1\",\"score\":\"score1\"}";
		string json1 = json.Replace ("Name1",PlayerPrefs.GetString("name")).Replace("score1",score.text);
		storageService.InsertJSONDocument("SCORES", "HighScores", json1, callBack2);
		Debug.Log ("IN Enumerator " + callBack2.getResult ());
		while (callBack2.getResult () == 0) {
			yield return new WaitForSeconds (1);
		}
		if (callBack2.getResult () == 1) {
			Debug.Log ("Score Saved Successful");
			callBack2.setResult ();
		}
		else if (callBack2.getResult () == 2) {
			//ScoreField.text = "Something wen't Wrong Please Try again";
			Debug.Log("Something wen't wrong");
			callBack2.setResult ();
		}
	}
}
