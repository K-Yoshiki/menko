using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster.Battle.State.Turn;

namespace MenkoiMonster.Battle.State
{
	public class PhaseStateMachine
	{
		StateMachine<PhaseStateName> stateMachine;
		PhaseShare share;

		public PhaseStateMachine(BattleManager manager, bool isPlayer)
		{
			stateMachine = new StateMachine<PhaseStateName>();
			share = new PhaseShare();
			share.cache.CacheState(new SelectChip(manager, share, isPlayer));
			share.cache.CacheState(new SetFallPoint(manager, share, isPlayer));
			share.cache.CacheState(new Attack(manager, share, isPlayer));
			share.cache.CacheState(new PhaseEnd(manager, share, isPlayer));
		}

		public void SetState(PhaseStateName stateName)
		{
			stateMachine.SetState(share.cache.GetState(stateName));
		}

		public void UpdateState()
		{
			stateMachine.UpdateState();
		}

		public PhaseStateName CurrentKey
		{
			get { return stateMachine.CurrentKey; }
		}
	}

	public enum PhaseStateName
	{
		/// <summary>
		/// 攻撃メンコの選択
		/// </summary>
		SelectChip,

		/// <summary>
		/// 落下位置の選択
		/// </summary>
		SetFallPoint,

		/// <summary>
		/// 攻撃
		/// </summary>
		Attack,

		/// <summary>
		/// 終了
		/// </summary>
		End,
	}

	/// <summary>
	/// フェーズをまたぐ共有データ
	/// </summary>
	public class PhaseShare
	{
		public StateCache<PhaseStateName> cache;
		public int selectIndex;
		public bool isUseSkill;
		public Transform fallPointer;

		public PhaseShare()
		{
			this.cache = new StateCache<PhaseStateName>();
			this.selectIndex = -1;
			SetupPointer();
		}

		void SetupPointer()
		{
			if (fallPointer != null)
				return;

			var path = "Prefabs/FallPointer";
			var assetData = AssetManager.Load<Transform>(path);
			if (!assetData.IsNull)
			{
				fallPointer = GameObject.Instantiate(assetData.Asset as Transform);
				return;
			}
			Debug.LogWarningFormat("[/Resources/{0}] is Not Found!");
		}
	}

	public abstract class PhaseStateBase : IState<PhaseStateName>
	{
		protected BattleManager manager;
		protected PhaseShare share;
		protected bool isPlayer;

		public PhaseStateBase(BattleManager manager, PhaseShare share, bool isPlayer)
		{
			this.manager = manager;
			this.share = share;
			this.isPlayer = isPlayer;
		}

		public virtual void Init(StateMediator<PhaseStateName> mediator)
		{
			
		}

		public virtual void Update(StateMediator<PhaseStateName> mediator)
		{
			
		}

		public virtual void Exit(StateMediator<PhaseStateName> mediator)
		{
			
		}

		public abstract PhaseStateName GetKey();
	}
}