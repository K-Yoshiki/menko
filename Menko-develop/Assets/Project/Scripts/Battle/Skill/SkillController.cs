using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// スキルの操作管理
	/// </summary>
	public class SkillController
	{
		BattleManager manager;
		Queue<SkillPlayTask> task;
		SkillPlayTask currentPlay;

		public SkillController(BattleManager manager)
		{
			this.manager = manager;
			task = new Queue<SkillPlayTask>();
		}

		public bool IsSkillPlaying
		{
			get { return currentPlay != null; }
		}

		/// <summary>
		/// スキルの実行
		/// </summary>
		/// <param name="battleData"></param>
		public void InvokeSkill(MenkoBattleData battleData, Menko invoker)
		{
			Debug.Log("Invoke Skill");
			string skillPath = AssetPath.GetSkillPrefabPath(battleData.SkillData.UseSkillID);
			var skillPrefab = AssetManager.Load<SkillBase>(skillPath).Asset as SkillBase;
			SkillBase skill = GameObject.Instantiate(skillPrefab);
			skill.gameObject.SetActive(false);
			skill.transform.position = invoker.transform.position;
			skill.transform.SetPosY(0f);

			var playTask = new SkillPlayTask(manager, invoker, battleData, skill, NextCall);

			if (currentPlay == null)
			{
				manager.MenkoList.StopRigidAll();
				currentPlay = playTask;
				currentPlay.Invoke();
			}
			else
			{
				task.Enqueue(playTask);
			}
		}

		void NextCall()
		{
			if (task.Count > 0)
			{
				currentPlay = task.Dequeue();
				currentPlay.Invoke();
			}
			else
			{
				currentPlay = null;
				manager.MenkoList.ResumeRigidAll();
			}
		}
	}

	public class SkillPlayTask
	{
		BattleManager manager;
		Menko invoker;
		MenkoBattleData battleData;
		SkillBase skill;
		Action nextCall;

		public SkillPlayTask(
			BattleManager manager,
			Menko invoker,
			MenkoBattleData battleData,
			SkillBase skill,
			Action nextCall)
		{
			this.manager = manager;
			this.invoker = invoker;
			this.battleData = battleData;
			this.skill = skill;
			this.nextCall = nextCall;
		}

		public void Invoke()
		{
			Debug.Log("Task Invoke Skill");

			// カットインを動作させる
			SceneManager.Instance.StartCoroutine(RunSkill());
		}

		IEnumerator RunSkill()
		{
			// ViewModelへの反映
			var skillData = battleData.SkillData;
			var battleVM = manager.ViewModels.BattleVM;
			var texPath = AssetPath.GetMonsterFullTexPath(battleData.BaseData.ID);
			battleVM.SkillInfoVM.SkillName = skillData.Name;
			battleVM.SkillInfoVM.SkillText = skillData.Info;
			battleVM.SkillCutInVM.MonsterTex = AssetManager.Load<Sprite>(texPath).Asset as Sprite;
			battleVM.SkillCutInVM.Enabled = true;
			battleVM.SkillCutInVM.BackBandStateName = battleData.Status.Element.ToString();

			// アニメーション終了まで待つ
			yield return new WaitForSeconds(2.333f);
			manager.ViewModels.BattleVM.SkillCutInVM.Enabled = false;

			// スキルの実行
			skill.Run(manager.MenkoList, battleData, invoker, nextCall);
		}
	}
}