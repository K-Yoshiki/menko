using UnityEngine;

namespace AppUtils.SoundPlayer
{
	/// <summary>
	/// Wrapping class of AudioSource
	/// </summary>
	public sealed class AudioData
	{
		AudioSource m_Source;

		public AudioData(AudioSource source)
		{
			this.m_Source = source;
			source.playOnAwake = false;
			source.loop = true;
		}

		public AudioSource source
		{
			get { return m_Source; }
			set { m_Source = value; }
		}

		public AudioClip clip
		{
			get { return m_Source.clip; }
			set
			{
				if (m_Source.clip != value)
					m_Source.clip = value;
			}
		}

		public float volume
		{
			get { return m_Source.volume; }
			set { m_Source.volume = value; }
		}

		public bool isPlaying
		{
			get { return m_Source.isPlaying; }
		}

		public bool loop
		{
			get { return m_Source.loop; }
			set { m_Source.loop = value; }
		}

		public bool playOnAwake
		{
			get { return m_Source.playOnAwake; }
			set { m_Source.playOnAwake = value; }
		}

		public void Play()
		{
			m_Source.Play();
		}

		public void Play(float delaySec)
		{
			m_Source.PlayDelayed(delaySec);
		}

		public void Pause()
		{
			m_Source.Pause();
		}

		public void UnPause()
		{
			m_Source.UnPause();
		}

		public void Stop()
		{
			m_Source.Stop();
		}	
	}
}