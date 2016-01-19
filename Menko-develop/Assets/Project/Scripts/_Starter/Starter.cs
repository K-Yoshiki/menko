using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MenkoiMonster.Scene;
using MenkoiMonster.Battle;
using AppUtils.Assets;

namespace MenkoiMonster
{
	/// <summary>
	/// テスト用スタータークラス
	/// </summary>
	public class Starter : MonoBehaviour
	{
		[SerializeField] SceneName startScene;
		[SerializeField] int targetFps;
		[SerializeField] bool isOfflineMode;

		IEnumerator Start()
		{
			Application.targetFrameRate = targetFps;
			Input.gyro.enabled = true;
			PhotonNetwork.offlineMode = isOfflineMode;

			while (Application.isShowingSplashScreen)
			{
				yield return null;
			}

			switch (startScene)
			{
				case SceneName.Title:
					ToTitle();
					break;
				case SceneName.Home:
					ToHome();
					break;
				case SceneName.Battle:
					ToBattle();
					break;
				case SceneName.Loading:
					ToLoading();
					break;
				default:
					break;
			}
		}

		void ToTitle()
		{
			SceneManager.Instance.ChangeScene(new TitleScene());
		}

		void ToHome()
		{
			SceneManager.Instance.ChangeScene(new HomeScene());
		}

		void ToBattle()
		{
			// 仮の対戦データを使用
			BattleUnit[] units = new BattleUnit[2];
			for (int i = 0; i < units.Length; ++i)
			{
				BattleUnit unit = new BattleUnit(
					new uint[] { 1, 2, 3, 2, 1 },
					new bool[] { false, true, false, true, false},
					(i == 0 ? true : false)
				);
				units[i] = unit;
			}
			BattleData battleSetUp = new BattleData(1, true, units);
			SceneManager.Instance.ChangeScene(new BattleScene(battleSetUp));
		}

		void ToLoading()
		{
			ToTitle();
		}
	}
}