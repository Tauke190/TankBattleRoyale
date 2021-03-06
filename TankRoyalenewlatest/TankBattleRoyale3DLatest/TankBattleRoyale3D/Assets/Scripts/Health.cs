﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	Player player;

	Slider healthBar;

	void Update () {
		if (GameObject.FindObjectOfType<TankController> () == null)
			return;
		if (player == null) {
			player = GameObject.FindObjectOfType<TankController> ().GetComponent<Player> ();
			healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider> ();
			healthBar.maxValue = player.maxHealth;
			healthBar.minValue = 0;
		}
		healthBar.value = player.currentHealth;
	}
}
