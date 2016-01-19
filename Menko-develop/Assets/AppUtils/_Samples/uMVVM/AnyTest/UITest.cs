using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils;
using AppUtils.MVVM;

public class UITest : MonoBehaviour
{
	[SerializeField] ViewRoot viewRoot;
	TextVM textVM;
	ToggleVM toggleVM;
	ButtonVM buttonVM;
	DropdownVM dropdownVM;
	SaveData data;

	void Awake()
	{
		data = SaveDataFiler.Load<SaveData>(0);
		if (data == null)
		{
			Debug.Log("Non Save Data");
			data = new SaveData();
		}

		viewRoot.Bind(textVM = new TextVM());
		viewRoot.Bind(toggleVM = new ToggleVM());
		viewRoot.Bind(buttonVM = new ButtonVM());
		viewRoot.Bind(dropdownVM = new DropdownVM());
		SetDefault();
	}

	void SetDefault()
	{
		textVM.Text = data.text;

		toggleVM.Toggle = false;

		buttonVM.PressEvent = () =>
		{
			textVM.Color = (textVM.Color == Color.white) ? Color.red : Color.white;
			SaveTest();
		};

		List<string> captions = new List<string>();
		captions.Add("Level01");
		captions.Add("Level02");
		captions.Add("Level03");

		dropdownVM.SelectNum = 1;
		dropdownVM.Captions = captions;
	}

	void SaveTest()
	{
		data.text = textVM.Text;
		SaveDataFiler.Save<SaveData>(data, 0);
		Debug.Log("SaveData");
	}
}

[Serializable]
public class SaveData
{
	public string text;
}