using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	void Start () {
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			GetComponent<Player> ().Setup ();
		}
	}

	public override void OnStartClient(){
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString ();
		Player _player = GetComponent<Player> (); 

		GameManager.RegisterPlayer (_netID, _player);
	}

	void OnDisable(){
		GameManager.UnRegisterPlayer (transform.name);
	}
}
