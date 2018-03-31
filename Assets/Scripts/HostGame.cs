using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 6;

	public Text debugText;
	private string roomName;

	private NetworkManager networkManager;

	void Start() {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		}
		debugText.text = "Match Maker Started";
	}
	public void SetRoomName(string _name) {
		roomName = _name;
	}

	public void CreateRoom() {
		if (roomName != null && roomName != "") {
			Debug.Log ("Creating room: " + roomName + " with room for " + roomSize + " players.");
			debugText.text = "Creating room: " + roomName + " with room for " + roomSize + " players.";
			// create room
			//networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
			networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, OnInternetMatchCreate);

		}
	
	}

	//this method is called when your request for creating a match is returned
	private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			Debug.Log("Create match succeeded");
			debugText.text = "Create match succeeded";

			MatchInfo hostInfo = matchInfo;
			NetworkServer.Listen(hostInfo, 9000);

			NetworkManager.singleton.StartHost(hostInfo);
		}
		else
		{
			Debug.LogError("Create match failed");
			debugText.text = "Create match failed";
		}
	}

	private static int playerCount = 0;
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + playerCount + " connected from " + player.ipAddress + ":" + player.port);
	}
}
