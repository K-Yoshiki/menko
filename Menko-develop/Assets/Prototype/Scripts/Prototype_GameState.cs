using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils;
using AppUtils.UserControls;

namespace Prototype
{
	public class GameState
	{
		StateMachine<GameStateType> stateMachine;
		MainMediator gameMediator;

		public GameState(MainMediator gameMediator)
		{
			this.stateMachine = new StateMachine<GameStateType>();
			this.gameMediator = gameMediator;
		}

		public void Init()
		{
			this.stateMachine.SetState(new GameAwake(gameMediator));
		}

		public void Update()
		{
			this.stateMachine.UpdateState();
		}
	}

	public enum GameStateType
	{
		GameAwake,
		PlayerSelectChip,
		PlayerSetFallPoint,
		PlayerAttack,
		EnemySelectChip,
		EnemyAttack,
		GameSet,
		Result,
	}

	public abstract class GameStateBase
	{
		protected MainMediator gameMediator;

		public GameStateBase(MainMediator gMediator)
		{
			this.gameMediator = gMediator;
		}
	}


	public class GameAwake : GameStateBase, IState<GameStateType>
	{
		public GameAwake(MainMediator gameMediator) : base(gameMediator)
		{
		}

		public void Init(StateMediator<GameStateType> mediator)
		{
			gameMediator.fallPointText.enabled = false;
			Debug.Log("Game - Awake");
		}

		public void Update(StateMediator<GameStateType> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(new PlayerSelectChip(gameMediator));
			}
		}

		public void Exit(StateMediator<GameStateType> mediator)
		{
			
		}

		public bool IsEnd()
		{
			return true;
		}

		public GameStateType GetKey()
		{
			return GameStateType.GameAwake;
		}
	}


	public class PlayerSelectChip : GameStateBase, IState<GameStateType>
	{
		bool isSelected;

		public PlayerSelectChip(MainMediator gameMediator) : base(gameMediator)
		{
		}

		public void Init(StateMediator<GameStateType> mediator)
		{
			isSelected = false;
			this.gameMediator.selecter.callback += Selected;
			this.gameMediator.selecter.ShowUp();
			Debug.Log("Game - PlayerSelectChip");
		}

		void Selected(int num)
		{
			this.gameMediator.selecter.callback -= Selected;
			gameMediator.baseObjects.SelecteChip(num, gameMediator);
			isSelected = true;
			Debug.Log("Game - ChipSelected");
		}

		public void Update(StateMediator<GameStateType> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(new PlayerSetFallPoint(gameMediator));
			}
		}

		public void Exit(StateMediator<GameStateType> mediator)
		{
			gameMediator.selecter.HideDown();
		}

		public bool IsEnd()
		{
			return isSelected;
		}

		public GameStateType GetKey()
		{
			return GameStateType.PlayerSelectChip;
		}
	}


	public class PlayerSetFallPoint : GameStateBase, IState<GameStateType>
	{
		Transform fallPointer;
		bool isMoved;
		bool isFallPointRegisted;

		public PlayerSetFallPoint(MainMediator gameMediator) : base(gameMediator)
		{
		}

		public void Init(StateMediator<GameStateType> mediator)
		{
			fallPointer = gameMediator.baseObjects.fallPointer.transform;
			TouchSensor.Instance.AddAction(TouchState.Enter, TouchMove);
			TouchSensor.Instance.AddAction(TouchState.Move, TouchMove);
			TouchSensor.Instance.AddAction(TouchState.Exit, TouchExit);
			TouchSensor.Instance.SingleTouchMode(true);
			gameMediator.fallPointText.enabled = true;
			Debug.Log("Game - PlayerAttack");
		}

		void TouchMove(IGestureInfo[] infos)
		{
			isMoved = true;
			Ray ray = gameMediator.camera.ScreenPointToRay(infos[0].Pos);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				fallPointer.gameObject.SetActive(true);
				fallPointer.transform.position = hit.point;
			}
		}

		void TouchExit(IGestureInfo[] infos)
		{
			if (isMoved == false)
				return;

			Ray ray = gameMediator.camera.ScreenPointToRay(infos[0].Pos);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				fallPointer.transform.position = hit.point;
				isFallPointRegisted = true;
			}
		}

		public void Update(StateMediator<GameStateType> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(new PlayerAttack(gameMediator));
			}
		}

		public void Exit(StateMediator<GameStateType> mediator)
		{
			Sound.Instance.PlaySE(gameMediator.selectSound);
			gameMediator.fallPointText.enabled = false;
			TouchSensor.Instance.RemoveAction(TouchState.Enter, TouchMove);
			TouchSensor.Instance.RemoveAction(TouchState.Move, TouchMove);
			TouchSensor.Instance.RemoveAction(TouchState.Exit, TouchExit);
		}

		public bool IsEnd()
		{
			return isFallPointRegisted;
		}

		public GameStateType GetKey()
		{
			return GameStateType.PlayerSetFallPoint;
		}
	}

	public class PlayerAttack : GameStateBase, IState<GameStateType>
	{
		bool isFallChip;
		float waitTimer;

		public PlayerAttack(MainMediator gameMediator) : base(gameMediator)
		{
		}

		public void Init(StateMediator<GameStateType> mediator)
		{
			gameMediator.shaker.StartShake(gameMediator, EndShake);
		}

		void EndShake(ResultShake info)
		{
			Vector3 fallPos = gameMediator.baseObjects.fallPointer.transform.position;
			fallPos.y = 4f;

			Prototype_Menko menko = gameMediator.baseObjects.attackChip;
			menko.gameObject.SetActive(true);
			menko.StartAttack(fallPos, info.lastAngle, info.Vector);
				
			isFallChip = true;
		}

		public void Update(StateMediator<GameStateType> mediator)
		{
			if (isFallChip)
			{
				waitTimer += Time.deltaTime;
			}

			if (IsEnd())
			{
				IsDeadDestory();
				mediator.SetState(new PlayerSelectChip(gameMediator));
			}
		}

		void IsDeadDestory()
		{
			List<Prototype_Menko> removeList = new List<Prototype_Menko>();
			foreach(var menko in gameMediator.menkoList)
			{
				if (menko.IsDead())
				{
					removeList.Add(menko);
				}
			}

			foreach(var menko in removeList)
			{
				gameMediator.menkoList.Remove(menko);
				GameObject.Destroy(menko.gameObject);
			}
		}

		public void Exit(StateMediator<GameStateType> mediator)
		{
			gameMediator.baseObjects.attackChip.EndAttack();
			gameMediator.baseObjects.fallPointer.gameObject.SetActive(false);
		}

		public bool IsEnd()
		{
			return waitTimer >= 2.5f;
		}

		public GameStateType GetKey()
		{
			return GameStateType.PlayerAttack;
		}
	}
}