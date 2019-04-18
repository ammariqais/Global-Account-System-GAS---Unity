using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResetPassword : MonoBehaviour {
	[SerializeField] private InputField Code;
	[SerializeField] private InputField Password;
	[SerializeField] public string ResetUrl;
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
	public void Reset(){
		StartCoroutine (Query_Php());
	}

	IEnumerator Query_Php(){
		WWW query = new WWW (ResetUrl+"?"+"code="+Code.text+"&password="+Password.text+"&secure="+ securePassword);
		yield return query;
		if (query.text.Trim () == "1") {
			GetLoginCanvas.ToggleCanvas ("login");
		} else {
			WarningMsg.text = query.text;
		}
	}



}
