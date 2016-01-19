using UnityEngine;
using System.Collections;

public class Prototype_InitButton : MonoBehaviour
{
	public void OnClick()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}