using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle.State.Turn
{
	public class Attack : PhaseStateBase
	{
		const float waitTime = 1f;

		StateMediator<PhaseStateName> mediator;
		ShakeCounter shakeCounter;
		AttackerMenko attacker;

		public Attack(BattleManager manager, PhaseShare share, bool isPlayer)
			: base(manager, share, isPlayer)
		{
			shakeCounter = new ShakeCounter(manager);
		}

		public override void Init(StateMediator<PhaseStateName> mediator)
		{
			Debug.Log("Attack !");
			this.mediator = mediator;
			shakeCounter.StartShake(EndShake);
			manager.ViewModels.BattleVM.BackButtonVM.Enabled = true;
			manager.ViewModels.BattleVM.BackButtonVM.Action = BackToSelect;
		}

		public override void Update(StateMediator<PhaseStateName> mediator)
		{
			shakeCounter.Update();
			OutMapDead();
		}

		public override void Exit(StateMediator<PhaseStateName> mediator)
		{
			manager.ViewModels.BattleVM.BackButtonVM.Enabled = false;
		}

		public override PhaseStateName GetKey()
		{
			return PhaseStateName.Attack;
		}

		void BackToSelect()
		{
			shakeCounter.ForceReset();
			manager.ViewModels.BattleVM.BackButtonVM.Action = null;
			var selectedUnit = manager.ViewModels.UnitListVM.PlayerUnitList[share.selectIndex];
			selectedUnit.IsPressable = true;
			mediator.SetState(share.cache.GetState(PhaseStateName.SelectChip));
		}

		void EndShake(ResultShake info)
		{
			manager.ViewModels.BattleVM.BackButtonVM.Enabled = false;
			manager.ViewModels.GuideVM.GuideText = "";
			Vector3 fallPos = share.fallPointer.transform.position;
			fallPos.y = 4f;

			attacker = manager.MenkoList.GetAll(isPlayer)[share.selectIndex] as AttackerMenko;
			share.selectIndex = -1;
			attacker.gameObject.SetActive(true);
			attacker.StartAttack(fallPos, info.lastAngle, info.Vector, share.isUseSkill);
			SceneManager.Instance.StartCoroutine(EndWait());
		}

		IEnumerator EndWait()
		{
			while (!manager.MenkoList.IsAllSleep() || manager.SkillController.IsSkillPlaying)
			{
				yield return null;
			}

			share.fallPointer.gameObject.SetActive(false);

			if (attacker != null)
			{
				attacker.EndAttack();
				manager.MenkoList.Remove(attacker);
			}
			RemoveDead();
			ExecuteSkill();

			// スキルが発動し終わるまで待つ
			while (manager.SkillController.IsSkillPlaying)
			{
				yield return null;
			}

			// 少し待ってから次へ
			yield return new WaitForSeconds(waitTime);
			mediator.SetState(share.cache.GetState(NextStateName()));
		}

		void OutMapDead()
		{
			List<Menko> removeList = new List<Menko>();
			manager.MenkoList.GetRepresentList().Foreach(menko => {
				if (menko.IsOutMap() && menko.gameObject.activeSelf)
				{
					removeList.Add(menko);
					menko.Difeat();
				}
			});
			if (attacker != null)
			{
				if (attacker.IsOutMap())
				{
					removeList.Add(attacker);
					attacker = null;
				}
			}
			removeList.ForEach(menko => manager.MenkoList.Remove(menko));
		}

		void RemoveDead()
		{
			List<Menko> removeList = new List<Menko>();
			manager.MenkoList.GetRepresentList(false).Foreach(menko => {
				if (menko.IsDead() && menko.gameObject.activeSelf)
				{
					removeList.Add(menko);
					menko.Difeat();
				}
			});
			removeList.ForEach(menko => manager.MenkoList.Remove(menko));
		}

		void ExecuteSkill()
		{
			manager.MenkoList.GetRepresentList(true).Foreach(menko => {
				if (menko.IsDead() && menko.gameObject.activeSelf)
				{
					// 向きを表向きに戻す
					menko.transform.AddRotX(180f);
					// スキル発動
					InvokeSkill(menko);
				}
			});
		}

		void InvokeSkill(Menko invoker)
		{
			int index = manager.MenkoList.GetPlayerIndex(invoker);
			MenkoBattleData data = manager.Data.PlayerUnit.GetData(index);
			if (data.CanUseSkill)
			{
				manager.SkillController.InvokeSkill(data, invoker);
			}
		}

		PhaseStateName NextStateName()
		{
			// 相手の代表格を全て撃破したら強制的に終了
			if (manager.MenkoList.IsFinished())
				return PhaseStateName.End;

			if (manager.ViewModels.UnitListVM.PlayerUnitList.Exists(unit => unit.IsPressable))
			{
				// まだ打てるメンコが残っている
				return PhaseStateName.SelectChip;
			}
			// 攻撃メンコ全て打ち終わり
			return PhaseStateName.End;
		}
	}
}