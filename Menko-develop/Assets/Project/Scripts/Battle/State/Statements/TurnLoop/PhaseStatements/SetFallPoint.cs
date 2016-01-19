using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using AppUtils;
using AppUtils.UserControls;

namespace MenkoiMonster.Battle.State.Turn
{
	public class SetFallPoint : PhaseStateBase
	{
		Camera mainCamera;
		bool isMoved;
		bool isFallPointRegisted;

		public SetFallPoint(BattleManager manager, PhaseShare share, bool isPlayer)
			: base(manager, share, isPlayer)
		{
			
		}

		public override void Init(StateMediator<PhaseStateName> mediator)
		{
			manager.ViewModels.GuideVM.GuideText = "落下位置をタップしてください";
			isMoved = false;
			isFallPointRegisted = false;
			SetupCamera();
			TouchSensor.Instance.AddAction(TouchState.Enter, TouchMove);
			TouchSensor.Instance.AddAction(TouchState.Move, TouchMove);
			TouchSensor.Instance.AddAction(TouchState.Exit, TouchExit);
			TouchSensor.Instance.SingleTouchMode(true);
		}

		public override void Update(StateMediator<PhaseStateName> mediator)
		{
			if (isFallPointRegisted)
			{
				mediator.SetState(share.cache.GetState(PhaseStateName.Attack));
			}
		}

		public override void Exit(StateMediator<PhaseStateName> mediator)
		{
			TouchSensor.Instance.RemoveAction(TouchState.Enter, TouchMove);
			TouchSensor.Instance.RemoveAction(TouchState.Move, TouchMove);
			TouchSensor.Instance.RemoveAction(TouchState.Exit, TouchExit);

			if (isPlayer)
			{
				var unitList = manager.ViewModels.UnitListVM.PlayerUnitList;
				for (int i = 0; i < unitList.Count; i++)
				{
					unitList[i].Pressed = null;
					unitList[i].LongPressed = null;
				}
			}
		}

		public override PhaseStateName GetKey()
		{
			return PhaseStateName.SetFallPoint;
		}
			
		void SetupCamera()
		{
			if (mainCamera != null)
				return;
			mainCamera = Camera.main;
		}

		void TouchMove(IGestureInfo[] infos)
		{
			isMoved = true;
			Ray ray = mainCamera.ScreenPointToRay(infos[0].Pos);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				share.fallPointer.gameObject.SetActive(true);
				share.fallPointer.transform.position = hit.point;
			}
		}

		void TouchExit(IGestureInfo[] infos)
		{
			if (isMoved == false)
				return;

			// UI要素にあたっていないかを検知する
			var pointerData = new PointerEventData(EventSystem.current);
			pointerData.position = infos[0].Pos;
			List<RaycastResult> result = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerData, result);
			if (result.Count > 0)
			{
				share.fallPointer.gameObject.SetActive(false);
				return;
			}

			Ray ray = mainCamera.ScreenPointToRay(infos[0].Pos);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				share.fallPointer.transform.position = hit.point;
				isFallPointRegisted = true;
			}			
		}
	}
}