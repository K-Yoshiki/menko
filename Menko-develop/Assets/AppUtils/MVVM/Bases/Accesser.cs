using System;
using System.Collections;
using System.Reflection;

namespace AppUtils.MVVM
{
	public interface IAccessor
	{
		object GetValue();
		void SetValue(object value);
	}

	/// <summary>
	/// プロパティアクセサ
	/// </summary>
	public sealed class Accessor<Field> : IAccessor
	{
		private Func<Field> getter;
		private Action<Field> setter;

		public Accessor(Func<Field> getter, Action<Field> setter)
		{
			this.getter = getter;
			this.setter = setter;
		}

		public object GetValue()
		{
			return this.getter();
		}

		public void SetValue(object value)
		{
			if (this.setter != null)
				this.setter((Field)value);
		}
	}
}