using UnityEngine;
using System.Collections;
using MenkoiMonster;
using MenkoiMonster.Scene;
using MenkoiMonster.Battle;

namespace MenkoiMonster.Home
{
	public class HomeSceneManager : MonoBehaviour
	{
		[SerializeField] FadeIn fadeIn;

		public void StartFadeIn()
		{
			fadeIn.HideScreen();
		}

		public void SceneMove()
		{
			// 今の所仮データを入れる
			BattleUnit[] units = new BattleUnit[2];
			for (int i = 0; i < units.Length; ++i)
			{
				BattleUnit unit = new BattleUnit(
					new uint[] { 1, 3, 5, 7, 3 },
					new bool[] { false, true, false, true, false},
					(i == 0 ? true : false)
				);
				units[i] = unit;
			}
			BattleData battleSetUp = new BattleData(1, true, units);

			SceneManager.Instance.ChangeScene(new BattleScene(battleSetUp));
		}
	}
}