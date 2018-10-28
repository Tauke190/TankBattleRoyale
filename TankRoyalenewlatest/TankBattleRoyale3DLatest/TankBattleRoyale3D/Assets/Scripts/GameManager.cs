using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	const string PLAYER_ID_PREFIX = "Player";

	public static GameManager instance;

	void Awake(){
		if (instance != null) {
			Debug.LogError ("There is more than one GameManager in the scene.");
		} else {
			instance = this;
		}
	}

	static Dictionary<string,Player>players = new Dictionary<string, Player>();

	public static void RegisterPlayer(string _netID,Player _player){
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add (_playerID, _player);
		_player.transform.name = _playerID;
	}

	public static void UnRegisterPlayer(string _playerID){
		players.Remove (_playerID);
	}

	public static Player GetPlayer(string _playerID){
		return players [_playerID];
	}

	/*void OnGUI(){
		GUILayout.BeginArea (new Rect (200, 200, 200, 500));
		GUILayout.BeginVertical ();

		foreach (string _playerID in players.Keys) {
			GUILayout.Label (_playerID + " - " + players [_playerID].transform.name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}*/
}
