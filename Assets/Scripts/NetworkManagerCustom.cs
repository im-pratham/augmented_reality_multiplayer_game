using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManagerCustom : NetworkManager {

	public void StartupHost() {
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame() {
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

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		Debug.Log ("level loaded ");
		Debug.Log (scene.name);
		Debug.Log (mode);
		if (scene.name.Equals("Menu")) {
			Debug.Log ("menu called");
			//SetUpMenuSceneButtons ();
			StartCoroutine(SetUpMenuSceneButtons ());
		} else {
			SetUpOtherSceneButtons();
		}
	}

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
		NetworkManager.singleton.StopHost ();
	}
}
