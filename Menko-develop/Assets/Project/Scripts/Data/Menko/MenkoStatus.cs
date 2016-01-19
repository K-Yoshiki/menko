using UnityEngine;
using System;

namespace MenkoiMonster
{
	[Serializable]
	public class MenkoStatus
	{
		[SerializeField] uint attack;
		[SerializeField] uint difense;
		[SerializeField] MenkoElement element;

		public uint Attack { get { return attack; } }

		public uint Difense { get { return difense; } }

		public MenkoElement Element { get { return element; } }

		public MenkoStatus(uint attack, uint difense, MenkoElement element)
		{
			this.attack = attack;
			this.difense = difense;
			this.element = element;
		}
	}
}