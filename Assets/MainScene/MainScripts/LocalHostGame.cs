using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class LocalHostGame : MonoBehaviour {
	public Text debugText;


	public void StartupHost() {
		Debug.Log ("Hosting Gaame");
		debugText.text = "Hosting Gaame";
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	void SetIPAddress() {
		string ipAddress = GameObject.Find ("InputFieldIPAddress").transform.Find ("Text").GetComponent<Text> ().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void SetPort() {
		NetworkManager.singleton.networkPort = 7777;
	}
}
