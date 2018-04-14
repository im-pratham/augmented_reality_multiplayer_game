using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SharedPrefs{
public class SharedPref{
		AndroidJavaObject AJO = null;

		public void SetPreferenceString (string prefKey, string prefValue) {
			if(AJO == null)
				AJO = new AndroidJavaObject("com.Pravin.Pravin123", new object[0]);

			AJO.Call("setPreferenceString", new object[] { prefKey, prefValue } );
		}

		public string GetPreferenceString (string prefKey) {
			if(AJO == null)
				AJO = new AndroidJavaObject("com.Pravin.Pravin123", new object[0]);

			if(AJO == null)
				return string.Empty;
			return AJO.Call<string>("getPreferenceString", new object[] { prefKey } );
		}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	}
}
