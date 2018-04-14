using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.storage;  
using AssemblyCSharp;
using UnityEngine.SceneManagement;
public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen,animator,anim;

	private int m_OpenParameterId;
	private Animator m_Open;
	private GameObject m_PreviouslySelected;
	public InputField username1,password1;
	public Text loginstatus,ScoreField;
	UserService userService;
	StorageService storageService;
	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	Constant cons = new Constant ();
	SignInResponse callBack = new SignInResponse ();
	ScoreResponse callBack1 = new ScoreResponse();
	ScoreSaveResponse callBack2 = new ScoreSaveResponse();
	void Start(){
		
		App42API.Initialize (cons.apiKey, cons.secretKey);  
		userService = App42API.BuildUserService ();
		storageService = App42API.BuildStorageService ();
		loginstatus.text = "";
		//clearLoginStatus ();
		PlayerPrefs.SetString("login_status","False");
		Debug.Log (PlayerPrefs.GetString("login_status"));
		if (anim == null)
			return;
		else if(anim != null && PlayerPrefs.GetString("login_status").Equals("True"))
			OpenPanel (anim);
	}

	public void clearLoginStatus() {
		PlayerPrefs.SetString ("login_status", "False");
	}

	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);

		if (initiallyOpen == null)
			return;
		/*if (PlayerPrefs.GetString ("login_status").Equals ("False"))
			OpenPanel (initiallyOpen);
		else*/
		OpenPanel (initiallyOpen);
	}

	public void OpenPanel (Animator anim)
	{
		if (m_Open == anim)
			return;

		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

		CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;

		m_Open = anim;
		m_Open.SetBool(m_OpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;

		m_Open.SetBool(m_OpenParameterId, false);
		SetSelected(m_PreviouslySelected);
		StartCoroutine(DisablePanelDeleyed(m_Open));
		m_Open = null;
	}

	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}

	public void LoginUser(Animator anim){
		//OpenPanel (anim);
		
		if (username1.text.Equals("") && password1.text.Equals("")) {
			loginstatus.text = "Please Provide Username and Password...";
			loginstatus.color = Color.red;
		}else if(username1.text.Equals("") || password1.text.Equals("")){
			loginstatus.text = "Username or Password is not Provided...";
			loginstatus.color = Color.red;
		} 
		else {
			loginstatus.color = Color.white;
			loginstatus.text = "Please wait while Verifying Credintials ....";
			//OpenPanel (anim);
			StartCoroutine (SignIn (anim));
		}
	}

	public void SignUPUser(){
		if (username1.text.Equals ("") && password1.text.Equals ("")) {
			loginstatus.text = "Please Provide Username and Password...";
			loginstatus.color = Color.red;
		} else if (username1.text.Equals ("") || password1.text.Equals ("")) {
			loginstatus.text = "Username or Password is not Provided...";
			loginstatus.color = Color.red;
		} else {
			loginstatus.color = Color.white;
			loginstatus.text = "Please wait while Creating New User.....";
			StartCoroutine (SignUp ());
		}
		
	}
	IEnumerator SignIn (Animator anim)
	{
		App42Log.SetDebug (true);
		//App42Log.SetDebug(true);        //Print output in your editor console   
		userService.Authenticate (username1.text, password1.text,callBack);
		Debug.Log ("IN Enumerator " + callBack.getResult ());
		while (callBack.getResult () == 0) {
			yield return new WaitForSeconds (0.5f);
		}
		if (callBack.getResult () == 1) {
			PlayerPrefs.SetString ("login_status","True");
			PlayerPrefs.SetString ("name",username1.text);
			PlayerPrefs.Save ();
			Debug.Log (PlayerPrefs.GetString("login_status"));
			OpenPanel (anim);
		}
		else if (callBack.getResult () == 2) {
			//loginstatus.text = "Something wen't \n wrong try again.....";
			//loginstatus.color = Color.red;
			callBack.setResult ();
		}
		else if (callBack.getResult () == 2002) {
			loginstatus.text = "UserName/Password did  not match. Authentication Failed.";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}else if(callBack.getResult() == 2000){
			loginstatus.text = "User by the name "+username1.text+" does not exist. ";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}else if(callBack.getResult() == 2006){
			loginstatus.text = "Users do not exist.";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}
	}
	IEnumerator SignUp ()
	{  
		userService.CreateUser (username1.text, password1.text,username1.text+"@gmail.com", callBack);
		Debug.Log ("IN Enumerator " + callBack.getResult ());
		while (callBack.getResult () == 0) {
			yield return new WaitForSeconds (2);
		}
		if (callBack.getResult () == 1)
			loginstatus.text = "User Created \nSuccessfully....";
		else if (callBack.getResult () == 2) {
			loginstatus.text = "Something wen't \n wrong try again.....";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}
		else if (callBack.getResult () == 2001) {
			loginstatus.text = "The request parameters are invalid. Username "+username1.text+" already exists.";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}else if(callBack.getResult() == 2000){
			loginstatus.text = "User by the name "+username1.text+" does not exist. ";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}else if(callBack.getResult() == 2006){
			loginstatus.text = "Users do not exist.";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}
	}
	public void NextActivity(){
		OpenPanel (animator);
	}

	public void GetHighScores(Animator anim){
		StartCoroutine (GetHighScoresForDB(anim));
	}
	IEnumerator GetHighScoresForDB (Animator anim)
	{
		//App42Log.SetDebug(true);        //Print output in your editor console
		Query query = QueryBuilder.Build("score","0",Operator.GREATER_THAN);
		storageService.FindDocsWithQueryPagingOrderBy("SCORES","HighScores",query,1,0,OrderByType.ASCENDING,"name",callBack1);
		//FindDocsWithQueryPagingOrderBy("SCORES", "HighScores",max, offset, key1, OrderByType.ASCENDING, new UnityCallBack());   
		Debug.Log ("IN Enumerator " + callBack.getResult ());
		while (callBack.getResult () == 0) {
			yield return new WaitForSeconds (0.5f);
		}
		if (callBack.getResult () == 1) {
			PlayerPrefs.SetString ("login_status","True");
			PlayerPrefs.Save ();
			Debug.Log (PlayerPrefs.GetString("login_status"));
			OpenPanel (anim);
		}
		else if (callBack.getResult () == 2) {
			loginstatus.text = "Something wen't \n wrong try again.....";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}
		else if (callBack.getResult () == 3) {
			loginstatus.text = "Username/Password \n Provided is Wrong oR\n Try Again....";
			loginstatus.color = Color.red;
			callBack.setResult ();
		}
	}

	public void SinglePlayer(){
		SceneManager.LoadScene ("SinglePlayerScene");
	}
	public void SaveScore(){
		StartCoroutine (SaveScoreForUser());
	}
	IEnumerator SaveScoreForUser ()
	{
		string json = "{\"name\":\"Name1\",\"score\":\"score1\"}";
		string json1 = json.Replace ("Name1",PlayerPrefs.GetString("name")).Replace("score1","600");
		storageService.InsertJSONDocument("SCORES", "HighScores", json1, callBack2);
		Debug.Log ("IN Enumerator " + callBack2.getResult ());
		while (callBack2.getResult () == 0) {
			yield return new WaitForSeconds (0.5f);
		}
		if (callBack2.getResult () == 1) {
			Debug.Log ("Score Saved Successful");
		}
		else if (callBack2.getResult () == 2) {
			ScoreField.text = "Something wen't Wrong Please Try again";
			callBack.setResult ();
		}
	}

	public void SetStatus(string value){
		loginstatus.text = value;
	}
}
