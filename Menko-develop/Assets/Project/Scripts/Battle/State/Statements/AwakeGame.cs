using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster.Network;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// ゲームの開始前準備
	/// </summary>
	public class AwakeGame : BattleStateBase
	{
		StateMediator<BattleStateName> mediator;

		public AwakeGame(BattleManager manager) : base(manager)
		{
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("ゲームの開始前準備");
			this.mediator = mediator;

			manager.UI.EntryScreen(this.NextState);

			//// ネットワーク接続(test)
			//// 本番は自分が到着したことを知らせるBufferRPCを飛ばす
			//PhotonManager.Instance.Connection(InRoom);

			// マップのロード
			var assetData = AssetManager.Load(AssetPath.GetStagePrefabPath(manager.Data.StageNumber));
			GameObject.Instantiate(assetData.Asset);

			//this.manager.ViewModels.GuideVM.GuideText = "対戦相手の到着を\n待っています...";
		}

		private void InRoom(bool success)
		{
			PhotonManager.Instance.JoinRoomRandom(StartEntry);
		}

		private void StartEntry(bool success)
		{
			if (success == false)
			{
				PhotonManager.Instance.CreateRoom("TestBattleRoom", false, 2, StartEntry);
				return;
			}

			// 対戦相手の待ち
			SceneManager.Instance.StartCoroutine(RivalWait());
		}

		IEnumerator RivalWait()
		{
			while (PhotonNetwork.otherPlayers.Length != 1)
			{
				yield return new WaitForSeconds(0.5f);
			}

			manager.ViewModels.GuideVM.GuideText = "";
			// 開始演出の開始
			manager.UI.EntryScreen(this.NextState);
		}

		private void NextState()
		{
			// 先攻後攻を決めるステートへ移行
			mediator.SetState(new DecidePlayFirst(manager));
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.AwakeGame;
		}

		#endregion
		
	}
}