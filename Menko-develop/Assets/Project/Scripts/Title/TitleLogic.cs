using UnityEngine;
using System.Collections;
using AppUtils;
using MenkoiMonster.Scene;
using MenkoiMonster;

public class TitleLogic : MonoBehaviour {

    public AudioClip _titleSE;
    public AudioClip _titleBGM;

    public void PressScreen()
    {
        Sound.Instance.PlaySE(_titleSE);
        Sound.Instance.StopBGM();
        SceneManager.Instance.ChangeScene(new HomeScene());
    }

    void Start()
    {
        Sound.Instance.PlayBGM(_titleBGM);
    }

}
