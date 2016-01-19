using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AppUtils.MVVM
{
	/*
	 * ViewModel と連携した View 要素を構築したい場合は、このクラスを継承してください。
	 * 
	 * また、このクラスの派生クラスを作る場合、
	 * 受け取ったデータをどのように処理するかをAddUpdaterに登録してください。
	 * 
	**/

	/// <summary>
	/// View要素のベースクラス
	/// </summary>
	public abstract class View : MonoBehaviour
	{
		[SerializeField] string viewModelName;
		Dictionary<string, Action<object>> updaters;
		ViewModel dataContext;

		/// <summary>
		/// 監視データ
		/// </summary>
		public ViewModel DataContext
		{
			set
			{
				RemoveEvent();
				dataContext = value;
				dataContext.UpdateEvent += UpdateParam;
			}
		}

		/// <summary>
		/// 監視するビューモデル名
		/// </summary>
		public string VMName
		{
			get { return viewModelName; }
		}

		/// <summary>
		/// 全てのパラメータ情報を更新する
		/// </summary>
		/// <value>The update all parameter.</value>
		public void UpdateAllParam()
		{
			foreach (var key in updaters.Keys)
			{
				UpdateParam(key);
			}
		}

		/// <summary>
		/// Awake時に呼び出されます
		/// </summary>
		protected abstract void Init();

		/// <summary>
		/// プロパティの追加
		/// </summary>
		/// <param name="updateAct">アップデート関数</param>
		protected void AddUpdater(string propertyName, Action<object> updateAct)
		{
			if (propertyName != "")
			{
				updaters[propertyName] = updateAct;
			}
		}

		/// <summary>
		/// プロパティをView側から変更する
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		/// <param name="property">Property.</param>
		protected void SetValue(string propertyName, object property)
		{
			var accessor = GetAccessor(propertyName);
			if (accessor != null)
			{
				accessor.SetValue(property);
			}
		}

		void Awake()
		{
			updaters = new Dictionary<string, Action<object>>();
			Init();
		}

		void Start()
		{
			BindRoot();
		}

		void OnDestroy()
		{
			RemoveEvent();
		}

		void RemoveEvent()
		{
			if (dataContext != null)
			{
				dataContext.UpdateEvent -= UpdateParam;
			}
		}

		/// <summary>
		/// このViewをrootに登録する
		/// </summary>
		void BindRoot()
		{
			// ViewModelNameが入力されていない場合は登録しない
			if (string.IsNullOrEmpty(viewModelName))
			{
				return;
			}
			
			ViewRoot[] roots = GetComponentsInParent<ViewRoot>(true);
			for (int i = 0; i < roots.Length; ++i)
			{
				roots[i].Bind(this);
			}
		}

		/// <summary>
		/// アクセサをコンテクストから取得
		/// </summary>
		/// <returns>The accessor.</returns>
		/// <param name="propertyName">Property name.</param>
		IAccessor GetAccessor(string propertyName)
		{
			return dataContext.GetAccessor(propertyName);
		}
			
		/// <summary>
		/// プロパティのアップデート
		/// </summary>
		/// <param name="propertyName">プロパティ名</param>
		void UpdateParam(string propertyName)
		{
			// プロパティが含まれていない場合は実行せず
			if (updaters.ContainsKey(propertyName) == false)
			{
				return;
			}

			var accessor = GetAccessor(propertyName);
			if (accessor != null)
			{
				updaters[propertyName](accessor.GetValue());
			}
		}
	}
}