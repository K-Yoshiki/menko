using System;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils
{
	/// <summary>
	/// ステートのキャッシュ機構
	/// </summary>
	public class StateCache<StateKey>
	{
		Dictionary<StateKey, IState<StateKey>> cache;

		public StateCache()
		{
			cache = new Dictionary<StateKey, IState<StateKey>>();
		}

		/// <summary>
		/// ステートをキャッシュする
		/// </summary>
		public IState<StateKey> CacheState(IState<StateKey> state)
		{
			return cache[state.GetKey()] = state;
		}

		/// <summary>
		/// キャッシュしたステートの取得
		/// </summary>
		public IState<StateKey> GetState(StateKey key)
		{
			return cache[key];
		}

		/// <summary>
		/// キャッシュしたステートの取得をします。キャッシュされていない場合、生成して返します。
		/// </summary>
		/// <returns>The state.</returns>
		/// <param name="key">Key.</param>
		/// <param name="args">Arguments.</param>
		public StateClass GetState<StateClass>(StateKey key, params object[] args) where StateClass : IState<StateKey>
		{
			if (IsCached(key))
			{
				return (StateClass)GetState(key);
			}
			StateClass state = (StateClass)Activator.CreateInstance(typeof(StateClass), args);
			this.CacheState(state);
			return state;
		}

		/// <summary>
		/// キャッシュしたステートの取得を試みる
		/// </summary>
		public bool TryGetState(StateKey key, out IState<StateKey> state)
		{
			return cache.TryGetValue(key, out state);
		}

		/// <summary>
		/// 指定ステートがキャッシュされているかどうか
		/// </summary>
		public bool IsCached(StateKey key)
		{
			return cache.ContainsKey(key);
		}

		/// <summary>
		/// 指定キャッシュを消去する
		/// </summary>
		public bool Clear(StateKey key)
		{
			return cache.Remove(key);
		}

		/// <summary>
		/// キャッシュを全消去する
		/// </summary>
		public void ClearAll()
		{
			cache.Clear();
		}
	}
}