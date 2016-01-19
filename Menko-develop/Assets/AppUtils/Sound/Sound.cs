using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils.SoundPlayer;


namespace AppUtils
{
	public sealed class Sound : UnitySingleton<Sound>
	{
		Dictionary<string, AudioClip> clipData;
		BGMPlayer bgmPlayer;
		SEPlayer sePlayer;

		/// <summary>
		/// Play BGM.
		/// </summary>
		/// <param name="path">Resources in File path</param>
		/// <param name="fadeTime">Fade In Time</param>
		public void PlayBGM(string path, float fadeTime = 0f, bool isLoop = true)
		{
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				bgmPlayer.Play(clip, fadeTime, isLoop);
			}
		}

		/// <summary>
		/// Play BGM.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="fadeTime">Fade time.</param>
		public void PlayBGM(AudioClip clip, float fadeTime = 0f, bool isLoop = true)
		{
			bgmPlayer.Play(clip, fadeTime, isLoop);
		}

		/// <summary>
		/// Play BGM with intro.
		/// </summary>
		/// <param name="introPath">Intro path.</param>
		/// <param name="loopPath">Loop path.</param>
		/// <param name="fadeTime">Fade time.</param>
		/// <param name="isLoop">If set to <c>true</c> is loop.</param>
		public void PlayBGMWithIntro(string introPath, string loopPath, float fadeTime = 0f, bool isLoop = true)
		{
			AudioClip intro = ClipLoad(introPath);
			AudioClip loop = ClipLoad(loopPath);
			if (intro != null && loop != null)
			{
				bgmPlayer.PlayIntro(intro, loop, fadeTime);
			}
		}

		/// <summary>
		/// Play BGM with intro.
		/// </summary>
		/// <param name="intro">Intro.</param>
		/// <param name="loop">Loop.</param>
		/// <param name="fadeTime">Fade time.</param>
		/// <param name="isLoop">If set to <c>true</c> is loop.</param>
		public void PlayBGMWithIntro(AudioClip intro, AudioClip loop, float fadeTime = 0f, bool isLoop = true)
		{
			bgmPlayer.PlayIntro(intro, loop, fadeTime);
		}

		/// <summary>
		/// Play Jingle.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="fadeTime">Fade time.</param>
		public void PlayJingle(string path, float fadeTime = 0f)
		{
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				bgmPlayer.PlayJingle(clip, fadeTime);
			}
		}

		/// <summary>
		/// Play Jingle.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="fadeTime">Fade time.</param>
		public void PlayJingle(AudioClip clip, float fadeTime = 0f)
		{
			bgmPlayer.PlayJingle(clip, fadeTime);
		}

		/// <summary>
		/// Pause BGM.
		/// </summary>
		/// <param name="fadeTime">Fade Out Time.</param>
		public void PauseBGM(float fadeTime = 0f)
		{
			bgmPlayer.Pause(fadeTime);
		}

		/// <summary>
		/// UnPause BGM.
		/// </summary>
		/// <param name="fadeTime">Fade time.</param>
		public void UnPauseBGM(float fadeTime = 0f)
		{
			bgmPlayer.UnPause(fadeTime);
		}

		/// <summary>
		/// Stop BGM.
		/// </summary>
		/// <param name="fadeTime">Fade Out Time</param>
		public void StopBGM(float fadeTime = 0f)
		{
			bgmPlayer.Stop(fadeTime);
		}

		/// <summary>
		/// Play 2D SoundEffect.
		/// </summary>
		/// <param name="path">Resources in File path</param>
		public void PlaySE(string path)
		{
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				sePlayer.PlaySE(clip);
			}
		}

		/// <summary>
		/// Play 2D SoundEffect
		/// </summary>
		/// <param name="clip">Clip.</param>
		public void PlaySE(AudioClip clip)
		{
			sePlayer.PlaySE(clip);
		}

		/// <summary>
		/// Play 3D SoundEffect to assigned position.
		/// </summary>
		/// <param name="path">Resources in File path</param>
		/// <param name="pos">Effect Position</param>
		public void PlaySE(string path, Vector3 pos)
		{
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				sePlayer.PlaySE(clip, pos);
			}
		}

		/// <summary>
		/// Play 3D SoundEffect to assigned position.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="pos">Effect Position</param>
		public void PlaySE(AudioClip clip, Vector3 pos)
		{
			sePlayer.PlaySE(clip, pos);
		}

		/// <summary>
		/// Play 3D SoundEffect by following the parent object.
		/// </summary>
		/// <param name="path">Resources in File path</param>
		/// <param name="parent">Follow Parent</param>
		public void PlaySE(string path, Transform parent)
		{
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				sePlayer.PlaySE(clip, parent);
			}
		}

		/// <summary>
		/// Play 3D SoundEffect by following the parent object.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="parent">Follow Parent</param>
		public void PlaySE(AudioClip clip, Transform parent)
		{
			sePlayer.PlaySE(clip, parent);
		}

		/// <summary>
		/// Audio Data PreLoading.
		/// </summary>
		/// <param name="path">Resources in File path</param>
		public void Preload(string path)
		{
			ClipLoad(path);
		}

		/// <summary>
		/// Audio Data UnLoad.
		/// </summary>
		/// <param name="path">Path.</param>
		public void Unload(string path)
		{
			if (clipData.ContainsKey(path))
			{
				Resources.UnloadAsset(clipData[path]);
			}
		}

		/// <summary>
		/// All Audio Data UnLoad.
		/// </summary>
		public void AllUnload()
		{
			foreach (var clip in clipData.Values)
			{
				Resources.UnloadAsset(clip);
			}
		}

		/// <summary>
		/// Get Audio Play Time.
		/// </summary>
		/// <returns>The play time.</returns>
		/// <param name="path">Path.</param>
		public float GetPlayTime(string path)
		{
			float result = 0f;
			AudioClip clip = ClipLoad(path);
			if (clip != null)
			{
				result = clip.length;
			}
			return result;
		}

		protected override void Initialize()
		{
			DontDestroyOnLoad(this.gameObject);
			clipData = new Dictionary<string, AudioClip>();
			bgmPlayer = new BGMPlayer(gameObject.AddComponent<AudioSource>());
			sePlayer = new SEPlayer(this.transform);
		}

		AudioClip ClipLoad(string path)
		{
			if (!clipData.ContainsKey(path))
			{
				AudioClip clip = Resources.Load<AudioClip>(path);
				if (clip == null)
				{
					Debug.LogError("Not Found AudioClip Data. path = [" + path + "]");
					return null;
				}
				else
				{
					clipData.Add(path, clip);
				}
			}
			return clipData[path];
		}

		void Update()
		{
			bgmPlayer.Update();
		}
	}
}