using UnityEngine;
using System;
using System.Collections;

namespace Prototype
{
	public class Prototype_MenkoCollider : MonoBehaviour
	{
		public event Action<Collision> CollisionEnterEvent;

		void OnCollisionEnter(Collision other)
		{
			if (CollisionEnterEvent != null)
				CollisionEnterEvent(other);
		}
	}
}