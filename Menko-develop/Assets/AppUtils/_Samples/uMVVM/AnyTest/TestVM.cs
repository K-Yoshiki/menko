using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils.MVVM;

/// <summary>
/// 例：テキストのビューモデル
/// </summary>
public class TextVM : ViewModel
{
	string text;
	Color color;

	public TextVM()
	{
		this.text = "Test Text";
		this.color = Color.white;
		Bind<string>("Text", () => Text, str => Text = str);
		Bind<Color>("Color", () => Color, color => Color = color);
	}

	public string Text
	{
		get { return text; }
		set
		{
			text = value;
			RaiseUpdate("Text");
		}
	}

	public Color Color
	{
		get { return color; }
		set
		{
			color = value;
			RaiseUpdate("Color");
		}
	}
}

public class ToggleVM : ViewModel
{
	bool toggle;

	public ToggleVM()
	{
		this.toggle = false;
		Bind<bool>("Toggle", () => toggle, value => Toggle = value);
	}

	public bool Toggle
	{
		get { return toggle; }
		set
		{
			toggle = value;
			RaiseUpdate("Toggle");
		}
	}
}

public class ButtonVM : ViewModel
{
	Action pressEvent;

	public ButtonVM()
	{
		pressEvent = null;
		Bind<Action>("PressEvent", () => pressEvent, value => PressEvent = value);
	}

	public Action PressEvent
	{
		get { return pressEvent; }
		set
		{
			pressEvent = value;
			RaiseUpdate("PressEvent");
		}
	}
}

public class DropdownVM : ViewModel
{
	List<string> captions;
	int selectNum;

	public DropdownVM()
	{
		this.captions = new List<string>();
		Bind<List<string>>("Captions", () => captions, value => Captions = value);
		Bind<int>("SelectNum", () => selectNum, value => SelectNum = value);
	}

	public List<string> Captions
	{
		get { return captions; }
		set
		{
			captions = value;
			RaiseUpdate("Captions");
		}
	}

	public int SelectNum
	{
		get { return selectNum; }
		set
		{
			selectNum = value;
			RaiseUpdate("SelectNum");
		}
	}
}