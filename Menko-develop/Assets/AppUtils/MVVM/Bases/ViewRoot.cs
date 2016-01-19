using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.MVVM
{
	/// <summary>
	/// ビューの管理クラス
	/// </summary>
	[AddComponentMenu("MVVM/View Root", 0)]
	public class ViewRoot : MonoBehaviour
	{
		Dictionary<string, ViewModel> viewModels;
		List<View> views;

		Dictionary<string, ViewModel> ViewModels
		{
			get
			{
				if (viewModels == null)
					viewModels = new Dictionary<string, ViewModel>();
				return viewModels;
			}
		}

		List<View> Views
		{
			get
			{
				if (views == null)
					views = new List<View>();
				return views;
			}
		}

		/// <summary>
		/// ビューの登録
		/// </summary>
		/// <param name="view">View.</param>
		public void Bind(View view)
		{
			Views.Add(view);
			SetContext(view);
		}

		/// <summary>
		/// ビューモデルの登録
		/// </summary>
		/// <param name="vm">Vm.</param>
		public void Bind(ViewModel vm)
		{
			string key = vm.GetType().Name;
			ViewModels[key] = vm;
		}

		public void SetContext()
		{
			foreach(var view in Views)
			{
				SetContext(view);
			}
		}

		/// <summary>
		/// ビューに指定ビューモデルを登録していく
		/// </summary>
		void SetContext(View view)
		{
			if (ViewModels.ContainsKey(view.VMName))
			{
				view.DataContext = ViewModels[view.VMName];
				view.UpdateAllParam();
			}
		}
	}
}