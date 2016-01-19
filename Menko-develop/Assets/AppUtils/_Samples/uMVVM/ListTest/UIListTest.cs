using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils.MVVM;

public class UIListTest : MonoBehaviour
{
	[SerializeField] ViewRoot viewRoot;
	List<TestDataVM> dataVMList;
	ListTestVM testVM;
	EventVM eventVM;

	void Awake()
	{
		testVM = new ListTestVM();
		eventVM = new EventVM();
		viewRoot.Bind(testVM);
		viewRoot.Bind(eventVM);
		SetUp();
	}

	void SetUp()
	{
		dataVMList = new List<TestDataVM>();
		testVM.DataList = dataVMList;

		eventVM.AddAct = OnAddData;
		eventVM.ChangeAct = OnChangeData;
		eventVM.DeleteAct = OnDeleteData;
	}

	void OnAddData()
	{
		TestDataVM dataVM = new TestDataVM(null, eventVM.Text);
		testVM.Add(dataVM);
	}

	void OnChangeData()
	{
		OnChangeData(int.Parse(eventVM.Index), eventVM.Text);
	}

	void OnChangeData(int index, string text)
	{
		dataVMList[index].Text = text;
	}

	void OnDeleteData()
	{
		OnDeleteData(int.Parse(eventVM.Index));
	}

	void OnDeleteData(int index)
	{
		testVM.RemoveAt(index);
	}
}

[Serializable]
public class TestData
{
	public Sprite image;
	public string text;
}