using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AppUtils.UserControls;

public class TouchTest : MonoBehaviour
{
	public struct TestInfo
	{
		public Transform pointObj;
		public Text infoObj;

		public TestInfo(Transform pointObj, Text infoObj)
		{
			this.pointObj = pointObj;
			this.infoObj = infoObj;
		}
	}

	[SerializeField] Canvas canvas;
	[SerializeField] RectTransform infoLayout;
	[SerializeField] Transform pointImage;
	[SerializeField] Text infoText;
	Dictionary<int, TestInfo> pointers;

	void Awake()
	{
		Application.targetFrameRate = 60;
		pointers = new Dictionary<int, TestInfo>();
		TouchSensor.Instance.AddAction(TouchState.Enter, Enter);
		TouchSensor.Instance.AddAction(TouchState.Move, Move);
		TouchSensor.Instance.AddAction(TouchState.Exit, Exit);
		TouchSensor.Instance.AddAction(TouchState.Cancel, Cancel);
	}

	void CreatePoint(int fingerID, Vector2 pos)
	{
		TestInfo tesInfo;
		if (pointers.ContainsKey(fingerID))
		{
			tesInfo = pointers[fingerID];
			tesInfo.pointObj.gameObject.SetActive(true);
			tesInfo.infoObj.gameObject.SetActive(true);
		}
		else
		{
			Transform pointer = Instantiate<Transform>(pointImage);
			Text info = Instantiate<Text>(infoText);

			pointer.SetParent(canvas.transform);
			info.transform.SetParent(infoLayout.transform);

			tesInfo = new TestInfo(pointer, info);
			pointers.Add(fingerID, tesInfo);
		}
		tesInfo.pointObj.position = pos;
		tesInfo.infoObj.text = string.Format("ID:{0} Pos:{1}", fingerID, pos);
	}

	void RemovePoint(int fingerID)
	{
		if (!pointers.ContainsKey(fingerID))
			return;

		Transform pointer = pointers[fingerID].pointObj;
		Text infoText = pointers[fingerID].infoObj;

		pointer.gameObject.SetActive(false);
		infoText.gameObject.SetActive(false);
	}

	void Enter(IGestureInfo[] info)
	{
		for (int i = 0; i < info.Length; ++i)
		{
			CreatePoint(info[i].FingerID, info[i].Pos);
		}
	}

	void Move(IGestureInfo[] info)
	{
		for (int i = 0; i < info.Length; ++i)
		{
			CreatePoint(info[i].FingerID, info[i].Pos);
		}
	}

	void Exit(IGestureInfo[] info)
	{
		for (int i = 0; i < info.Length; ++i)
		{
			RemovePoint(info[i].FingerID);
		}
	}

	void Cancel(IGestureInfo[] info)
	{
		for (int i = 0; i < info.Length; ++i)
		{
			RemovePoint(info[i].FingerID);
		}
	}
}