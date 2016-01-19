using System;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.Events
{
	public class ActionEvents<Key, Arg>
	{
		Dictionary<Key, ActionEvent<Arg>> events;

		public ActionEvents()
		{
			events = new Dictionary<Key, ActionEvent<Arg>>();
		}

		public void Execute(Key key, Arg arg)
		{
			getEvent(key).Execute(arg);
		}

		public void Add(Key key, Action<Arg> action)
		{
			getEvent(key).OnEvent += action;
		}

		public void Remove(Key key, Action<Arg> action)
		{
			getEvent(key).OnEvent -= action;
		}

		public void ClearAll()
		{
			foreach (var value in events.Values)
			{
				value.Clear();
			}
			events.Clear();
		}

		public void ClearEvent(Key key)
		{
			getEvent(key).Clear();
		}

		ActionEvent<Arg> getEvent(Key key)
		{
			ActionEvent<Arg> result;
			if (!events.TryGetValue(key, out result))
			{
				result = new ActionEvent<Arg>();
				events.Add(key, result);
			}
			return result;
		}
	}

	public class ActionEvent<Arg>
	{
		public event Action<Arg> OnEvent;

		public void Execute(Arg arg)
		{
			if (OnEvent != null)
			{
				OnEvent(arg);
			}
		}

		public void Clear()
		{
			OnEvent = null;
		}
	}
}