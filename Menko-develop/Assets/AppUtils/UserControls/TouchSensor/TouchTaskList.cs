using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils.Events;

namespace AppUtils.UserControls
{
	public class TouchTaskList
	{
		Dictionary<TouchState, List<TouchInfo>> actionTask;

		public TouchTaskList()
		{
			actionTask = new Dictionary<TouchState, List<TouchInfo>>();
		}

		public void Add(TouchInfo info)
		{
			getList(info.State).Add(info);
		}

		public void Remove(TouchInfo info)
		{
			getList(info.State).Remove(info);
		}

		List<TouchInfo> getList(TouchState key)
		{
			List<TouchInfo> result;
			if (!actionTask.TryGetValue(key, out result))
			{
				result = new List<TouchInfo>();
				actionTask.Add(key, result);
			}
			return result;
		}

		public void Clear()
		{
			foreach (var value in actionTask.Values)
			{
				value.Clear();
			}
			actionTask.Clear();
		}

		public void ExecuteAction(ActionEvents<TouchState, IGestureInfo[]> actions)
		{
			foreach (var pair in actionTask)
			{
				pair.Value.Sort(compareByTouchInfo);
				actions.Execute(pair.Key, pair.Value.ToArray());
				pair.Value.Clear();
			}
			Clear();
		}

		int compareByTouchInfo(TouchInfo a, TouchInfo b)
		{
			return a.FingerID - b.FingerID;
		}
	}
}