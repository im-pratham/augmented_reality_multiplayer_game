using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine;

public class GameInfoMenu : MonoBehaviour {

	[SerializeField]
	public Text debugText;

	public static bool isOn = true;
	private NetworkManager networkManager;

	void Start() {
		networkManager = NetworkManager.singleton;
		MatchInfo matchInfo = networkManager.matchInfo;
		debugText.text = matchInfo.ToString();
	}


	public void LeaveRoom() {
		MatchInfo matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
		networkManager.StopHost ();

	}
}
