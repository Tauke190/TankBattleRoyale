using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform tank;
	[SerializeField]
	float smoothSpeed = 10f;
	[SerializeField]
	Vector3 offset;

	void FixedUpdate () 
	{
		if (GameObject.FindObjectOfType<TankController> () == null)
			return;
		if (tank == null) {
			tank = GameObject.FindObjectOfType<TankController> ().transform;
			transform.position = tank.position + offset;
		}

		MoveCamera ();
	}

	void MoveCamera(){
		Vector3 desiredPosition = tank.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}

	public void CameraZoomOut(int yValue){
		offset.y = yValue;
	}
}
