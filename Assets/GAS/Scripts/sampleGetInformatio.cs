using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class sampleGetInformatio : MonoBehaviour {
	[SerializeField] private Text firstName ;
	[SerializeField] private Text lastname ; 
	[SerializeField] private Text age ; 
	[SerializeField] private Text email ; 
	[SerializeField] private Text id ; 
	[SerializeField] private Text username ; 
	[SerializeField] private Text country ; 

	// Use this for initialization
	void Start () {
		firstName.text = PlayerPrefs.GetString ("firstname");
		lastname.text = PlayerPrefs.GetString ("lastname");
		email.text = PlayerPrefs.GetString ("email");
		id.text =PlayerPrefs.GetInt ("userID").ToString();
		age.text = PlayerPrefs.GetInt ("age").ToString();
		country.text = PlayerPrefs.GetString ("country");
		username.text = PlayerPrefs.GetString ("username");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
