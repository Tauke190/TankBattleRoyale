using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

	public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
	JoinRoomDelegate joinRoomCallBack;

	[SerializeField]
	Text roomNameText;

	MatchInfoSnapshot match;

	public void Setup(MatchInfoSnapshot _match,JoinRoomDelegate _joinRoomCallback){
		match = _match;
		joinRoomCallBack = _joinRoomCallback;
		roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
	}

	public void JoinRoom(){
		joinRoomCallBack.Invoke (match);
	}
}
