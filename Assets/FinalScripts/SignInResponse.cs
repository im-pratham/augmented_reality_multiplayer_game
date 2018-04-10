using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
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
	}
}

