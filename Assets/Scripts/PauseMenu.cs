using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool isOn = true;
	private NetworkManager networkManager;

	void Start() {
		networkManager = NetworkManager.singleton;
	}
	public void LeaveRoom() {
		MatchInfo matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
		networkManager.StopHost ();

	}
}
