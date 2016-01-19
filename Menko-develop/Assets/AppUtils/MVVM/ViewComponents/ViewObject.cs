using UnityEngine;
using AppUtils.MVVM;
using System;

[AddComponentMenu("MVVM/Components/View Object")]
public class ViewObject : View
{
	[Header("Property Names")]
	[SerializeField] string activeName;

	protected override void Init()
	{
		AddUpdater(activeName, UpdateActive);
	}

	public void UpdateActive(object value)
	{
		gameObject.SetActive((bool)value);
	}
}