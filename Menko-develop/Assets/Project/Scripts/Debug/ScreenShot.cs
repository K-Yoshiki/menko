using UnityEngine;

public class ScreenShot : MonoBehaviour
{
	public bool shotterButton;

	void OnValidate()
	{
		if (shotterButton)
		{
			shotterButton = false;
			Application.CaptureScreenshot("screenshot.png");
		}
	}
}