using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle.State.Turn
{
	public static class ShakeParam
	{
		public const float AccelThreshold = 1.25f;
	}

	public class ShakeCounter
	{
		ShakeInfo info;
		StateMachine<ShakeKey> stateMachine;
		StateCache<ShakeKey> stateCache;

		public ShakeCounter(BattleManager manager)
		{
			Input.gyro.enabled = true;
			info = new ShakeInfo();
			stateMachine = new StateMachine<ShakeKey>();
			stateCache = new StateCache<ShakeKey>();
			stateCache.CacheState(new ShakeState_Standby(manager, info));
			stateCache.CacheState(new ShakeState_Wait(manager, info));
			stateCache.CacheState(new ShakeState_Shaking(manager, info));
			stateCache.CacheState(new ShakeState_End(manager, info));
			info.stateCache = stateCache;

			stateMachine.SetState(stateCache.GetState(ShakeKey.Standby));
		}

		public void Update()
		{
			stateMachine.UpdateState();
		}

		public void StartShake(Action<ResultShake> callback)
		{
			info.resultCallback = callback;
			stateMachine.SetState(stateCache.GetState(ShakeKey.Wait));
		}

		public void ForceReset()
		{
			stateMachine.SetState(stateCache.GetState(ShakeKey.Standby));
		}
	}

	public enum ShakeKey
	{
		Standby,
		Wait,
		Shaking,
		End
	}

	public class ShakeInfo
	{
		public List<Vector3> accelarations;
		public List<Vector3> gyroAngles;
		public StateCache<ShakeKey> stateCache;
		public Action<ResultShake> resultCallback;

		public ShakeInfo()
		{
			accelarations = new List<Vector3>();
			gyroAngles = new List<Vector3>();
		}
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

	public abstract class ShakeStateBase : IState<ShakeKey>
	{
		protected BattleManager manager;
		protected ShakeInfo info;
		protected Gyroscope gyro;

		public ShakeStateBase(BattleManager manager, ShakeInfo info)
		{
			this.manager = manager;
			this.info = info;
			this.gyro = Input.gyro;
			this.gyro.enabled = true;
		}

		public virtual void Init(StateMediator<ShakeKey> mediator)
		{
		}

		public virtual void Update(StateMediator<ShakeKey> mediator)
		{
		}

		public virtual void Exit(StateMediator<ShakeKey> mediator)
		{
		}

		public abstract ShakeKey GetKey();
	}


	#region ShakeState
	public class ShakeState_Standby : ShakeStateBase
	{
		public ShakeState_Standby(BattleManager manager, ShakeInfo info)
			: base(manager, info)
		{
		}

		public override void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_Standby");
		}

		public override ShakeKey GetKey()
		{
			return ShakeKey.Standby;
		}
	}

	public class ShakeState_Wait : ShakeStateBase
	{

		public ShakeState_Wait(BattleManager manager, ShakeInfo info)
			: base(manager, info)
		{
		}

		public override void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_Wait");
			manager.ViewModels.GuideVM.GuideText = "端末を振ってください";
		}

		public override void Update(StateMediator<ShakeKey> mediator)
		{
			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
			if (Input.GetKey(KeyCode.Space))
			{
				mediator.SetState(info.stateCache.GetState(ShakeKey.Shaking));
				return;
			}
			#endif

			#if UNITY_IOS || UNITY_ANDROID
			if (IsEnd())
			{
				mediator.SetState(info.stateCache.GetState(ShakeKey.Shaking));
			}
			#endif
		}

		public override void Exit(StateMediator<ShakeKey> mediator)
		{
			manager.ViewModels.GuideVM.GuideText = "";
			Debug.Log("ShakeState_Wait_Exit");
		}

		public bool IsEnd()
		{
			// 指定域を超えたら振ったと判定
			return Input.gyro.userAcceleration.z >= ShakeParam.AccelThreshold;
		}

		public override ShakeKey GetKey()
		{
			return ShakeKey.Wait;
		}
	}

	public class ShakeState_Shaking : ShakeStateBase
	{
		public ShakeState_Shaking(BattleManager manager, ShakeInfo info)
			: base(manager, info)
		{
		}

		public override void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_Shaking");
			info.accelarations.Clear();
			info.gyroAngles.Clear();
			info.accelarations.Add(Input.gyro.userAcceleration);
			info.gyroAngles.Add(gyro.attitude.eulerAngles);
		}

		public override void Update(StateMediator<ShakeKey> mediator)
		{
			info.accelarations.Add(Input.gyro.userAcceleration);
			info.gyroAngles.Add(gyro.attitude.eulerAngles);

			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
			if (Input.GetKey(KeyCode.Space))
			{
				info.accelarations.Add(Vector3.forward * ShakeParam.AccelThreshold);
				info.gyroAngles.Add(Vector3.zero);
				mediator.SetState(info.stateCache.GetState(ShakeKey.End));
				return;
			}
			#endif

			#if UNITY_IOS || UNITY_ANDROID
			if (IsEnd())
			{
				if (IsShake())
					mediator.SetState(info.stateCache.GetState(ShakeKey.End));
				else
					mediator.SetState(info.stateCache.GetState(ShakeKey.Wait));
			}
			#endif
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

		public override ShakeKey GetKey()
		{
			return ShakeKey.Shaking;
		}
	}

	public class ShakeState_End : ShakeStateBase
	{
		public ShakeState_End(BattleManager manager, ShakeInfo info)
			: base(manager, info)
		{
		}

		public override void Init(StateMediator<ShakeKey> mediator)
		{
			Debug.Log("ShakeState_End");
			Vector3 vector;
			Vector3 lastAngle = Vector3.zero;

			vector = AvarageAccel();
			lastAngle = info.gyroAngles[info.gyroAngles.Count - 1];

			vector = SwapYZ(vector);
			lastAngle = SwapYZ(lastAngle);

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

		public override ShakeKey GetKey()
		{
			return ShakeKey.End;
		}
	}
	#endregion
}