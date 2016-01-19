using UnityEngine;
using AppUtils.MVVM;
using System;

namespace MenkoiMonster.Battle
{
	public class UnitVM : ViewModel
	{
		Sprite faceSprite;
		bool isPressable;
		bool isRepresent;
		string percent;
		string skillTurn;
		Action pressed;
		Action longPressed;
		bool isDead;

		public UnitVM(Sprite face, bool isRepresent, int skillTurn)
		{
			this.faceSprite = face;
			this.isRepresent = isRepresent;
			this.percent = "0";
			this.skillTurn = skillTurn.ToString();
			this.isDead = false;

			Bind("FaceSprite", () => this.faceSprite, null);
			Bind("IsPressable", () => this.IsPressable, null);
			Bind("IsRepresent", () => this.isRepresent, null);
			Bind("Percent", () => this.percent, null);
			Bind("SkillTurn", () => this.skillTurn, null);
			Bind("Pressed", () => this.pressed, null);
			Bind("LongPressed", () => this.longPressed, null);
			Bind("IsDead", () => this.isDead, null);
		}

		public Sprite FaceSprite
		{
			get { return faceSprite; }
			set
			{
				faceSprite = value;
				RaiseUpdate("FaceSprite");
			}
		}

		public bool IsPressable
		{
			get { return isPressable; }
			set
			{
				isPressable = !isRepresent && value; 
				RaiseUpdate("IsPressable");
			}
		}

		public bool IsRepresent
		{
			get { return isRepresent; }
			set
			{
				isRepresent = value;
				RaiseUpdate("IsRepresent");
				RaiseUpdate("IsPressable");
			}
		}

		public string Persent
		{
			get { return percent; }
			set
			{
				percent = value;
				RaiseUpdate("Percent");
			}
		}

		public string SkillTurn
		{
			get { return skillTurn; }
			set
			{
				skillTurn = value;
				RaiseUpdate("SkillTurn");
			}
		}

		public Action Pressed
		{
			get { return pressed; }
			set
			{
				pressed = value;
				RaiseUpdate("Pressed");
			}
		}

		public Action LongPressed
		{
			get { return longPressed; }
			set
			{
				longPressed = value;
				RaiseUpdate("LongPressed");
			}
		}

		public bool IsDead
		{
			get { return isDead; }
			set
			{
				isDead = value;
				RaiseUpdate("IsDead");
			}
		}
	}
}