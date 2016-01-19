using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Network
{
	/// <summary>
	/// Photonネットワークの管理とRPC管理
	/// </summary>
	public class PhotonManager : UnitySingleton<PhotonManager>
	{
		PhotonView pView;
		RPCEventListener listener;
		Action<bool> endCallback;

		PhotonView View
		{
			get { return GetOrCreateView(); }
		}

		PhotonView GetOrCreateView()
		{
			if (pView == null)
			{
				pView = this.gameObject.AddComponent<PhotonView>();
				pView.viewID = PhotonNetwork.AllocateViewID();
			}
			return pView;
		}

		protected override void Initialize()
		{
			DontDestroyOnLoad(this.gameObject);
			pView = this.GetComponent<PhotonView>();
			listener = new RPCEventListener();
		}

		public bool Connection(Action<bool> callback = null)
		{
			if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
			{
				endCallback = callback;
				PhotonNetwork.ConnectUsingSettings("v0.1a");
				return true;
			}
			return false;
		}

		public bool JoinLobby(Action<bool> callback = null)
		{
			if (!PhotonNetwork.insideLobby && endCallback == null)
			{
				endCallback = callback;
				PhotonNetwork.JoinLobby();
				return true;
			}
			return false;
		}

		public bool CreateRoom(string name, bool isPrivate, byte maxPlayers, Action<bool> callback = null)
		{
			if (!PhotonNetwork.inRoom && endCallback == null)
			{
				endCallback = callback;
				var options = new RoomOptions() {
					isVisible = !isPrivate,
					maxPlayers = maxPlayers
				};
				PhotonNetwork.CreateRoom(name, options, TypedLobby.Default);
				return true;
			}
			return false;
		}

		public bool JoinRoom(string name, Action<bool> callback = null)
		{
			if (!PhotonNetwork.inRoom && endCallback == null)
			{
				endCallback = callback;
				PhotonNetwork.JoinRoom(name);
				return true;
			}
			return false;
		}

		public bool JoinRoomRandom(Action<bool> callback = null)
		{
			if (!PhotonNetwork.inRoom && endCallback == null)
			{
				endCallback = callback;
				PhotonNetwork.JoinRandomRoom();
				return true;
			}
			return false;
		}

		public bool LeaveRoom(Action<bool> callback = null)
		{
			if (PhotonNetwork.inRoom && endCallback == null)
			{
				endCallback = callback;
				PhotonNetwork.LeaveRoom();
				return true;
			}
			return false;
		}

		/// <summary>
		/// RPCイベントの追加
		/// </summary>
		/// <param name="eventName">Event name.</param>
		/// <param name="e">E.</param>
		public void AddRPCEvent(string eventName, Action<object[]> e)
		{
			listener.AddEvent(eventName, e);
		}

		/// <summary>
		/// RPCイベントの削除
		/// </summary>
		/// <param name="eventName">Event name.</param>
		public void RemoveRPCEvent(string eventName)
		{
			listener.RemoveEvent(eventName);
		}

		/// <summary>
		/// RPCの発行
		/// </summary>
		/// <param name="eventName">Event name.</param>
		/// <param name="target">Target.</param>
		/// <param name="parameters">Parameters.</param>
		public void SendRPC(string eventName, PhotonTargets target, params object[] parameters)
		{
			View.RPC("CallEvent", target, eventName, parameters);
		}

		[PunRPC]
		void CallEvent(params object[] parameters)
		{
			string eventName = (string)parameters[0];
			if (parameters.Length > 1)
			{
				listener.Invoke(eventName, (object[])parameters[1]);
			}
			else
			{
				listener.Invoke(eventName);
			}
		}

		void OnConnectedToMaster()
		{
			Debug.Log("OnConnectedToMaster");
			CallBack(true);
		}

		void OnJoinedLobby()
		{
			Debug.Log("OnJoinedLobby");
			CallBack(true);
		}

		void OnJoinedRoom()
		{
			Debug.Log("OnJoinedRoom");
			GetOrCreateView();
			CallBack(true);
		}

		void OnFailedToConnectToPhoton()
		{
			Debug.Log("OnFailedToConnectToPhoton");
			CallBack(false);
		}

		void OnPhotonCreateRoomFailed()
		{
			Debug.Log("OnPhotonCreateRoomFailed");
			CallBack(false);
		}

		void OnPhotonJoinRoomFailed()
		{
			Debug.Log("OnPhotonJoinRoomFailed");
			CallBack(false);
		}

		void OnPhotonRandomJoinFailed()
		{
			Debug.Log("OnPhotonRandomJoinFailed");
			CallBack(false);
		}

		void CallBack(bool success)
		{
			if (endCallback != null)
			{
				var callback = endCallback;
				endCallback = null;
				callback(success);
			}
		}
	}
}