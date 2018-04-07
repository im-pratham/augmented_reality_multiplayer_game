using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomHandler : MonoBehaviour {
	public Text debugText;
	// Use this for initialization
	void Start () {		
	}

	private int playerCount = 0;
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port);
		debugText.text = "Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port;
	}
}
