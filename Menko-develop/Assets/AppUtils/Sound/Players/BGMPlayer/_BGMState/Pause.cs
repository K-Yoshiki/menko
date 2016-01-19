using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class Pause : BGMState
	{
		public Pause(AudioDataContainer audio) : base(audio)
		{
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			if (audio.main.isPlaying)
			{
				audio.main.Pause();
			}
		}

		public override PlayState GetKey()
		{
			return PlayState.Pause;
		}
	}
}