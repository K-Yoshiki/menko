using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace Prototype
{
	public static class ShakeParam
	{
		public const float AccelThreshold = 1.25f;
	}

	public class Prototype_ShakeCounter : MonoBehaviour
	{
		[SerializeField] ShakeInfo info;
		StateMachine<ShakeKey> stateMachine;
		StateCache<ShakeKey> stateCache;

		void Awake()
		{
			Input.gyro.enabled = true;
			stateMachine = new StateMachine<ShakeKey>();
			stateCache = new StateCache<ShakeKey>();
			stateCache.CacheState(new ShakeState_Standby(info));
			stateCache.CacheState(new ShakeState_Wait(info));
			stateCache.CacheState(new ShakeState_Shaking(info));
			stateCache.CacheState(new ShakeState_End(info));
			info.stateCache = stateCache;

			stateMachine.SetState(stateCache.GetState(ShakeKey.Standby));
		}

		void Update()
		{
			stateMachine.UpdateState();
		}

		public void StartShake(MainMediator main, Action<ResultShake> callback)
		{
			info.menkoList = main.menkoList;
			info.resultCallback = callback;
			stateMachine.SetState(stateCache.GetState(ShakeKey.Wait));
		}
	}

	public enum ShakeKey
	{
		Standby,
		Wait,
		Shaking,
		End
	}

	[Serializable]
	public class ShakeInfo
	{
		public Vector3 result;
		public RectTransform shakeText;
		public List<Vector3> accelarations;
		public List<Vector3> gyroAngles;
		public StateCache<ShakeKey> stateCache;
		public Action<ResultShake> resultCallback;
		public List<Prototype_Menko> menkoList;
	}

	public struct ResultShake
	{
		public readonly Vector3 Vector;
		public readonly Vector3 lastAngle;

		public ResultShake(Vector3 vector, Vector3 lastAngle)
		{
			this.Vector = vector;
			this.lastAngle = lastAngle;
		}
	}

	public abstract class ShakeStateBase
	{
		protected ShakeInfo info;
		protected Gyroscope gyro;

		public ShakeStateBase(ShakeInfo info)
		{
			this.info = info;
			this.gyro = Input.gyro;
			this.gyro.enabled = true;
		}
	}


	#region ShakeState
	public class ShakeState_Standby : ShakeStateBase, IState<ShakeKey>
	{
		public ShakeState_Standby(ShakeInfo info) : base(info)
		{
		}

		public void Init(StateMediator<ShakeKey> mediator)
		{
			info.shakeText.gameObject.SetActive(false);
		}

		public void Update(StateMediator<ShakeKey> mediator)
		{
		}

		public void Exit(StateMediator<ShakeKey> mediator)
		{
		}

		public ShakeKey GetKey()
		{
			return ShakeKey.Standby;
		}
	}

	public class ShakeState_Wait : ShakeStateBase, IState<ShakeKey>
	{

		public ShakeState_Wait(ShakeInfo info) : base(info)
		{

		}

		public void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_Wait");
			info.shakeText.gameObject.SetActive(true);
		}

		public void Update(StateMediator<ShakeKey> mediator)
		{
			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
			if (Input.GetMouseButton(0))
			{
				mediator.SetState(info.stateCache.GetState(ShakeKey.Shaking));
			}
			#endif

			if (IsEnd())
			{
				mediator.SetState(info.stateCache.GetState(ShakeKey.Shaking));
			}
		}

		public void Exit(StateMediator<ShakeKey> mediator)
		{
			info.shakeText.gameObject.SetActive(false);
		}

		public bool IsEnd()
		{
			// 指定域を超えたら振ったと判定
			return Input.gyro.userAcceleration.z >= ShakeParam.AccelThreshold;
		}

		public ShakeKey GetKey()
		{
			return ShakeKey.Wait;
		}
	}

	public class ShakeState_Shaking : ShakeStateBase, IState<ShakeKey>
	{
		public ShakeState_Shaking(ShakeInfo info) : base(info)
		{
		}

		public void Init(StateMediator<ShakeKey> mediator)
		{
			info.accelarations.Clear();
			info.gyroAngles.Clear();
			info.accelarations.Add(Input.gyro.userAcceleration);
			info.gyroAngles.Add(gyro.attitude.eulerAngles);
			Debug.Log("ShakeState_Shaking");
		}

		public void Update(StateMediator<ShakeKey> mediator)
		{
			info.accelarations.Add(Input.gyro.userAcceleration);
			info.gyroAngles.Add(gyro.attitude.eulerAngles);


			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
			if (Input.GetKey(KeyCode.Mouse0))
			{
				info.accelarations.Add(Vector3.forward * ShakeParam.AccelThreshold);
				info.gyroAngles.Add(Vector3.zero);
				mediator.SetState(info.stateCache.GetState(ShakeKey.End));
			}
			#endif

			if (IsEnd())
			{
				if (IsShake())
					mediator.SetState(info.stateCache.GetState(ShakeKey.End));
				else
					mediator.SetState(info.stateCache.GetState(ShakeKey.Wait));
			}
		}

		public void Exit(StateMediator<ShakeKey> mediator)
		{
			
		}

		public bool IsEnd()
		{
			return Input.gyro.userAcceleration.z < ShakeParam.AccelThreshold;
		}

		/// <summary>
		/// 指定速度以上のベクトルがあるかどうか
		/// </summary>
		/// <returns><c>true</c> if this instance is shake count over; otherwise, <c>false</c>.</returns>
		bool IsShake()
		{
			foreach (var vec in info.accelarations)
			{
				if (vec.z >= ShakeParam.AccelThreshold)
				{
					return true;
				}
			}
			return false;
		}

		public ShakeKey GetKey()
		{
			return ShakeKey.Shaking;
		}
	}

	public class ShakeState_End : ShakeStateBase, IState<ShakeKey>
	{
		public ShakeState_End(ShakeInfo info) : base(info)
		{
		}

		public void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_End");
			Vector3 vector;
			Vector3 lastAngle = Vector3.zero;

			vector = AvarageAccel();
			lastAngle = info.gyroAngles[info.gyroAngles.Count - 1];

			vector = SwapYZ(vector);
			lastAngle = SwapYZ(lastAngle);

			info.result = vector;

			info.resultCallback(new ResultShake(vector, lastAngle));
			mediator.SetState(info.stateCache.GetState(ShakeKey.Standby));
		}

		Vector3 AvarageAccel()
		{
			var avarage = Vector3.zero;
			var accelList = info.accelarations;
			foreach (var accel in accelList)
			{
				avarage += accel;
			}
			avarage /= accelList.Count;
			return avarage;
		}

		Vector3 SwapYZ(Vector3 vec)
		{
			float y = vec.y;
			vec.y = vec.z;
			vec.z = y;
			return vec;
		}

		public void Update(StateMediator<ShakeKey> mediator)
		{

		}

		public void Exit(StateMediator<ShakeKey> mediator)
		{

		}

		public ShakeKey GetKey()
		{
			return ShakeKey.End;
		}
	}
	#endregion
}