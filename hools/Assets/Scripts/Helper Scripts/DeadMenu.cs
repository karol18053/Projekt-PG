using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour {

	Text userNameInputText;
	string playername;

	[SerializeField]
	GameObject HS;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundTrack;

    public void PlayGame ()
	{
		if (CheckScore()) {
			SaveScore();
		}
		ScoreScript.scoreValue = 0;
		SceneManager.LoadScene("Gameplay");
	}

	public void QuitGame ()
	{
		if (CheckScore()) {
			SaveScore();
		}
		ScoreScript.scoreValue = 0;
		SceneManager.LoadScene("Menu");
	}
	
	void Start() 
	{
        //audioSource = GetComponent<AudioSource> ();
        //audioSource.clip = soundTrack;
        //audioSource.Play ();

        CheckScore ();
	}

	public void InputYourName() {
		playername = userNameInputText.text;
	}

	bool CheckScore() {
		for(int i=0; i<4; i++) {
			if (PlayerPrefs.HasKey(i+"HScore")) {
				if(PlayerPrefs.GetInt(i+"HScore")<=ScoreScript.scoreValue) {
					HS.SetActive(true);
					return true;
				}
			}
			else
			{
				HS.SetActive(true);
				return true;
			}
		}
		return false;
	}

	void SaveScore() {
		userNameInputText = GameObject.Find("NameInputText").GetComponent<Text>();
		InputYourName();
		Debug.Log("Saving score: " + playername + ": " + ScoreScript.scoreValue);
		int oldScore = 0;
		int newScore = ScoreScript.scoreValue;
		string oldName;
		string newName = playername;
		for(int i=0; i<=4; i++) {
			if (PlayerPrefs.HasKey(i+"HScore")) {
				if(PlayerPrefs.GetInt(i+"HScore")<=newScore) {
					oldScore = PlayerPrefs.GetInt(i+"HScore");
					oldName = PlayerPrefs.GetString(i+"HScoreName");
					PlayerPrefs.SetInt(i+"HScore",newScore);
					PlayerPrefs.SetString(i+"HScoreName",newName);
					newScore = oldScore;
					newName = oldName;
				}
			}
			else
			{
				PlayerPrefs.SetInt(i+"HScore",newScore);
				PlayerPrefs.SetString(i+"HScoreName",newName);
				newScore = 0;
				newName = "";
				break;
	      	}
		}
	}
}
