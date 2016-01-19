using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AppUtils.Events;

namespace AppUtils.UserControls
{
	/// <summary>
	/// Touch Event Manager Class
	/// </summary>
	public sealed class TouchSensor : UnitySingleton<TouchSensor>
	{
		ActionEvents<TouchState, IGestureInfo[]> actions;
		TouchTaskList taskList;
		TouchInfoList infoList;

		public void AddAction(TouchState type, Action<IGestureInfo[]> action)
		{
			actions.Add(type, action);
		}

		public void RemoveAction(TouchState type, Action<IGestureInfo[]> action)
		{
			actions.Remove(type, action);
		}

		public void ClearAction()
		{
			actions.ClearAll();
		}

		public void Clear()
		{
			actions.ClearAll();
			taskList.Clear();
			infoList.Clear();
		}

		public void SingleTouchMode(bool isActive)
		{
			Input.multiTouchEnabled = !isActive;
		}

		protected override void Initialize()
		{
			actions = new ActionEvents<TouchState, IGestureInfo[]>();
			taskList = new TouchTaskList();
			infoList = new TouchInfoList(taskList);
			Input.simulateMouseWithTouches = false;

			#if UNITY_STANDALONE || UNITY_EDITOR
			StartCoroutine(mouseEmulate());
			#endif
		}

		void Update()
		{
			infoList.Update(Input.touches);
			taskList.ExecuteAction(actions);
		}

		IEnumerator mouseEmulate()
		{
			Debug.Log("Mouse Emulation Start");
			EmulateInfo mouseInfo = new EmulateInfo();
			EmulateInfo[] infoAry = new EmulateInfo[] { mouseInfo };

			while (true)
			{
				mouseInfo.deltaPos = (Vector2)Input.mousePosition - mouseInfo.Pos;
				mouseInfo.pos = Input.mousePosition;
				mouseInfo.stateTime += Time.deltaTime;

				Action<TouchState> eventExecute = state =>
				{
					mouseInfo.state = state;
					mouseInfo.stateTime = 0f;
					actions.Execute(state, infoAry);
				};

				if (Input.GetMouseButtonDown(0))
				{
					mouseInfo.totalVec = mouseInfo.DeltaPos;
					eventExecute(TouchState.Enter);
				}
				else if (Input.GetMouseButtonUp(0))
				{
					eventExecute(TouchState.Exit);
				}
				else if (Input.GetMouseButton(0))
				{
					if (mouseInfo.deltaPos == Vector2.zero)
					{
						eventExecute(TouchState.Stay);
					}
					else
					{
						mouseInfo.totalVec += mouseInfo.deltaPos;
						eventExecute(TouchState.Move);
					}
				}
				yield return null;
			}
		}
	}
}