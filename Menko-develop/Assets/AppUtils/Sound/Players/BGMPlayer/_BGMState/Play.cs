using UnityEngine;
using AppUtils;

namespace AppUtils.SoundPlayer
{
	class Play : BGMState
	{
		public Play(AudioDataContainer audio) : base(audio)
		{
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			audio.main.volume = SoundVolume.PlayBGMVolume;

			if (audio.main.isPlaying == false)
			{
				audio.main.Play();
			}
		}

		public override void Update(StateMediator<PlayState> mediator)
		{
			audio.main.volume = SoundVolume.PlayBGMVolume;
		}

		public override PlayState GetKey()
		{
			return PlayState.Play;
		}
	}
}