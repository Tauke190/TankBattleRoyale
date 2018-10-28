using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public enum GameState{
	Shoot,
	wait
}

[RequireComponent(typeof(Rigidbody))]
public class TankController : NetworkBehaviour {

	public Weapon weapon;

	[SerializeField]
	[Range(1,100)]
	float speed = 10f;
	Rigidbody rb;
	public GameState currentState = GameState.Shoot;
	[SerializeField]
	float shootDelay = 1.0f;
	Transform nozzle;

	public GameObject bullet;

	float x;
	float y;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		nozzle = GameObject.FindObjectOfType<Scope> ().transform;
	}

	void Update ()
	{
		x = CrossPlatformInputManager.GetAxis ("Horizontal") * speed;
		y = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
	}

	void FixedUpdate() {
		MovePlayer ();

		if(Input.GetKeyDown(KeyCode.Space) && currentState == GameState.Shoot){
			currentState = GameState.wait;
			StartCoroutine (ShootCo ());
			CmdFireBullet ();
		}
	}

	void MovePlayer(){
		Vector3 rotatetank = new Vector3(x,0,y);
		Vector3 movetank = new Vector3(x,-10,y);
		rb.velocity = movetank;
		transform.LookAt(rotatetank + transform.position);
	}
		
	IEnumerator ShootCo(){
		yield return new WaitForSeconds (shootDelay);
		currentState = GameState.Shoot;
	}

	[Command]
	void CmdFireBullet(){
		GameObject go = Instantiate (bullet, nozzle.position, nozzle.rotation);
		NetworkServer.Spawn (go);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "2") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (35);
			Destroy (col.gameObject);
		}
		if (col.tag == "4") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (40);
			Destroy (col.gameObject);
		}
		if (col.tag == "8") {
			CameraFollow cam = GameObject.FindObjectOfType<CameraFollow> ();
			cam.CameraZoomOut (45);
			Destroy (col.gameObject);
		}
		if (col.tag == "Bullet") {
			CmdPlayerHit (transform.name, weapon.damage);
		}
	}

	[Command]
	void CmdPlayerHit(string _playerID,int _damage){
		Player _player = GameManager.GetPlayer (_playerID);
		_player.RpcTakeDamage (_damage);
	}
}
