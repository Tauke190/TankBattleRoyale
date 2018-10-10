using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform tank;
	public float smoothSpeed = 10f;
	public Vector3 offset;

	public void SelectPlayer(Transform playerTransform){
		tank = playerTransform;
		transform.position = tank.position + offset;
	}

	void FixedUpdate () 
	{
		/*if (!isPlayerSpawned)
			return;*/

		if (GameObject.FindObjectOfType<TankController> () == null)
			return;
		/*if (tank == null)
			tank = GameObject.FindObjectOfType<TankController> ().transform;*/

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
