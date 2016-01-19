using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class CrossFade : BGMState
	{
		IState<PlayState> nextState;
		float fadeInAmount;
		float fadeOutAmount;
		float fadeTime;
		float timer;

		public CrossFade(AudioDataContainer audio) : base(audio) {}

		public void Set(AudioClip changeClip, float fadeTime, IState<PlayState> nextState)
		{
			this.audio.SwapSource();
			this.audio.main.clip = changeClip;
			this.fadeTime = fadeTime;
			this.nextState = nextState;
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			if (SoundVolume.PlayBGMVolume <= 0f)
			{
				timer = fadeTime;
				return;
			}

			timer = 0;
			fadeInAmount = SoundVolume.PlayBGMVolume / fadeTime;
			fadeOutAmount = (audio.sub.volume / fadeTime) * -1f;

			audio.main.volume = 0f;
			audio.main.Play();
		}

		public override void Update(StateMediator<PlayState> mediator)
		{
			timer += Time.deltaTime;
			audio.sub.volume += fadeOutAmount * Time.deltaTime;
			audio.main.volume += fadeInAmount * Time.deltaTime;

			if (IsEnd())
			{
				mediator.SetState(nextState);
			}
		}

		public override void Exit(StateMediator<PlayState> mediator)
		{
			audio.sub.Stop();
			audio.sub.clip = null;
		}

		public override bool IsEnd()
		{
			return timer >= fadeTime;
		}

		public override PlayState GetKey()
		{
			return PlayState.CrossFade;
		}
	}
}