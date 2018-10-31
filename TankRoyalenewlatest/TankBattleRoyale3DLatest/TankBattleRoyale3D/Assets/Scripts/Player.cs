using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SerializeField]
	int maxHealth = 100;

	[SyncVar]
	int currentHealth;

	[SyncVar]
	bool _isDead;
	public bool isDead{
		get{ return _isDead; }
		protected set{ _isDead = value; }
	}

	[SerializeField]
	Behaviour[] disableOnDeath;
	bool[] wasEnabled;

	bool firstSetup = true;

	public void Setup () {
		CmdBroadcastNewPlayerSetup ();
	}

	[Command]
	void CmdBroadcastNewPlayerSetup(){
		RpcSetupPlayerOAllClients ();	
	}

	[ClientRpc]
	void RpcSetupPlayerOAllClients(){
		if (firstSetup) {
			wasEnabled = new bool[disableOnDeath.Length];
			for (int i = 0; i < wasEnabled.Length; i++)
				wasEnabled [i] = disableOnDeath [i].enabled;
		}
		firstSetup = false;
		SetDefaults ();
	}
		
	[ClientRpc]
	public void RpcTakeDamage(int _amount){
		if (isDead)
			return;
		currentHealth -= _amount;
		Debug.Log (transform.name + " now has " + currentHealth + " health.");
		if (currentHealth <= 0)
			Die ();
	}

	void Die(){
		isDead = true;
		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = false;
		}
		Collider col = GetComponent<Collider> ();
		if (col != null)
			col.enabled = false;
		LineRenderer lr = GetComponentInChildren<LineRenderer> ();
		if (lr != null)
			lr.enabled = false;
		Debug.Log (transform.name + " is DEAD!");
		StartCoroutine (Respawn ());
	}
		
	public void SetDefaults(){
		isDead = false;
		currentHealth = maxHealth;
		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = wasEnabled [i];
		}
		Collider col = GetComponent<Collider> ();
		if (col != null)
			col.enabled = true;
		LineRenderer lr =  GetComponentInChildren<LineRenderer> ();
		if (lr != null)
			lr.enabled = true;
	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds (3f);
		Transform spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;
		//yield return new WaitForSeconds (0.1f);
		Setup ();
		Debug.Log (transform.name + " has been respawned.");
	}

	void Update(){
		if (!isLocalPlayer)
			return;
		if (Input.GetKeyDown (KeyCode.K))
			RpcTakeDamage (9999);
	}
}
