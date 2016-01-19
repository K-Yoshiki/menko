using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MenkoiMonster.Network;

public class PhotonTest : MonoBehaviour
{
	public Text log;
	public GameObject lobby;
	public GameObject room;

	void Awake()
	{
		FlipScreen(true);
	}

	public void Connection()
	{
		if (PhotonManager.Instance.Connection(Connected))
		{
			log.text = "Connecting...";
		}
	}
		
	public void JoinLobby()
	{
		if (PhotonManager.Instance.JoinLobby(JoinedLobby))
		{
			log.text = "Lobby in...";
		}
	}

	public void CreateOpenRoom()
	{
		if (PhotonManager.Instance.CreateRoom(null, true, 2, CreatedRoom))
		{
			log.text = "Create open room...";
		}
	}

	public void CreateInvisibleRoom()
	{
		if (PhotonManager.Instance.CreateRoom("001", false, 2, CreatedRoom))
		{
			log.text = "Create private room...";
		}
	}

	public void JoinRandomRoom()
	{
		if (PhotonManager.Instance.JoinRoomRandom(JoinedRandomRoom))
		{
			log.text = "Join random room...";
		}
	}

	public void LeaveRoom()
	{
		PhotonManager.Instance.SendRPC("ThrowMessage", PhotonTargets.All, PhotonNetwork.player.name + "が退室しました");
		if (PhotonManager.Instance.LeaveRoom(LeavedRoom))
		{
			log.text = "Leave room...";
		}
	}

	void FlipScreen(bool isLobby)
	{
		lobby.SetActive(isLobby);
		room.SetActive(!isLobby);
	}

	#region Callback Receivers
	void Connected(bool success)
	{
		if (success)
			log.text = "Connected!";
		else
			log.text = @"<color=""red"">connect failed</color>";
	}

	void JoinedLobby(bool success)
	{
		if (success)
			log.text = "Lobby joined!";
		else
			log.text = @"<color=""red"">Lobby join failed</color>";
	}

	void CreatedRoom(bool success)
	{
		if (success)
		{
			log.text = "Created!";
			FlipScreen(false);
		}
		else
			log.text = @"<color=""red"">Room create failed</color>";
	}

	void JoinedRandomRoom(bool success)
	{
		if (success)
		{
			log.text = "Room Joined!";
			FlipScreen(false);
		}
		else
			log.text = @"<color=""red"">Room join failed</color>";
	}

	void LeavedRoom(bool success)
	{
		if (success)
		{
			log.text = "Leaved Room";
			FlipScreen(true);
		}
		else
			log.text = @"<color=""red"">Room leave failed</color>";
	}
	#endregion
}