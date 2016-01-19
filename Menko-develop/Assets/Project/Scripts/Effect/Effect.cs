using UnityEngine;

namespace MenkoiMonster.Battle
{
	public class Effect : MonoBehaviour, IEffect
	{
		[SerializeField] protected Transform selfTf;
		[SerializeField] protected float duration;

		public void SetPos(Vector3 pos)
		{
			selfTf.position = pos;
		}

		public float Duration
		{
			get { return duration; }
		}
	}
}