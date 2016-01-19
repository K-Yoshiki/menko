using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MenkoiMonster.Network
{
	public class RPCEventListener
	{
		Dictionary<string, Action<object[]>> rpcEvents;

		public RPCEventListener()
		{
			rpcEvents = new Dictionary<string, Action<object[]>>();
		}

		public void Invoke(string eventName, params object[] parameters)
		{
			Action<object[]> e;
			if (rpcEvents.TryGetValue(eventName, out e))
			{
				e.Invoke(parameters);
			}
		}

		public void AddEvent(string eventName, Action<object[]> e)
		{
			rpcEvents[eventName] = e;
		}

		public void RemoveEvent(string eventName)
		{
			if (rpcEvents.ContainsKey(eventName))
			{
				rpcEvents.Remove(eventName);
			}
		}
	}

//	public interface IRPCEvent
//	{
//		void Invoke(params object[] parameters);
//	}
}