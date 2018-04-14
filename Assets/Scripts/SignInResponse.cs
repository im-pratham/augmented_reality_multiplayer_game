using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
namespace AssemblyCSharp
{
	public class SignInResponse:App42CallBack
	{
		public static int result = 0;
		public void OnSuccess(object user)
		{
			try
			{
				if (user is User)
				{
					User userObj = (User)user;
					Debug.Log ("UserName : " + userObj.GetUserName());
					Debug.Log ("EmailId : " + userObj.GetEmail());
					Debug.Log("SingInResponse "+PlayerPrefs.GetString("login_status"));
					result = 1;
				}
			}
			catch (App42Exception e)
			{
				result = 2;
				Debug.Log ("App42Exception : "+ e);
			}
			Debug.Log (String.Format("Result {0}",result));
		}

		public void OnException(Exception e)
		{
			result = 3;
			Debug.Log ("Exception : " + e);
		}

		public int getResult() {
			return result;
		}	
		public void setResult(){
			result = 0;
		}
	}
	public class ScoreResponse:App42CallBack{
		public void OnSuccess(object response)  
		{  
			Storage storage = (Storage) response;  
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();   
			for(int i=0;i <jsonDocList.Count;i++)  
			{     
				App42Log.Console("objectId is " + jsonDocList[i].GetDocId());  
				App42Log.Console("jsonDoc is " + jsonDocList[i].GetJsonDoc());  
			}    
		}  

		public void OnException(Exception e)  
		{  
			App42Log.Console("Exception : " + e);  
		}  
	}
}

