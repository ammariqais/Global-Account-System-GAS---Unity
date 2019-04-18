using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL)
using System.Net.NetworkInformation;
#endif

public class LoginSystem : MonoBehaviour {
	[SerializeField] private InputField userName;
	[SerializeField] private InputField passwordName;
	[SerializeField] public string LoginUrl;
	[SerializeField] public string checkBannedUrl;
	private string info;
	[SerializeField] public string SecureKey;
	[SerializeField] private Text WarningMsg;
	private UiAccountManager GetLoginCanvas;

	// Use this for initialization
	void Start () {		
		GetLoginCanvas = gameObject.GetComponent<UiAccountManager>();
		#if UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
		ShowNetworkInterfaces ();
		#endif

	}



	// Update is called once per frame
	void Update () {

	}

	public void Login (){
		StartCoroutine (Query_Account ());
	}
	public void ShowNetworkInterfaces()
	{
		#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_IOS && !UNITY_ANDROID)

		IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
		NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

		foreach (NetworkInterface adapter in nics)
		{
			PhysicalAddress address = adapter.GetPhysicalAddress();
			byte[] bytes = address.GetAddressBytes();
			string mac = null;
			for (int i = 0; i < bytes.Length; i++)
			{
				mac = string.Concat(mac +(string.Format("{0}", bytes[i].ToString("X2"))));
				if (i != bytes.Length - 1)
				{
					mac = string.Concat(mac + "-");
				}
			}
			info += mac ;

		}
		#endif

	}
	IEnumerator Query_Account (){
		#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_ANDROID ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)

		WWW checkBanned = new WWW (checkBannedUrl + "?" + "mac=" + info.Trim() +"&secureid="+SecureKey);

		#endif

		#if UNITY_WEBGL || UNITY_WEBPLAYER || UNITY_ANDROID || UNITY_IOS
		WWW checkBanned = new WWW (checkBannedUrl + "?" + "ip=1&secureid="+SecureKey);

		#endif


		WarningMsg.text = "Check White User... ";
		yield return checkBanned;
		Debug.Log (checkBanned.text);
		if (checkBanned.text.Trim () == "1" || checkBanned.text.Trim () == "3") {
			WWW query = new WWW (LoginUrl + "?" + "username=" + userName.text + "&password=" + passwordName.text + "&secureid=" + SecureKey);
			WarningMsg.text = "Please Wait ... ";
			yield return query;
			string[] split = query.text.Split (',');
			if (split [0].Trim () == "1") {


				PlayerPrefs.SetInt ("userID", int.Parse (split [1].Trim ()));
				PlayerPrefs.SetString ("firstname", split [2].Trim ());
				PlayerPrefs.SetString ("lastname", split [3].Trim ());
				PlayerPrefs.SetString ("country", split [4].Trim ());
				PlayerPrefs.SetString ("username", split [5].Trim ());
				PlayerPrefs.SetInt ("age", int.Parse (split [6].Trim ()));
				PlayerPrefs.SetString ("email", split [7].Trim ());

				// After Login do what you want ex. load new scene ...
				Application.LoadLevel (1);


			} else if (split [0].Trim () == "2") {
				PlayerPrefs.SetString ("TempUser", userName.text.Trim ());
				GetLoginCanvas.ToggleCanvas ("active");
			} else {
				WarningMsg.text = split [0];


			}
		} else if (checkBanned.text.Trim () == "2") {

			WarningMsg.text = "BANNED";

		} else if (checkBanned.text.Trim () == "3") {
			WarningMsg.text = "Please Fill All Field";
		}

		else {
			WarningMsg.text = checkBanned.text;
		}

	}
}

