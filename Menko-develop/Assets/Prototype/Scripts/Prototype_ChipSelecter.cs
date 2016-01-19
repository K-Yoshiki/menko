using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using AppUtils;

public class Prototype_ChipSelecter : MonoBehaviour
{
	[SerializeField] RectTransform selfRtf;
	[SerializeField] Text infoText;
	[SerializeField] AudioClip slideSound;
	public event Action<int> callback;
	Coroutine moveWindow;

	public void Awake()
	{
		infoText.enabled = false;
		if (selfRtf == null)
		{
			selfRtf = this.GetComponent<RectTransform>();
		}
	}

	public void ShowUp()
	{
		infoText.enabled = true;
		if (moveWindow != null)
		{
			StopCoroutine(moveWindow);
		}
		moveWindow = StartCoroutine(MoveWindow(0.0f, 0.25f));
		Sound.Instance.PlaySE(slideSound);
	}

	public void HideDown()
	{
		infoText.enabled = false;
		if (moveWindow != null)
		{
			StopCoroutine(moveWindow);
		}
		moveWindow = StartCoroutine(MoveWindow(-selfRtf.sizeDelta.y, 0.25f));
		Sound.Instance.PlaySE(slideSound);
	}

	IEnumerator MoveWindow(float targetYpos, float time)
	{
		float speed = 1 / time;

		while (targetYpos != selfRtf.anchoredPosition.y)
		{
			selfRtf.anchoredPosition3D = Vector3.Slerp(selfRtf.anchoredPosition3D, Vector3.up * targetYpos, Time.deltaTime * speed);
			yield return null;
		}
		moveWindow = null;
	}

	public void PushButton(int num)
	{
		if (callback != null)
			callback(num);
	}
}