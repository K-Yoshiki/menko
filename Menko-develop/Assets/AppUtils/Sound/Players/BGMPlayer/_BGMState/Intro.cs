using UnityEngine;
using System.Collections;

namespace AppUtils.SoundPlayer
{
	public class Intro : BGMState
	{
		IState<PlayState> nextState;
		AudioClip loop;

		public Intro(AudioDataContainer audio) : base(audio)
		{
		}

		public void Set(AudioClip loop, IState<PlayState> nextState)
		{
			this.loop = loop;
			this.nextState = nextState;
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			audio.main.volume = SoundVolume.PlayBGMVolume;

			if (audio.main.isPlaying == false)
			{
				audio.main.Play();
			}

			// 現在再生している音源側をSubSourceに移動
			this.audio.SwapSource();
			this.audio.main.clip = this.loop;
			this.audio.main.loop = true;
			this.audio.sub.loop = false;

			// 現在のtimeSampleと曲全体のtimeSampleを比較して,
			// この曲の終端でループ音源を再生開始予約する.
			var totalSec = this.audio.sub.clip.length;
			var playSec = this.audio.sub.source.time;
			var delaySec = totalSec - playSec;
			this.audio.main.Play(delaySec);
		}

		public override void Update(StateMediator<PlayState> mediator)
		{
			audio.main.volume = SoundVolume.PlayBGMVolume;
			audio.sub.volume = SoundVolume.PlayBGMVolume;

			// イントロが終了していたらnextStateに移行
			if (this.audio.sub.isPlaying == false)
			{
				mediator.SetState(nextState);
			}
		}

		public override void Exit(StateMediator<PlayState> mediator)
		{
			this.audio.sub.clip = null;
		}

		public override PlayState GetKey()
		{
			return PlayState.Intro;
		}
	}
}