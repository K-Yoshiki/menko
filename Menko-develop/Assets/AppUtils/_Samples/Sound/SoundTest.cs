using UnityEngine;
using System.Collections;
using AppUtils;

// Need using AppUtils.
namespace AppUtils.Samples
{
    public class SoundTest : MonoBehaviour
    {
        public VolumeControl inspecterVolume;
		[Range(0, 10)] public float fadeTime = 1.0f;
		public AudioClip BGM;
        public AudioClip BGM2;
        public AudioClip SE;
        public AudioClip jingle;
        public AudioClip intro;
        public AudioClip introLoop;

        void Start()
        {
			VolumeSet();
        }

        void Update()
        {
			VolumeSet();
        }

		void VolumeSet()
		{
			SoundVolume.Master = (inspecterVolume.MasterVolume * 0.01f);
			SoundVolume.BGM = (inspecterVolume.BGMVolume * 0.01f);
			SoundVolume.SE = (inspecterVolume.SEVolume * 0.01f);
		}

		public void PlayBGM()
        {
            Sound.Instance.PlayBGM(BGM, fadeTime);
        }

        public void PlayBGM2()
        {
            Sound.Instance.PlayBGM(BGM2, fadeTime);
        }

        public void PlayJignle()
        {
            Sound.Instance.PlayJingle(jingle, fadeTime);
        }

        public void PlayIntro()
        {
            Sound.Instance.PlayBGMWithIntro(intro, introLoop, fadeTime);
        }

        public void PauseBGM()
        {
            Sound.Instance.PauseBGM(fadeTime);
        }

        public void StopBGM()
        {
            Sound.Instance.StopBGM(fadeTime);
        }

        public void PlaySE()
        {
            Sound.Instance.PlaySE(SE);
        }

        public void AllUnload()
        {
            Sound.Instance.AllUnload();
        }
    }

    [System.Serializable]
    public class VolumeControl
    {
        [Range(0, 100)]
        public int MasterVolume = 50;
        [Range(0, 100)]
        public int BGMVolume = 50;
        [Range(0, 100)]
        public int SEVolume = 50;
    }
}