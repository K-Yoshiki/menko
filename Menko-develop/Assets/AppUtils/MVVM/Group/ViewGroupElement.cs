using UnityEngine;
using System.Collections;

namespace AppUtils.MVVM
{
	/// <summary>
	/// リスト型ViewModelの要素オブジェクト
	/// </summary>
	[AddComponentMenu("MVVM/View Group Element", 1)]
	public class ViewGroupElement : MonoBehaviour
	{
		[SerializeField] Transform selfTf;
		[SerializeField] View[] views;

		public View[] Views
		{
			get { return views; }
		}

		public Transform Parent
		{
			get { return selfTf.parent; }
			set { selfTf.SetParent(value); }
		}

		void Awake()
		{
			selfTf.localScale = Vector3.one;
		}
	}
}