using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuController : MonoBehaviour
{
    public Slider sliderMusic;
    public Slider sliderSFX;
    public void SetVolumeSFX(){
        AudioManager.instance.SetVolumeSFX(this.sliderSFX.value);
    }
    public void SetVolumeMusic(){
        AudioManager.instance.SetVolumeTheme(this.sliderMusic.value);

    }
}
