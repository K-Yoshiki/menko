using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugInfo : MonoBehaviour
{
	public Text uiText;
	public float interval = 1.0f;

	private int frameCount;
	private float frameTimer;

	const int MBFix = 1024 * 1024;
	const string format = "Fps:\n {0}fps\nMem:\n {1}/{2}MB";


	void Start()
	{
		#if DEBUG
		DontDestroyOnLoad(this.gameObject);
		StartCoroutine(UpdateUI());
		#else
		Destroy(gameObject);
		#endif
	}

	void Update()
	{
		++frameCount;
		frameTimer += Time.unscaledDeltaTime;
	}

	IEnumerator UpdateUI()
	{
		while (true)
		{
			uiText.text = string.Format(
				format,
				(frameCount / frameTimer).ToString("F"),
				Profiler.GetTotalAllocatedMemory() / MBFix,
				(Profiler.GetTotalReservedMemory() + Profiler.GetTotalUnusedReservedMemory()) / MBFix
			);
			frameCount = 0;
			frameTimer = 0;
			yield return new WaitForSeconds(interval);
		}
	}
}