using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : NetworkBehaviour {

	public GameObject playerPrefab;

	[SyncVar(hook = "OnPlayerChangeName")] //Calls function OnPlayerChangeName when the variable playerName is changed
	public string playerName = "Anonymus"; //If we use hook on a syncvar the local value does not get automatically updated

	void Start () {
		if (!isLocalPlayer)
			return;
		CmdSpawnPlayer ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			CmdChangePlayerName ();
		}
	}

	[Command]
	void CmdChangePlayerName(){
		playerName = "Utsav";
	}

	[Command]
	void CmdSpawnPlayer() {
		GameObject go = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority (go, connectionToClient);
	}
	
	void OnPlayerChangeName(string newName){
		playerName = newName;
		gameObject.name = newName;
	}
}
