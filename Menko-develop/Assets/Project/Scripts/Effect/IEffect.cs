using UnityEngine;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public interface IEffect
	{
		void SetPos(Vector3 pos);

		float Duration { get; }
	}
}