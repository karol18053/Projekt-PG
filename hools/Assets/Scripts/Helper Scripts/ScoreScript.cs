﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public static int scoreValue = 0;
	Text score;

	void Awake() {
		scoreValue = 0;
	}

	void Start() {
		score = GetComponent<Text>();
	}

	void Update() {
		score.text = scoreValue.ToString();
	}
}
