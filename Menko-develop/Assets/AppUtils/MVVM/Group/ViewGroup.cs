using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.MVVM
{
	/// <summary>
	/// リスト型のViewModel連携の構築クラス
	/// </summary>
	[AddComponentMenu("MVVM/View Group", 1)]
	public class ViewGroup : View
	{
		[SerializeField] ViewGroupElement template;
		[SerializeField] List<ViewGroupElement> contents;
		[Header("Property Names")]
		[SerializeField] string listName;

		protected override void Init()
		{
			if (template)
			{
				template.gameObject.SetActive(false);
			}
			AddUpdater(listName, listUpdate);
		}

		void listUpdate(object value)
		{
			IList list = (IList)value;
			int count = list.Count;

			if (count > contents.Count)
			{
				addContents(count - contents.Count);
			}
			else if (count < contents.Count)
			{
				removeContents(contents.Count - count);
			}
			updateContents(list);
		}

		void addContents(int addCount)
		{
			if (template == null)
			{
				return;
			}

			for (int i = 0; i < addCount; ++i)
			{
				createContent();
			}
		}

		void removeContents(int removeCount)
		{
			if (template == null)
			{
				return;
			}

			for (int i = removeCount - 1; 0 <= i; --i)
			{
				Destroy(contents[i].gameObject);
				contents.RemoveAt(i);
			}
		}

		void createContent()
		{
			var element = Instantiate<ViewGroupElement>(template);
			element.Parent = template.Parent;
			element.gameObject.SetActive(true);
			contents.Add(element);
		}

		void updateContents(IList list)
		{
			int count = contents.Count;
			for (int i = 0; i < count; ++i)
			{
				var element = contents[i];
				var views = element.Views;
				for (int j = 0; j < views.Length; ++j)
				{
					var view = views[j];
					view.DataContext = (ViewModel)list[i];
					view.UpdateAllParam();
				}
			}
		}
	}
}