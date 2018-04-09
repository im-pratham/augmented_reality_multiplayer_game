using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class HealthVuforia : NetworkBehaviour {
	public const int maxHealth = 100;
	public bool destroyOnDeath;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	private NetworkStartPosition[] spawnPoints;

	void Start() {
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}
	public void TakeDamage(int amount) {
		if (!isServer) {
			return;
		}
		currentHealth -= amount;
		if (currentHealth <= 0) {
			if (destroyOnDeath) {
				Destroy (gameObject);
			} else {
				currentHealth = maxHealth;
				//Debug.Log ("Dead!");
				// called on server, but invoked on the clients
				RpcRespawn ();
			}
		}

	}

	void OnChangeHealth(int health) {
		healthBar.sizeDelta = new Vector2 (health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn() {
		if (isLocalPlayer) {
			// set the spawn point to origin as default point
			Vector3 spawnPoint = Vector3.zero;

			// if there is spawn point array, and the array is not empty, pick one at random
			if (spawnPoints != null && spawnPoints.Length > 0) {
				spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}

			// set the player's position to selected point
			transform.position = spawnPoint;
		}
	}
}
