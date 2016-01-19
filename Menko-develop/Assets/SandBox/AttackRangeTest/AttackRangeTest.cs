using UnityEngine;
using System.Collections;
using MenkoiMonster.Battle;

namespace sandbox
{
	public class AttackRangeTest : MonoBehaviour
	{
		[System.Serializable]
		public class Outer
		{
			public Transform outer;
			public bool isTargeting;
			public float angle;
		}

		public Transform me;
		public float range;
		public float searchAngle;
		public float dirAngle;
		[Header("Inspection")]
		public Outer[] outers;

		void Update()
		{
			
			outers.Foreach(o => o.isTargeting = false);

			float harfSearchAngle = searchAngle * 0.5f;
			Physics.OverlapSphere(me.position, 0.2f + 0.4f * range).Foreach(hit => {
				Vector3 dir = hit.transform.position - me.position;
				float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
				angle += dirAngle;
				bool isTarget = false;
				if (angle <= harfSearchAngle && -harfSearchAngle <= angle)
				{
					isTarget = true;
				}

				TargetCheck(hit.transform, angle, isTarget);
			});
		}

		void TargetCheck(Transform hit, float angle, bool isTargeting)
		{
			outers.Foreach(o => {
				if (hit == o.outer)
				{
					o.isTargeting = isTargeting;
					o.angle = angle;
				}
			});
		}
	}
}