using UnityEngine;


namespace AppUtils.SoundPlayer
{
	public class FadeIn : BGMState
	{
		IState<PlayState> nextState;
		float fadeAmount;
		float fadeTime;
		float timer;

		public FadeIn(AudioDataContainer audio) : base(audio)
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
			audio.main.volume = 0f;

			if (SoundVolume.PlayBGMVolume <= 0f)
			{
				timer = fadeTime;
			}
			else
			{
				fadeAmount = (SoundVolume.PlayBGMVolume - audio.main.volume) / fadeTime;
			}

			audio.main.Play();
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
			return PlayState.FadeIn;
		}
	}
}