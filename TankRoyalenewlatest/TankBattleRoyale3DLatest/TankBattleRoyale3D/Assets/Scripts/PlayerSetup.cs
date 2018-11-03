using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	public GameObject playerUIPrefab;
	GameObject playerUIInstance;

	[SerializeField]
	Behaviour[] componentsToDisable;

	void Start () {
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} else {
			GetComponent<Player> ().Setup ();
			playerUIInstance = Instantiate (playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;
		}
	}

	public override void OnStartClient(){
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString ();
		Player _player = GetComponent<Player> (); 

		GameManager.RegisterPlayer (_netID, _player);
	}

	void OnDisable(){
		Destroy (playerUIInstance);
		GameManager.UnRegisterPlayer (transform.name);
	}
}
