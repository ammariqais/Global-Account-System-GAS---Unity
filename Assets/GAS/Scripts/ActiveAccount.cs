using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ActiveAccount : MonoBehaviour {
	[SerializeField] private InputField Code;
	[SerializeField] private Text WarningMSG;
	[SerializeField] public string activeURL ;
	[SerializeField] public string resendURL ;

	[SerializeField] public string securePassword;
	private UiAccountManager GetLoginCanvas;


	// Use this for initialization
	void Start () {
		GetLoginCanvas = gameObject.GetComponent<UiAccountManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Resend(){
		StartCoroutine (Query_Php("Resend"));
	}

	public void Active(){
		StartCoroutine (Query_Php("Active"));
	}
	IEnumerator Query_Php(string type){
		if (type == "Active") {
			WWW query = new WWW (activeURL + "?" + "code=" + Code.text + "&secure=" + securePassword);
			yield return query;
			if (query.text.Trim () == "1") {
				GetLoginCanvas.ToggleCanvas ("login");
			} else {
				WarningMSG.text = query.text;
			}
		} else if (type == "Resend") {
			string getTempAccount = PlayerPrefs.GetString ("TempUser");
			WWW query = new WWW (resendURL + "?" + "username=" + getTempAccount + "&secure=" + securePassword);
			yield return query;
			if (query.text.Trim () == "1") {
				WarningMSG.color = Color.green;
				WarningMSG.text = "Send Code To your Email";
			} else {
				WarningMSG.color = Color.red;

				WarningMSG.text = query.text;
			}
		}
	}
}
