using UnityEngine;

/// <summary>
/// Handles the saving, recalling, and applying of all PlayerPrefs for the application.
/// </summary>
public class PlayerPrefsHandler {
	// constant keys
	public const string MUTE_INT = "mute";
	public const string VOLUME_F = "volume";

	private const bool DEBUG_ON = true;

	/// <summary>
	/// This method should call all other methods that will apply saved or default preferences.
	/// We should call this as soon as possible when loading our application.
	/// </summary>
	public void RestorePreferences() {
		SetMuted(GetIsMuted());
		SetVolume(GetVolume());
	}

	public void SetMuted(bool muted) {
		// Set the MUTE_INT key to 1 if muted, 0 if not muted
		PlayerPrefs.SetInt(MUTE_INT, muted ? 1 : 0);

		// Pausing the AudioListener will disable all sounds.
		AudioListener.pause = muted;

		if (DEBUG_ON)
			Debug.LogFormat("SetMuted({0})", muted);
	}
		
	public bool GetIsMuted() {
		// If the value of the MUTE_INT key is 1 then sound is muted, otherwise it is not muted.
		// The default value of the MUTE_INT key is 0 (i.e. not muted).
		return PlayerPrefs.GetInt(MUTE_INT, 0) == 1;
	}

	public void SetVolume(float volume) {
		// Prevent values less than 0 and greater than 1 from
		// being stored in the PlayerPrefs (AudioListener.volume expects a value between 0 and 1).
		volume = Mathf.Clamp(volume, 0, 1);

		PlayerPrefs.SetFloat(VOLUME_F, volume);
		AudioListener.volume = volume;
	}

	public float GetVolume() {
		return Mathf.Clamp(PlayerPrefs.GetFloat(VOLUME_F, 1), 0, 1);
	}
}
