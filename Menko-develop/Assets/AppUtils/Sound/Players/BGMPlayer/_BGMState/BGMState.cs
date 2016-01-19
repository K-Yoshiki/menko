using UnityEngine;
using AppUtils;

namespace AppUtils.SoundPlayer
{
	public abstract class BGMState : IState<PlayState>
	{
		protected AudioDataContainer audio;

		public BGMState(AudioDataContainer audio)
		{
			this.audio = audio;
		}
			
		public virtual void Init(StateMediator<PlayState> mediator)
		{
		}

		public virtual void Update(StateMediator<PlayState> mediator)
		{
		}

		public virtual void Exit(StateMediator<PlayState> mediator)
		{
		}

		public virtual bool IsEnd()
		{
			return false;
		}

		public abstract PlayState GetKey();
	}
}