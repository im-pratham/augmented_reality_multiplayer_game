using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {
	List<GameObject> roomList = new List<GameObject> ();
	private NetworkManager networkManager;

	[SerializeField]
	private Text status;

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Transform roomListParent;
	// Use this for initialization
	void Start () {
		networkManager = NetworkManager.singleton;	
		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		}

		RefreshRoomList ();
	}
	
	public void RefreshRoomList() {
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
		status.text = "";
		if (!success || matchList == null) {
			status.text = "Couldn't get matches..";
			return;
		}


		// MatchDesc has been renamed to MatchInfoSnapshot. 
		foreach (MatchInfoSnapshot match in matchList) {
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab); 
			_roomListItemGO.transform.SetParent (roomListParent);

			// have a component on gameobject that will take care of setting up a name/amount of users.
			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
			if (_roomListItem != null) {
				_roomListItem.Setup (match, JoinRoom);
			}
			// as well as setting up callback function that will join a game

			roomList.Add (_roomListItemGO);
		}

		if (roomList.Count == 0) {
			status.text = "no rooms available...";
		}
	}

	public void JoinRoom(MatchInfoSnapshot _match) {
		//Debug.Log ("Joining " + _match.name);
		networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		ClearRoomList ();
		status.text = "Joining...";
	} 

	void ClearRoomList () {
		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList [i]);
		}
		roomList.Clear ();
	}
}
