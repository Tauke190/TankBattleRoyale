using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickEnable : MonoBehaviour {

	public GameObject joystick;

	void Start () {
		joystick.SetActive (false);
	}

	public void setTrue(){
		joystick.SetActive (true);
	}
}
