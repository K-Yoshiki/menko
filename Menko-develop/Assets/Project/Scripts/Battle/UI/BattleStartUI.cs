using UnityEngine;
using System;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public class BattleStartUI : MonoBehaviour
	{
		[SerializeField]
		AttachedTween m_entryScreen;
		Action m_entryCallback;

		public void EntryScreen(Action callback)
		{
			m_entryScreen.Execute();
			m_entryCallback = callback;
		}

		public void EntryEnd()
		{
			if (m_entryCallback != null)
				m_entryCallback();
			m_entryCallback = null;
		}
	}
}