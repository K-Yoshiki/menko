using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster.Network;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// 先攻決めのフェーズ
	/// </summary>
	public class DecidePlayFirst : BattleStateBase
	{
		StateMediator<BattleStateName> mediator;
		DecideMenko decideMenko;
		const float WaitTime = 1.5f;

		public DecidePlayFirst(BattleManager manager) : base(manager)
		{
		}

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			this.mediator = mediator;
			PhotonManager.Instance.AddRPCEvent("Flipping", Flipping);
			manager.ViewModels.GuideVM.GuideText = "メンコの表裏で\n先攻後攻を決定します";
			var path = BattleConst.Menko.DecideMenkoPath;
			var prefab = (DecideMenko)AssetManager.Load<DecideMenko>(path).Asset;
			decideMenko = Object.Instantiate(prefab);
			decideMenko.transform.position = Vector3.up * 1f;

			if (PhotonNetwork.offlineMode)
			{
				Flipping(new object[] { Random.Range(2.5f, 10f), Random.Range(50f, 150f) });
				return;
			}

			if (PhotonNetwork.player.isMasterClient)
			{
				PhotonManager.Instance.SendRPC(
					"Flipping",
					PhotonTargets.All,
					Random.Range(2.5f, 10f),
					Random.Range(50f, 150f)
				);
			}
		}

		void Flipping(object[] parameters)
		{
			float addForce = (float)parameters[0];
			float addTorque = (float)parameters[1];

			decideMenko.Flipping(addForce, addTorque);
			SceneManager.Instance.StartCoroutine(ShowResult(mediator));
		}

		public override void Exit(StateMediator<BattleStateName> mediator)
		{
			var effect = Object.Instantiate(ResourceUtils.GetMenkoReturnEffect());
			effect.transform.position = decideMenko.transform.position;
			Object.Destroy(decideMenko.gameObject);
			PhotonManager.Instance.RemoveRPCEvent("Flipping");
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.DecidePlayFirst;
		}

		IEnumerator ShowResult(StateMediator<BattleStateName> mediator)
		{
			while (decideMenko.IsSleep() == false)
			{
				yield return null;
			}

			bool isFirst = !decideMenko.IsBack();
			isFirst = manager.Data.IsPlayerHost ? isFirst : !isFirst;

			string guideText;
			if (isFirst)
			{
				guideText = @"あなたは <color=""red"">先攻</color> になりました";
			}
			else
			{
				guideText = @"あなたは <color=""red"">後攻</color> になりました";
			}
			manager.ViewModels.GuideVM.GuideText = guideText;

			yield return new WaitForSeconds(WaitTime);

			manager.ViewModels.GuideVM.GuideText = "";
			mediator.SetState(new StartGame(manager, isFirst));
		}
	}
}