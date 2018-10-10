using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public enum GameState{
	Shoot,
	wait
}

public class TankController : NetworkBehaviour {

	[Range(1,100)]
	public float speed = 10f;
	Rigidbody rb;
	public GameState currentState = GameState.Shoot;
	public float shootDelay = 1.0f;
	public Transform nozzle;
	//public float bulletForce = 5000f;

	public Rigidbody Bullet;
	float x;
	float y;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		GameObject.FindObjectOfType<CameraFollow> ().SelectPlayer (transform);
		GameObject.FindObjectOfType<JoystickEnable> ().setTrue ();
	}

	void Update ()
	{
		x = CrossPlatformInputManager.GetAxis ("Horizontal") * speed;
		y = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
	}

	void FixedUpdate() {
		if (!hasAuthority) {
			return;
		}

		MovePlayer ();

		if(Input.GetKeyDown(KeyCode.Space) && currentState == GameState.Shoot){
			currentState = GameState.wait;
			StartCoroutine (ShootCo ());
		}
	}

	void MovePlayer(){
		Vector3 rotatetank = new Vector3(x,0,y);
		Vector3 movetank = new Vector3(x,-10,y);
		rb.velocity = movetank;
		transform.LookAt(rotatetank + transform.position);
	}

	IEnumerator ShootCo(){
		FireBullet ();
		yield return new WaitForSeconds (shootDelay);
		currentState = GameState.Shoot;
	}

	void FireBullet(){
		/*Rigidbody bulletInstance;
		bulletInstance = Instantiate (Bullet, nozzle.position, nozzle.rotation) as Rigidbody;
		bulletInstance.AddForce (nozzle.forward * bulletForce);*/

		AmmoManager.SpawnAmmo (nozzle.position, nozzle.rotation);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "2") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (35);
		}
		if (col.tag == "4") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (40);
		}
		if (col.tag == "8") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (45);
		}
		Destroy (col.gameObject);
	}
}
