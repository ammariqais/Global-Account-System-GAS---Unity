using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)
using System.Net.NetworkInformation;
#endif
using System.Net;
using System.Collections.Generic;
using System;
[Serializable]
public class RegisterSystem : MonoBehaviour {
	[SerializeField] private InputField firstName;
	[SerializeField] private InputField lastName;
	[SerializeField] private InputField Age;
	[SerializeField] private InputField Country;
	[SerializeField] private InputField userName;
	[SerializeField] private InputField Email;
	[SerializeField] private InputField Password;
	[SerializeField] private Text WarningMSG;
	[SerializeField] public string RegisterUrl ;

	[SerializeField] public  string checkBannedUrl;
	[SerializeField] public string securePassword;
	[SerializeField] private int minAge;
	[SerializeField] private int maxAge;
	[SerializeField] private int minPassword;
	[SerializeField] private bool nameRequierd;
	[SerializeField] private bool ageRequierd;
	[SerializeField] private bool countryRequierd;
	[SerializeField] private bool ActiveFromEmail;
	[SerializeField] private bool MultiAccounts;

	private string info;


	private UiAccountManager GetLoginCanvas;

	// Use this for initialization
	void Start () {



		#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX  ) && !UNITY_WEBPLAYER
		ShowNetworkInterfaces ();

		#endif
		GetLoginCanvas = gameObject.GetComponent<UiAccountManager>();
	}

	// Update is called once per frame
	void Update () {

	}


	public void ShowNetworkInterfaces()
	{
		#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX  ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)

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

	public void Register(){

		StartCoroutine (checkBanned ());


	}

	IEnumerator checkBanned(){

		#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX  ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)

		WWW checkBanned = new WWW (checkBannedUrl + "?" + "mac=" + info.Trim() +"&secureid="+securePassword);
		WarningMSG.text = "Check White User... ";
		yield return checkBanned;
		if (checkBanned.text.Trim () == "3" || (MultiAccounts && checkBanned.text.Trim () == "1")) {
			if (nameRequierd && (firstName.text.Length < 1|| lastName.text.Length < 1)) {
				WarningMSG.text = "first name and last name is reqiuerd!";
			}else if(ageRequierd && (Age.text.Length < 1)){

				WarningMSG.text = "Age is Requierd";

			}else if(countryRequierd && Country.text.Length < 1){

				WarningMSG.text = "Country is Requierd";

			}else{
				if (Age.text.Length < 1) {
					Age.text = "0";
				}
				if ((minAge > int.Parse(Age.text) || maxAge < int.Parse(Age.text))&& Age.text!="0") {
					WarningMSG.text = "Wrong Age";
				}
				else {
					if (Password.text.Length < minPassword) {
						WarningMSG.text = "The password most be bigger than " + minPassword + " characters";
					} else {

						StartCoroutine (CreateAccount ());
					}
				}
			}
		} else {
			if (checkBanned.text.Trim () == "1") {

				WarningMSG.text = "you have already account in this device"; 
			} else {
				if (checkBanned.text.Trim () == "2") {
					WarningMSG.text = "this Device is Banned";
				} else {
					WarningMSG.text = checkBanned.text;
				}

			}

		}
		#endif



		#if UNITY_WEBGL || UNITY_WEBPLAYER || UNITY_ANDROID || UNITY_IOS
		WWW checkBanned_WEBGL = new WWW (checkBannedUrl + "?" + "ip=1&secureid="+securePassword);
		WarningMSG.text = "Check White User... ";
		yield return checkBanned_WEBGL;
		if (checkBanned_WEBGL.text.Trim () == "3" || (MultiAccounts && checkBanned_WEBGL.text.Trim () == "1")) {
		if (nameRequierd && (firstName.text.Length < 1|| lastName.text.Length < 1)) {
		WarningMSG.text = "first name and last name is reqiuerd!";
		}else if(ageRequierd && (Age.text.Length < 1)){

		WarningMSG.text = "Age is Requierd";

		}else if(countryRequierd && Country.text.Length < 1){

		WarningMSG.text = "Country is Requierd";

		}else{
		if (Age.text.Length < 1) {
		Age.text = "0";
		}
		if ((minAge > int.Parse(Age.text) || maxAge < int.Parse(Age.text))&& Age.text!="0") {
		WarningMSG.text = "Wrong Age";
		}
		else {
		if (Password.text.Length < minPassword) {
		WarningMSG.text = "The password most be bigger than " + minPassword + " characters";
		Debug.Log (nameRequierd);
		} else {
		StartCoroutine (CreateAccount ());
		}
		}
		}
		} else {
		if (checkBanned_WEBGL.text.Trim () == "1") {

		WarningMSG.text = "you have already account in this device"; 
		} else {
		if (checkBanned_WEBGL.text.Trim () == "2") {
		WarningMSG.text = "BANNED";
		} else {
		WarningMSG.text = checkBanned_WEBGL.text;
		}

		}

		}

		#endif

	}


	IEnumerator CreateAccount(){

		if (!ActiveFromEmail) {
			#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX  ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)

			string sends = RegisterUrl + "?" + "firstname=" + firstName.text.Trim () + "&lastname=" + lastName.text.Trim () + "&age=" + Age.text.Trim () + "&country=" + Country.text.Trim () + "&username=" + userName.text.Trim () + "&email=" + Email.text.Trim () + "&password=" + Password.text.Trim () + "&mac=" + info.Trim () + "&secure=" + securePassword.Trim ();
			#endif	
			#if UNITY_WEBGL || UNITY_WEBPLAYER || UNITY_ANDROID || UNITY_IOS

			string sends = RegisterUrl + "?" + "firstname=" + firstName.text.Trim () + "&lastname=" + lastName.text.Trim () + "&age=" + Age.text.Trim () + "&country=" + Country.text.Trim () + "&username=" + userName.text.Trim () + "&email=" + Email.text.Trim () + "&password=" + Password.text.Trim () + "&mac=1&secure=" + securePassword.Trim ();

			#endif
			WWW php_query = new WWW (sends);
			yield return php_query;
			if (php_query.text.Trim () == "1") {

				GetLoginCanvas.ToggleCanvas ("login");
			} else {
				WarningMSG.text = php_query.text;
			}

		} else {
			#if (UNITY_STANDALONE || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX  ) && (!UNITY_WEBPLAYER && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS)
			string sends = RegisterUrl + "?" + "firstname=" + firstName.text.Trim () + "&lastname=" + lastName.text.Trim () + "&age=" + Age.text.Trim () + "&country=" + Country.text.Trim () + "&username=" + userName.text.Trim () + "&email=" + Email.text.Trim () + "&password=" + Password.text.Trim () + "&mac=" + info.Trim () + "&secure=" + securePassword.Trim () + "&activate=1";
			#endif	
			#if UNITY_WEBGL || UNITY_WEBPLAYER || UNITY_ANDROID || UNITY_IOS  

			string sends = RegisterUrl + "?" + "firstname=" + firstName.text.Trim () + "&lastname=" + lastName.text.Trim () + "&age=" + Age.text.Trim () + "&country=" + Country.text.Trim () + "&username=" + userName.text.Trim () + "&email=" + Email.text.Trim () + "&password=" + Password.text.Trim () + "&mac=1&secure=" + securePassword.Trim () + "&activate=1";

			#endif	


			Debug.Log (sends);
			WWW php_query = new WWW (sends);
			yield return php_query;
			if (php_query.text.Trim () == "1") {

				GetLoginCanvas.ToggleCanvas ("login");
			} else {
				WarningMSG.text = php_query.text;
				Debug.Log (php_query.text);
			}
		}


	}

}
