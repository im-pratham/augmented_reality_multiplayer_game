using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalJoinGame : MonoBehaviour {
	public Text debugText;

	public void JoinGame() {
		Debug.Log ("Joining Game");
		debugText.text = "Joining Game...";
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
}
