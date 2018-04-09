using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PlayerControllerVuforia : NetworkBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	//public Button btnForward;
	[SerializeField]
	public Image bgImg;

	[SerializeField]
	public Image joystickImg;


	private Vector3 inputVector;

	[SerializeField]
	public Button btnFire;

	void Start () {
		if (!isLocalPlayer)
			return;
		Debug.Log ("setting Player: ");
		//GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener(CmdFire);

		//bgImg = GetComponent<Image> ();
		//joystickImg = transform.GetChild (0).GetComponent<Image> ();



		if (btnFire != null) {
			Button btn = btnFire.GetComponent<Button> ();
			btn.onClick.AddListener (CmdFire);
		}

	}

	public virtual void OnDrag(PointerEventData ped) {
		if (!isLocalPlayer)
			return;
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform
			, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y= (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			Debug.Log (inputVector);
			// move joystick image
			joystickImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x /3)
				, inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		if (!isLocalPlayer)
			return;
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped) {
		if (!isLocalPlayer)
			return;
		inputVector = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal() {
		if (inputVector.x != 0) {
			return inputVector.x;
		} else {
			return Input.GetAxis ("Horizontal");
		}
	}
	public float Vertical() {
		if (inputVector.z != 0) {
			return inputVector.z;
		} else {
			return Input.GetAxis ("Vertical");
		}
	}
	void Update() {
		if (!isLocalPlayer) {
			return;
		}

		//var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		//var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		var x = Horizontal ();
		var z = Vertical ();

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
		if (Input.touchCount == 1) {
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
		// spawn the bullet on clients
		NetworkServer.Spawn(bullet);
		Destroy (bullet, 2.0f);

		//GetComponent<AudioSource> ().Play ();
	}


	public override void OnStartLocalPlayer ()
	{
		GetComponent <MeshRenderer>().material.color = Color.blue;
	}
}