using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	GameObject HS1, HS2, HS3, HS4, HS5;

    public void PlayGame ()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void QuitGame ()
	{
		Debug.Log("Quit");
		PlayerPrefs.DeleteAll();
		Application.Quit();
	}

	public void Start () 
	{
		HS1 = GameObject.Find("HS1");
		HS2 = GameObject.Find("HS2");
		HS3 = GameObject.Find("HS3");
		HS4 = GameObject.Find("HS4");
		HS5 = GameObject.Find("HS5");
    }

    public void Update ()
	{
		if (PlayerPrefs.HasKey(0+"HScore"))
			HS1.GetComponent<Text>().text = 1 + ". " + PlayerPrefs.GetString(0+"HScoreName") + ": " + PlayerPrefs.GetInt(0+"HScore");
		if (PlayerPrefs.HasKey(1+"HScore"))
			HS2.GetComponent<Text>().text = 2 + ". " + PlayerPrefs.GetString(1+"HScoreName") + ": " + PlayerPrefs.GetInt(1+"HScore");
		if (PlayerPrefs.HasKey(2+"HScore"))
			HS3.GetComponent<Text>().text = 3 + ". " + PlayerPrefs.GetString(2+"HScoreName") + ": " + PlayerPrefs.GetInt(2+"HScore");
		if (PlayerPrefs.HasKey(3+"HScore"))
			HS4.GetComponent<Text>().text = 4 + ". " + PlayerPrefs.GetString(3+"HScoreName") + ": " + PlayerPrefs.GetInt(3+"HScore");
		if (PlayerPrefs.HasKey(4+"HScore"))
			HS5.GetComponent<Text>().text = 5 + ". " + PlayerPrefs.GetString(4+"HScoreName") + ": " + PlayerPrefs.GetInt(4+"HScore");

	}
}
