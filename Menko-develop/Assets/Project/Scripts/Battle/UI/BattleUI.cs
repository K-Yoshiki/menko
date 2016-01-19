using UnityEngine;
using System;

namespace MenkoiMonster.Battle
{
	public class BattleUI : MonoBehaviour
	{
		[SerializeField] BattleStartUI startUI;
		[SerializeField] BattleEndUI endUI;

		public void EntryScreen(Action callback)
		{
			startUI.EntryScreen(callback);
		}

		public void PlayEnd(Action callback)
		{
			endUI.Play(callback);
		}
	}
}