using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using AssemblyCSharp;
public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen,animator;

	private int m_OpenParameterId;
	private Animator m_Open;
	private GameObject m_PreviouslySelected;
	public InputField username1,password1;
	public Text loginstatus;
	UserService userService;
	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	Constant cons = new Constant ();
	SignInResponse callBack = new SignInResponse ();

	void Start(){

		App42API.Initialize (cons.apiKey, cons.secretKey);  
		userService = App42API.BuildUserService (); 
	}
	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);

		if (initiallyOpen == null)
			return;

		OpenPanel(initiallyOpen);
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
		loginstatus.text = "Please wait while \n Verifying Credintials ....";
		StartCoroutine (SignIn(anim));
	}

	public void SignUPUser(){
		loginstatus.text = "Please wait whil \nCreating New User.....";
		StartCoroutine (SignUp());
		
	}
	IEnumerator SignIn (Animator anim)
	{
		//App42Log.SetDebug(true);        //Print output in your editor console   
		userService.Authenticate (username1.text, password1.text, callBack);
		Debug.Log ("IN Enumerator " + callBack.getResult ());
		while (callBack.getResult () == 0) {
			yield return new WaitForSeconds (2);
		}
		if (callBack.getResult () == 1)
			OpenPanel (anim);
		else if (callBack.getResult () == 2) {
			loginstatus.text = "Something wen't \n wrong try again.....";
			loginstatus.color = Color.red;
		}
		else if (callBack.getResult () == 3) {
			loginstatus.text = "Username/Password \n Provided is Wrong....";
			loginstatus.color = Color.red;
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
		}
		else if (callBack.getResult () == 3) {
			loginstatus.text = "Username Providedx`x Will\n Already Exists Please \nChange And Try....";
			loginstatus.color = Color.red;
		}
	}
	public void NextActivity(){
		OpenPanel (animator);
	}

}
