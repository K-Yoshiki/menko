using System;
using System.Collections;

public static class IEnumerableExtention 
{
	public static void Foreach<T>(this T[] array, Action<T> action)
	{
		int length = array.Length;
		for (int i = 0; i < length; ++i)
		{
			action(array[i]);
		}
	}
}