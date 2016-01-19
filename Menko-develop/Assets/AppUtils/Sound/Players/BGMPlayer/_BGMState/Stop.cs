using UnityEngine;
using AppUtils;

namespace AppUtils.SoundPlayer
{
	class Stop : BGMState
	{
		public Stop(AudioDataContainer audio) : base(audio)
		{
		}

		public override void Init(StateMediator<PlayState> mediator)
		{
			audio.main.Stop();
		}

		public override PlayState GetKey()
		{
			return PlayState.Stop;
		}
	}
}