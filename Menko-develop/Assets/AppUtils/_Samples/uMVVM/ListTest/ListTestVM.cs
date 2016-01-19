using UnityEngine;
using System;
using System.Collections.Generic;
using AppUtils.MVVM;

public class ListTestVM : ViewModel
{
	List<TestDataVM> dataList;

	public ListTestVM()
	{
		dataList = new List<TestDataVM>();
		Bind("DataList", () => dataList, null);
	}

	public List<TestDataVM> DataList
	{
		get { return this.dataList; }
		set
		{ 
			dataList = value;
			RaiseUpdate("DataList");
		}
	}

	public void Add(TestDataVM data)
	{
		dataList.Add(data);
		RaiseUpdate("DataList");
	}

	public void AddRange(IEnumerable<TestDataVM> dataArray)
	{
		dataList.AddRange(dataArray);
		RaiseUpdate("DataList");
	}

	public void Remove(TestDataVM data)
	{
		dataList.Remove(data);
		RaiseUpdate("DataList");
	}

	public void RemoveAt(int index)
	{
		dataList.RemoveAt(index);
		RaiseUpdate("DataList");
	}

	public void RemoveRange(int count)
	{
		for (int i = 0; i < count; ++i)
		{
			dataList.RemoveAt(Count - 1);
		}
		RaiseUpdate("DataList");
	}

	public int Count
	{
		get { return dataList.Count; }
	}
}


public class TestDataVM : ViewModel
{
	Sprite image;
	string text;

	public TestDataVM()
	{
		image = null;
		text = "";
	}

	public TestDataVM(Sprite image, string text)
	{
		this.image = image;
		this.text = text;
		Bind<string>("Text", () => Text, str => Text = str);
		Bind<Sprite>("Image", () => Image, img => Image = img);
	}

	public string Text
	{
		get { return this.text; }
		set
		{
			this.text = value;
			RaiseUpdate("Text");
		}
	}

	public Sprite Image
	{
		get { return this.image; }
		set
		{
			this.image = value;
			RaiseUpdate("Image");
		}
	}
}

public class EventVM : ViewModel
{
	string index;
	string text;
	Action addAct;
	Action changeAct;
	Action delAct;

	public EventVM()
	{
		Bind<string>("Index", () => index, i => Index = i);
		Bind<string>("Text", () => text, t => Text = t);
		Bind<Action>("AddAct", () => addAct, a => AddAct = a);
		Bind<Action>("ChangeAct", () => changeAct, a => ChangeAct = a);
		Bind<Action>("DeleteAct", () => delAct, a => DeleteAct = a);
	}

	public string Index
	{
		get { return this.index; }
		set
		{
			this.index = value;
			RaiseUpdate("Index");
		}
	}

	public string Text
	{
		get { return this.text; }
		set
		{
			this.text = value;
			RaiseUpdate("Text");
		}
	}

	public Action AddAct
	{
		get { return this.addAct; }
		set
		{
			this.addAct = value;
			RaiseUpdate("AddAct");
		}
	}

	public Action ChangeAct
	{
		get { return this.changeAct; }
		set
		{
			this.changeAct = value;
			RaiseUpdate("ChangeAct");
		}
	}

	public Action DeleteAct
	{
		get { return this.delAct; }
		set
		{
			this.delAct = value;
			RaiseUpdate("DeleteAct");
		}
	}
}