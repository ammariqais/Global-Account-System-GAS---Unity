using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class forgetPassword : MonoBehaviour {
	[SerializeField] public string UrlSendCode;
	[SerializeField] private InputField email;
	[SerializeField] private Text WarningMsg;
	private UiAccountManager GetLoginCanvas;
	[SerializeField] public string securePassword;

	// Use this for initialization
	void Start () {
		GetLoginCanvas = gameObject.GetComponent<UiAccountManager>();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void sendCode(){
		StartCoroutine (Query_php ());
	}

	IEnumerator Query_php(){
		WWW query = new WWW(UrlSendCode+"?email="+email.text+"&secure="+ securePassword);
		yield return query;
		if (query.text.Trim() == "1") {
			GetLoginCanvas.ToggleCanvas ("reset");
		} else {
			WarningMsg.text = query.text;
		}
	}
}
