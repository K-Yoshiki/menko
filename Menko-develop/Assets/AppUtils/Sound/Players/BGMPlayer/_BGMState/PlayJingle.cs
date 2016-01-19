using UnityEngine;
using AppUtils;

namespace AppUtils.SoundPlayer
{
	class PlayJingle : BGMState
	{
		IState<PlayState> nextState;

		public PlayJingle(AudioDataContainer audio) : base(audio)
		{
		}

		public void Set(AudioClip jingleClip, IState<PlayState> nextState)
		{
			this.audio.sub.clip = jingleClip;
			this.nextState = nextState;
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			this.audio.sub.volume = SoundVolume.PlayBGMVolume;
			this.audio.sub.loop = false;

			if (audio.main.isPlaying)
				audio.main.Pause();

			this.audio.sub.Play();
		}

		public override void Update(StateMediator<PlayState> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(nextState);
			}
		}

		public override void Exit(StateMediator<PlayState> mediator)
		{
			this.audio.sub.clip = null;
		}

		public override bool IsEnd()
		{
			return this.audio.sub.isPlaying == false;
		}

		public override PlayState GetKey()
		{
			return PlayState.PlayJingle;
		}
	}
}