using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreMaintain {
	public Text score;
	public static int count = 0;
	// Use this for initialization
	void Start () {
		score.text = "Score :- " + count.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = count + "";
	}
	public void SetCount (){
		//score.text = "Score :- " + counts.ToString ();
		count++;
	}
}
