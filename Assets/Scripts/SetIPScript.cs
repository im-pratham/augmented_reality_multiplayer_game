using UnityEngine;
using UnityEngine.UI; // The namespace for the UI stuff.
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class SetIPScript : MonoBehaviour {

	private Text m_TextComponent;

	void Awake()
	{
		// Get a reference to the text component
		m_TextComponent = GetComponent<Text>();

		m_TextComponent.text = "connect using: " + LocalIPAddress();
	}
	public string LocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}
}
