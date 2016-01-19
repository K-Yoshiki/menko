using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 読み込み中画面
/// </summary>
public class LoadingScreen : MonoBehaviour
{
	[SerializeField] CanvasGroup m_group;
	[SerializeField] Slider m_slider;
	[SerializeField] float m_fadeTime;
	bool m_isFade;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void ShowScreen()
	{
		if (m_isFade)
		{
			return;
		}
		m_group.alpha = 0;
		m_slider.value = m_slider.minValue;
		m_group.gameObject.SetActive(true);
		StartCoroutine(Fade(m_fadeTime));
	}

	public void HideScreen()
	{
		if (m_isFade)
		{
			return;
		}
		m_group.alpha = 1;
		StartCoroutine(Fade(m_fadeTime, true));
	}

	public void SetProgress(float prog)
	{
		m_slider.value = prog;
	}

	public bool IsFading()
	{
		return m_isFade;
	}

	IEnumerator Fade(float fadeTime, bool isFadeOut = false)
	{
		float amount = 1.0f / fadeTime;
		if (isFadeOut)
		{
			amount *= -1;
		}

		m_isFade = true;
		while (fadeTime > 0)
		{
			fadeTime -= Time.deltaTime;
			m_group.alpha += amount * Time.deltaTime;
			yield return null;
		}

		if (isFadeOut)
		{
			m_group.gameObject.SetActive(false);
		}
		m_isFade = false;
	}
}