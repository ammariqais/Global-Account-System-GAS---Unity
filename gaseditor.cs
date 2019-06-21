using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class gaseditor : EditorWindow {

	//private static GameObject AccountManager;
	private static EditorWindow success_window;
	private static EditorWindow success_window2;

	private static EditorWindow _window;
	private static EditorWindow ngo_window;
	string URL = "http://localhost/gas_php";
	string Key = "dfed34rrfqwdxscgvcxscsfdcfwe";

	SerializedProperty damageProp;
	SerializedProperty armorProp;
	SerializedProperty gunProp;
	private const string AccountManagerPath = "Assets/GAS/Prefabs/AccountManager.prefab";
	private const string ManagerPath = "Assets/GAS/Prefabs/Manager.prefab";

	private LoginSystem LoginManager;
	private RegisterSystem RegisterManager;
	private forgetPassword ForgetManager;
	private ResetPassword ResetManager;
	private ActiveAccount ActiveManager;
	private AccountSystem AccountM;
	private GameObject AccountManager;
	private GameObject MAS;

	private RegisterSystem Manager ;
	private forgetPassword ManagerForget ;
	private LoginSystem ManagerLogin ;
	private ResetPassword ManagerReset ;
	private ActiveAccount ManagerActive ;

	private SerializedObject star;
	private SerializedObject serializedObject;
	[MenuItem("Window/Global Account System/Configure")]
	private static void ShowGasSetup() {

		_window = (gaseditor)EditorWindow.GetWindow (typeof(gaseditor), true, "GAS Configure v1.0.0 Beta!");


	}

	[MenuItem("GameObject/Create Material")]
	private static void CreateMaterial () {
		// Create a simple material asset
		var material = new Material (Shader.Find("Specular"));
		AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");

		// Print the path of the created asset
		Debug.Log(AssetDatabase.GetAssetPath(material));
	}



	void Awake()
	{
		var AM = AssetDatabase.LoadAssetAtPath<GameObject> (AccountManagerPath);
		Manager = AM.GetComponent<RegisterSystem> ();
		ManagerForget = AM.GetComponent<forgetPassword> ();
		ManagerLogin = AM.GetComponent<LoginSystem> ();
		ManagerReset = AM.GetComponent<ResetPassword> ();
		ManagerActive = AM.GetComponent<ActiveAccount> ();
	}
	string MSG = "                            ";
	string MSG2 = "                            ";
	void OnGUI () {
		if (_window != null) {
			GUI.contentColor = Color.red;  // Apply Red color to Button
			GUILayout.Label ("PHP Source Folder & Secure Key", EditorStyles.boldLabel);

			URL = EditorGUILayout.TextField ("Folder URL", URL);
			Key = EditorGUILayout.TextField ("Secure Key", Key);

			GUILayout.Label ("First Step : Before Login", EditorStyles.boldLabel);
			GUILayout.Label (MSG);

			AccountManager = (GameObject)EditorGUILayout.ObjectField("Account Manager:", AccountManager, typeof(GameObject), true);
			if (GUILayout.Button ("Configure ! STEP 1") && AccountManager != null) {


		

				LoginManager = AccountManager.GetComponent<LoginSystem> ();
				RegisterManager = AccountManager.GetComponent<RegisterSystem> ();
				ForgetManager = AccountManager.GetComponent<forgetPassword> ();
				ResetManager = AccountManager.GetComponent<ResetPassword> ();
				ActiveManager = AccountManager.GetComponent<ActiveAccount> ();
				//			AccountM = Manager.GetComponent<AccountSystem>();

				PlayerPrefs.SetString ("registerURL", URL + "/register.php");

				PlayerPrefs.SetString ("LoginUrl", URL + "/login.php");
				ManagerLogin.LoginUrl = URL + "/login.php";
				LoginManager.LoginUrl = URL + "/login.php";
				ManagerLogin.SecureKey = Key;
				ManagerLogin.checkBannedUrl = URL + "/checkbanned.php";
				ManagerLogin.checkBannedUrl = URL + "/checkbanned.php";
				Manager.securePassword = Key;
				Manager.checkBannedUrl = URL + "/checkbanned.php";
				Manager.RegisterUrl = URL + "/register.php";

				RegisterManager.checkBannedUrl = URL + "/checkbanned.php";
				ManagerForget.UrlSendCode = URL + "/sendcode.php";
				ForgetManager.UrlSendCode = URL + "/sendcode.php";
				ManagerForget.securePassword = Key;
			ManagerReset.ResetUrl = URL + "/resetpassword.php";
				ResetManager.ResetUrl = URL + "/resetpassword.php";
				ManagerReset.securePassword = Key;
			ManagerActive.activeURL = URL + "/activeaccount.php";
				ManagerActive.resendURL = URL + "/resendactive.php";
				ManagerActive.securePassword = Key;
				ActiveManager.activeURL = URL + "/activeaccount.php";
				ActiveManager.resendURL = URL + "/resendactive.php";

	
				PlayerPrefs.SetString ("UrlSendCode", URL + "/sendcode.php");

				PlayerPrefs.SetString ("ResetUrl", URL + "/resetpassword.php");

				PlayerPrefs.SetString ("UpdateURL", URL + "/update.php");

				PlayerPrefs.SetString ("RefreshURL", URL + "/refresh.php");
				PlayerPrefs.SetString ("ActiveAccountURL", URL + "/activeaccount.php");
				PlayerPrefs.SetString ("CheckBanned", URL + "/checkbanned.php");
				PlayerPrefs.SetString ("ResendActive", URL + "/resendactive.php");



				_window.Close ();
				success_window = (gaseditor)EditorWindow.GetWindow (typeof(gaseditor), true, "Done !");
				//Manager.registerURL = "ololo";
				//Manager.oo = ":";
		
				EditorUtility.SetDirty (AccountManager);

				AssetDatabase.Refresh ();

			} 

			GUILayout.Label ("Second Step: After Login", EditorStyles.boldLabel);
			GUILayout.Label (MSG2);

			MAS = (GameObject)EditorGUILayout.ObjectField("Manager GameObject:", MAS, typeof(GameObject), true);


			if (GUILayout.Button ("Configure ! STEP 2") && MAS != null) {

				AccountM = MAS.GetComponent<AccountSystem>();

	
				AccountM.UpdateURL = URL + "/update.php";
				AccountM.RefreshURL = URL + "/refresh.php";
				AccountM.securePassword = Key;
				//ManagerLogin.LoginUrl = URL + "/login.php";
			//	ManagerLogin.checkBannedUrl = URL + "/checkbanned.php";
				//Manager.registerURL = URL + "/register.php";
				//Manager.checkBannedUrl = URL + "/checkbanned.php";
			//	ManagerForget.UrlSendCode = URL + "/sendcode.php";
			//	ManagerReset.ResetUrl = URL + "/resetpassword.php";
			//	ManagerActive.activeURL = URL + "/activeaccount.php";
			//	ManagerActive.resendURL = URL + "/resendactive.php";
				PlayerPrefs.SetString ("UpdateURL", URL + "/update.php");

				PlayerPrefs.SetString ("RefreshURL", URL + "/refresh.php");



				_window.Close ();
				success_window2 = (gaseditor)EditorWindow.GetWindow (typeof(gaseditor), true, "Done !");
				//Manager.registerURL = "ololo";
				//Manager.oo = ":";

				EditorUtility.SetDirty (MAS);

				AssetDatabase.Refresh ();

			} 




			if (AccountManager == null) {
		
				//GUI.contentColor = Color.red;
				MSG = "Please Attach Account Manager !";
			
			} else {
				MSG = "Good !";

			}

			if (MAS == null) {

				GUI.contentColor = Color.red;
				MSG2 = "Please Attach Manager !";

			} else {
				MSG2 = "Good !";

			}
		}
		if (success_window != null) {

			GUILayout.Label ("We Got These :", EditorStyles.boldLabel);

				GUILayout.Label ("Register URL : " + PlayerPrefs.GetString ("registerURL"));
				GUILayout.Label ("Login URL : " + PlayerPrefs.GetString ("LoginUrl"));
				GUILayout.Label ("Send URL : " + PlayerPrefs.GetString ("UrlSendCode"));
				GUILayout.Label ("Reset URL : " + PlayerPrefs.GetString ("ResetUrl"));
				GUILayout.Label ("Active Account URL : " + PlayerPrefs.GetString ("ActiveAccountURL"));
				GUILayout.Label ("Check Banned URL : " + PlayerPrefs.GetString ("CheckBanned"));
				GUILayout.Label ("Resend Active URL : " + PlayerPrefs.GetString ("ResendActive"));




			if (GUILayout.Button ("Seems Good  !")) {

				success_window.Close ();
			}
			if (GUILayout.Button ("Nope ! , Re-Setup")) {
				success_window.Close ();
				_window = (gaseditor)EditorWindow.GetWindow (typeof(gaseditor), true, "GAS Database Setup");
			}

		}



		if (success_window2 != null) {

			GUILayout.Label ("We Got These :", EditorStyles.boldLabel);


			GUILayout.Label ("Update URL : " + PlayerPrefs.GetString ("UpdateURL"));
			GUILayout.Label ("Refresh URL : " + PlayerPrefs.GetString ("RefreshURL"));



			if (GUILayout.Button ("Seems Good  !")) {

				success_window2.Close ();
			}
			if (GUILayout.Button ("Nope ! , Re-Setup")) {
				success_window2.Close ();
				_window = (gaseditor)EditorWindow.GetWindow (typeof(gaseditor), true, "GAS Database Setup");
			}

		}
	}

	void OnDestroy(){
		EditorApplication.SaveScene();

//	EditorUtility.SetDirty (Manager);
		EditorUtility.SetDirty (ManagerLogin);
EditorUtility.SetDirty (ManagerForget);
		EditorUtility.SetDirty (ManagerReset);
		EditorUtility.SetDirty (ManagerLogin);
	AssetDatabase.SaveAssets ();


}
}


