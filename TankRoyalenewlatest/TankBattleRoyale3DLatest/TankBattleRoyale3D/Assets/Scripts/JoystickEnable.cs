using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickEnable : MonoBehaviour {

	public GameObject joystick;

	void Update(){
		if (GameObject.FindObjectOfType<TankController> () == null)
			joystick.SetActive (false);
		else
			joystick.SetActive (true);
	}
}
