using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	public GameObject pauseMenu;

	void Start(){
		PauseMennu.isOn = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			TogglePauseMenu ();
	}

	void TogglePauseMenu(){
		Debug.Log ("PauseGame!");
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMennu.isOn = pauseMenu.activeSelf;
	}
}
