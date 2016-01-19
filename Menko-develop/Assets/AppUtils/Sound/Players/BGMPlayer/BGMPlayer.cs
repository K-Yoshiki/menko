using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.SoundPlayer;

namespace AppUtils.SoundPlayer
{
	public enum PlayState
	{
		Play,
		Pause,
		Stop,
		FadeIn,
		FadeOut,
		CrossFade,
		PlayJingle,
		Intro
	}

	public sealed class AudioDataContainer
	{
		public AudioData main;
		public AudioData sub;

		public AudioDataContainer(AudioSource source)
		{
			main = new AudioData(source);
			sub = new AudioData(source.gameObject.AddComponent<AudioSource>());
		}

		public void SwapSource()
		{
			var temp = main.source;
			main.source = sub.source;
			sub.source = temp;
		}
	}

	public sealed class BGMPlayer
	{
		StateMachine<PlayState> stateMachine;
		StateCache<PlayState> stateCache;
		AudioDataContainer audio;

		public BGMPlayer(AudioSource source)
		{
			audio = new AudioDataContainer(source);
			stateCache = new StateCache<PlayState>();
			stateMachine = new StateMachine<PlayState>();
			stateMachine.SetState(getCahceState<Stop>(PlayState.Stop));
		}

		/// <summary>
		/// BGMの再生
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="fadeTime">Fade time.</param>
		public void Play(AudioClip clip, float fadeTime, bool isLoop)
		{
			if (audio.main.clip == clip)
				return;

			audio.main.loop = isLoop;

			// Play
			if (fadeTime <= 0f)
			{
				onPlay(clip);
				return;
			}

			// Cross Fade -> Play
			if (stateMachine.CurrentKey != PlayState.Stop &&
				stateMachine.CurrentKey != PlayState.Pause)
			{
				onCrossFadePlay(clip, fadeTime, getCahceState<Play>(PlayState.Play));
			}
			else // Fade In -> Play
			{
				onFadeInPlay(clip, fadeTime, getCahceState<Play>(PlayState.Play));
			}
		}

		/// <summary>
		/// イントロ付きループ再生
		/// </summary>
		/// <param name="intro">Intro.</param>
		/// <param name="loopClip">Loop clip.</param>
		/// <param name="fadeTime">Fade time.</param>
		public void PlayIntro(AudioClip intro, AudioClip loopClip, float fadeTime)
		{
			// Intro Play
			if (fadeTime <= 0f)
			{
				onIntroPlay(intro, loopClip);
				return;
			}

			// Cross Fade -> Intro -> Play
			if (stateMachine.CurrentKey != PlayState.Stop &&
			    stateMachine.CurrentKey != PlayState.Pause)
			{
				onIntroCrossFade(intro, loopClip, fadeTime);
			}
			else // Fade In -> Intro -> Play
			{
				onIntroFadeInPlay(intro, loopClip, fadeTime);
			}
		}

		/// <summary>
		/// ジングルの再生
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="unPauseFadeTime">Un pause fade time.</param>
		public void PlayJingle(AudioClip clip, float unPauseFadeTime)
		{
			IState<PlayState> nextState;
			if (stateMachine.CurrentKey != PlayState.Stop)
			{
				nextState = getCahceState<FadeIn>(PlayState.FadeIn);
				((FadeIn)nextState).Set(unPauseFadeTime, getCahceState<Play>(PlayState.Play));
			}
			else
			{
				nextState = getCahceState<Stop>(PlayState.Stop);
			}

			var state = getCahceState<PlayJingle>(PlayState.PlayJingle);
			state.Set(clip, nextState);
			setState(state);
		}

		/// <summary>
		/// Pause BGM
		/// </summary>
		/// <param name="fadeTime">Fade time.</param>
		public void Pause(float fadeTime)
		{
			if (stateMachine.CurrentKey == PlayState.Stop &&
				stateMachine.CurrentKey == PlayState.Pause)
				return;

			if (fadeTime <= 0f)
			{
				setState(getCahceState<Pause>(PlayState.Pause));
			}
			else
			{
				var state = getCahceState<FadeOut>(PlayState.FadeOut);
				state.Set(fadeTime, getCahceState<Pause>(PlayState.Pause));
				setState(state);
			}
		}

		/// <summary>
		/// UnPause BGM
		/// </summary>
		/// <param name="fadeTime">Fade time.</param>
		public void UnPause(float fadeTime)
		{
			if (stateMachine.CurrentKey != PlayState.Pause)
				Play(audio.main.clip, fadeTime, true);
		}

		/// <summary>
		/// Stop BGM
		/// </summary>
		/// <param name="fadeTime">Fade time.</param>
		public void Stop(float fadeTime)
		{
			if (stateMachine.CurrentKey == PlayState.Stop)
				return;

			if (fadeTime <= 0f)
			{
				setState(getCahceState<Stop>(PlayState.Stop));
			}
			else
			{
				var state = getCahceState<FadeOut>(PlayState.FadeOut);
				state.Set(fadeTime, getCahceState<Stop>(PlayState.Stop));
				setState(state);
			}
		}

		/// <summary>
		/// Player Update
		/// </summary>
		public void Update()
		{
			stateMachine.UpdateState();
		}

		void setState(IState<PlayState> state)
		{
			stateMachine.SetState(state);
		}

		StateClass getCahceState<StateClass>(PlayState stateKey) where StateClass : IState<PlayState>
		{
			return stateCache.GetState<StateClass>(stateKey, audio);
		}

		#region Play
		void onPlay(AudioClip clip)
		{
			audio.main.clip = clip;
			setState(getCahceState<Play>(PlayState.Play));
		}

		void onFadeInPlay(AudioClip clip, float fadeTime, IState<PlayState> next)
		{
			audio.main.clip = clip;
			var state = getCahceState<FadeIn>(PlayState.FadeIn);
			state.Set(fadeTime, next);
			setState(state);
		}

		void onCrossFadePlay(AudioClip clip, float fadeTime, IState<PlayState> next)
		{
			var state = getCahceState<CrossFade>(PlayState.CrossFade);
			state.Set(clip, fadeTime, next);
			setState(state);
		}
		#endregion

		#region Intro
		void onIntroPlay(AudioClip intro, AudioClip loop)
		{
			audio.main.clip = intro;
			var state = getCahceState<Intro>(PlayState.Intro);
			state.Set(loop, getCahceState<Play>(PlayState.Play));
			setState(state);
		}

		void onIntroFadeInPlay(AudioClip intro, AudioClip loop, float fadeTime)
		{
			audio.main.clip = intro;
			var fadeIn = getCahceState<FadeIn>(PlayState.FadeIn);
			var introPlay = getCahceState<Intro>(PlayState.Intro);
			introPlay.Set(loop, getCahceState<Play>(PlayState.Play));
			fadeIn.Set(fadeTime, introPlay);
			setState(fadeIn);
		}

		void onIntroCrossFade(AudioClip intro, AudioClip loop, float fadeTime)
		{
			var crossFade = getCahceState<CrossFade>(PlayState.CrossFade);
			var introPlay = getCahceState<Intro>(PlayState.Intro);
			introPlay.Set(loop, getCahceState<Play>(PlayState.Play));
			crossFade.Set(intro, fadeTime, introPlay);
			setState(crossFade);
		}
		#endregion
	}
}