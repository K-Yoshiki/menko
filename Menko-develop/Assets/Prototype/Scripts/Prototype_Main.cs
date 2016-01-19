using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Prototype
{
	/// <summary>
	/// プロトタイプのメイン処理部分です
	/// </summary>
	public class Prototype_Main : MonoBehaviour
	{
		[SerializeField] MainMediator gameMediator;
		GameState gameState;

		void Awake()
		{
			Application.targetFrameRate = 60;
			gameState = new GameState(gameMediator);
		}

		void Start()
		{
			gameState.Init();
		}

		void Update()
		{
			gameState.Update();
		}
	}

	/// <summary>
	/// プロトタイプのメイン処理の仲介クラスです
	/// </summary>
	[Serializable]
	public class MainMediator
	{
		public BaseObjects baseObjects;
		public CameraController camera;
		public Prototype_ChipSelecter selecter;
		public Prototype_ShakeCounter shaker;
		public Text fallPointText;
		public AudioClip selectSound;
		public List<Prototype_Menko> menkoList;
	}

	[Serializable]
	public class BaseObjects
	{
		public Prototype_Menko chipBase;
		[NonSerialized]public Prototype_Menko attackChip;
		public Material[] firefoxes;
		public GameObject fallPointer;

		public void SelecteChip(int num, MainMediator info)
		{
			attackChip = GameObject.Instantiate<Prototype_Menko>(chipBase);
			attackChip.gameObject.SetActive(false);
			attackChip.SetMaterial(SelectMaterial(num));
			info.menkoList.Add(attackChip);
		}

		Material SelectMaterial(int num)
		{
			return firefoxes[Mathf.Clamp(num, 0, firefoxes.Length - 1)];
		}
	}
}