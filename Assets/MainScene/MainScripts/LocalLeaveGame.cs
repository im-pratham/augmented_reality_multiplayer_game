using UnityEngine.Networking;
using UnityEngine;

public class LocalLeaveGame : MonoBehaviour {

	public void LeaveGame() {
		NetworkManager.singleton.StopHost ();
	}
}
