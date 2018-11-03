using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	NetworkManager networkManager;

	List<GameObject> roomList = new List<GameObject>();

	[SerializeField]
	Text status;

	public GameObject roomListItemPrefab;

	[SerializeField]
	Transform roomListParent;

	void Start () {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
			networkManager.StartMatchMaker ();
		RefreshList ();
	}

	public void RefreshList(){
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		status.text = "";
		if (!success || matchList == null) {
			status.text = "Couldn't get room list.";
			return;
		}
		foreach (MatchInfoSnapshot match in matchList) {
			GameObject roomListItemGO = Instantiate (roomListItemPrefab);
			roomListItemGO.transform.SetParent (roomListParent);
			RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem> ();
			if (roomListItem != null)
				roomListItem.Setup (match, JoinRoom);
			roomList.Add (roomListItemGO);
		}
		if (roomList.Count == 0)
			status.text = "No room at the moment.";
	}

	void ClearRoomList(){
		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList [i]);
		}
		roomList.Clear ();
	}

	public void JoinRoom(MatchInfoSnapshot _match){
		networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		ClearRoomList ();
		status.text = "Joining...";
	}
}
