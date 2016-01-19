using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.UserControls
{
	public class TouchInfoList
	{
		Dictionary<int, TouchInfo> infos;
		TouchTaskList taskList;

		public TouchInfoList(TouchTaskList taskList)
		{
			this.infos = new Dictionary<int, TouchInfo>();
			this.taskList = taskList;
		}

		public void Update(Touch[] touches)
		{
			closeInfo();
			for (int i = 0; i < touches.Length; ++i)
			{
				updateInfo(ref touches[i]);
			}
		}

		public void Clear()
		{
			infos.Clear();
		}

		public int Count
		{
			get { return infos.Count; }
		}

		void closeInfo()
		{
			foreach (var info in infos.Values)
			{
				if (info.State == TouchState.Exit ||
				    info.State == TouchState.Cancel)
				{
					info.Close();
				}
			}
		}

		void updateInfo(ref Touch touch)
		{
			TouchInfo info;
			if (!infos.TryGetValue(touch.fingerId, out info))
			{
				info = new TouchInfo(touch);
				infos.Add(info.FingerID, info);
			}

			if (touch.phase == TouchPhase.Began)
			{
				info.Init(touch);
			}
			else
			{
				info.Update(touch);
			}

			taskList.Add(info);
		}
	}
}