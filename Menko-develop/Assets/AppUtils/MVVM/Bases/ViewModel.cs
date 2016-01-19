using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.MVVM
{
	/// <summary>
	/// ビューモデルベース
	/// </summary>
	public abstract class ViewModel
	{
		/// <summary>
		/// 変更通知のイベント
		/// </summary>
		public event Action<string> UpdateEvent;

		/// <summary>
		/// データへのアクセッサ
		/// </summary>
		Dictionary<string, IAccessor> accessors;

		public ViewModel()
		{
			accessors = new Dictionary<string, IAccessor>();
		}

		/// <summary>
		/// アクセッサの登録
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		/// <param name="accessor">Accessor.</param>
		protected void Bind(string propertyName, IAccessor accessor)
		{
			accessors[propertyName] = accessor;
		}

		/// <summary>
		/// アクセッサの登録
		/// </summary>
		/// <param name="propertyName">PropertyName.</param>
		/// <param name="getter">Getter.</param>
		/// <param name="setter">Setter.</param>
		/// <typeparam name="FieldType">The 1st type parameter.</typeparam>
		protected void Bind<FieldType>(string propertyName, Func<FieldType> getter, Action<FieldType> setter)
		{
			Accessor<FieldType> accessor = new Accessor<FieldType>(getter, setter);
			Bind(propertyName, accessor);
		}

		/// <summary>
		/// プロパティの変更通知
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected void RaiseUpdate(string propertyName)
		{
			if (UpdateEvent != null)
			{
				UpdateEvent(propertyName);
			}
		}

		/// <summary>
		/// アクセッサの取得
		/// </summary>
		/// <returns>The accessor.</returns>
		/// <param name="propertyName">Property name.</param>
		public IAccessor GetAccessor(string propertyName)
		{
			IAccessor accessor;
			if (accessors.TryGetValue(propertyName, out accessor))
			{
				return accessor;
			}
			return null;
		}
	}
}