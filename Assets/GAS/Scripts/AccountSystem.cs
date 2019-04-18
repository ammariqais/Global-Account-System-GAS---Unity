using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
[Serializable]
public class AccountSystem : MonoBehaviour {
	[SerializeField] private InputField firstName;
	[SerializeField] private InputField lastName;
	[SerializeField] private InputField Age;
	[SerializeField] private InputField Country;
	[SerializeField] private InputField email;

	[SerializeField] private InputField Password;

	[SerializeField] private Text WarningMSG;
	[SerializeField] public string UpdateURL ;
	[SerializeField] public string RefreshURL ;

	[SerializeField] public string securePassword;
	[SerializeField] private int minAge;
	[SerializeField] private int maxAge;
	[SerializeField] private int minPassword;
	[SerializeField] private bool nameRequierd;
	[SerializeField] private bool ageRequierd;
	[SerializeField] private bool countryRequierd;
	private string userID;
	// Use this for initialization
	void Start () {
		userID = PlayerPrefs.GetInt ("userID").ToString();

		firstName.text = PlayerPrefs.GetString ("firstname");
		lastName.text = PlayerPrefs.GetString ("lastname");
		email.text = PlayerPrefs.GetString ("email");
		Age.text = PlayerPrefs.GetInt ("age").ToString();
		Country.text = PlayerPrefs.GetString ("country");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Refresh(){

		StartCoroutine (RefreshAccount ());
	}

	IEnumerator RefreshAccount(){
		WWW query = new WWW (RefreshURL + "?id=" + userID+"&secure="+ securePassword);
		WarningMSG.text = "Please Wait ... ";
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

			WarningMSG.text = "Refresh Done";
			// After Refesh call what you want
			Application.LoadLevel(1);


		} else {

			WarningMSG.text = split [0];

		}
	}

	public void UpdateAccount(string Query){
		StartCoroutine (Query_Php(Query));
	}


	IEnumerator Query_Php(string Query){

		if (Query == "password") {
			if (Password.text.Length < minPassword) {
				WarningMSG.color = Color.red;

				WarningMSG.text = "The password most be bigger than " + minPassword + " characters";
				Debug.Log (nameRequierd);
			} else {
				WWW CallPHP = new WWW (UpdateURL + "?id=" + userID + "&newpassword=" + Password.text + "&changePassword=1"+"&secure="+ securePassword);
				yield return CallPHP;
				if (CallPHP.text.Trim () == "1") {
					WarningMSG.color = Color.green;
					WarningMSG.text = "Password is Updated";
				} else {
					WarningMSG.color = Color.red;

					WarningMSG.text = CallPHP.text;

				}
			}
		} else if (Query == "information") {
			if (nameRequierd && (firstName.text.Length < 1 || lastName.text.Length < 1)) {
				WarningMSG.text = "first name and last name is reqiuerd!";
			} else if (ageRequierd && (Age.text.Length < 1)) {

				WarningMSG.text = "Age is Requierd";

			} else if (countryRequierd && Country.text.Length < 1) {

				WarningMSG.text = "Country is Requierd";

			} else {
				if (Age.text.Length < 1) {
					Age.text = "0";
				}
				if ((minAge > int.Parse (Age.text) || maxAge < int.Parse (Age.text)) && Age.text != "0") {
					WarningMSG.text = "Wrong Age";
				} else {
					WWW CallPHP = new WWW (UpdateURL + "?id=" + userID + "&firstname=" + firstName.text + "&lastname=" + lastName.text + "&country=" + Country.text + "&age=" + Age.text + "&email=" + email.text +"&secure="+ securePassword+"&changeInformation=1");
					yield return CallPHP;
					if (CallPHP.text.Trim () == "1") {
						WarningMSG.color = Color.green;
						WarningMSG.text = "Information is Updated";
					} else {
						WarningMSG.color = Color.red;

						WarningMSG.text = CallPHP.text;

					}

				}
			
			}
	}
}
}