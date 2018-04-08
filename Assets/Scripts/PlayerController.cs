using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	void Start () {
		if (!isLocalPlayer)
			return;
		Debug.Log ("setting Player: ");
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener(CmdFire);
	}
	void Update() {
		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}


	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire () {
		/***
		// create bullet from bullet prefab
		var bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		// add velocity to bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// spawn the bullet on clients
		NetworkServer.Spawn(bullet);
		// destroy bullet after 2 seconds
		Destroy(bullet, 2.0f);
		**/
		//GameObject bullet = Instantiate (Resources.Load ("bullet", typeof(GameObject))) as GameObject;
		GameObject bullet = (GameObject) Instantiate(bulletPrefab);
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		bullet.transform.rotation = Camera.main.transform.rotation;
		bullet.transform.position = Camera.main.transform.position;
		rb.AddForce (Camera.main.transform.forward * 500f);
		Destroy (bullet, 2.0f);

		//GetComponent<AudioSource> ().Play ();
	}


	public override void OnStartLocalPlayer ()
	{
		GetComponent <MeshRenderer>().material.color = Color.blue;
	}
}