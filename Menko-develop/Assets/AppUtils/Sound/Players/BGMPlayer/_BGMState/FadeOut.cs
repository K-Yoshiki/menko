using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class FadeOut : BGMState
	{
		IState<PlayState> nextState;
		float fadeAmount;
		float fadeTime;
		float timer;

		public FadeOut(AudioDataContainer audio) : base(audio)
		{
		}

		public void Set(float fadeTime, IState<PlayState> nextState)
		{
			this.fadeTime = Mathf.Abs(fadeTime);
			this.nextState = nextState;
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			timer = 0;

			if (SoundVolume.PlayBGMVolume <= 0f)
			{
				timer = fadeTime;
			}
			else
			{
				fadeAmount = (audio.main.volume / fadeTime) * -1f;
			}
		}

		public override void Update(StateMediator<PlayState> mediator)
		{
			timer += Time.deltaTime;
			audio.main.volume += fadeAmount * Time.deltaTime;

			if (IsEnd())
			{
				mediator.SetState(nextState);
			}
		}

		public override bool IsEnd()
		{
			return timer >= fadeTime;
		}

		public override PlayState GetKey()
		{
			return PlayState.FadeOut;
		}
	}
}