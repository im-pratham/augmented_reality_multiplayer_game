using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Match;

public class NetworkManagerCustom : NetworkManager {
	public Text debugText;



	public void StartupHost() {
		Debug.Log ("Hosting Gaame");
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame() {
		Debug.Log ("Joining Game");
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetIPAddress() {
		string ipAddress = GameObject.Find ("InputFieldIPAddress").transform.Find ("Text").GetComponent<Text> ().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void SetPort() {
		NetworkManager.singleton.networkPort = 7777;
	}

	/*void OnLevelWasLoaded(int level) {
		if (level == 0) {
			//SetUpMenuSceneButtons ();
			StartCoroutine(SetUpMenuSceneButtons ());
		} else {
			SetUpOtherSceneButtons ();
		}
	}*/
	/**
	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		Debug.Log ("level loaded " + scene.name);
		Debug.Log (scene.name);
		Debug.Log (mode);
		if (scene.name.Equals("LocalGame")) {
			Debug.Log ("menu called");
			//SetUpMenuSceneButtons ();
			StartCoroutine(SetUpMenuSceneButtons ());
		} else if (scene.name.Equals("Arena")){
			SetUpOtherSceneButtons();
		}
	}
	**/
	IEnumerator SetUpMenuSceneButtons () {
		yield return new WaitForSeconds (0.3f);
		GameObject.Find ("ButtonLanHost").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonLanHost").GetComponent<Button> ().onClick.AddListener(StartupHost);

		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.AddListener(JoinGame);
	}

	void SetUpOtherSceneButtons() {
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener(onStopHost);
	}
	void onStopHost() {
		Debug.Log ("stooped");
		//debugText.text = "stopped";
		NetworkManager.singleton.StopHost ();
	}
	private int playerCount = 0;
	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo match) {
		Debug.Log ("Onmatch joined called");
	}
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port);

		//debugText.text = "Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port;
	}
}
