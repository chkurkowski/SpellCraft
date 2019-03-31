using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
public class Settings : MonoBehaviour {
    public AudioMixer mixer;

   public void MasterVolume (float sliderValue)
   {
       mixer.SetFloat ("masterVol", Mathf.Log10 (sliderValue) *20);
   }
    public void MusicVolume (float sliderValue)
   {
       mixer.SetFloat ("musicVol", Mathf.Log10 (sliderValue) *20);
   }
    public void SFXVolume (float sliderValue)
   {
       mixer.SetFloat ("sfxVol", Mathf.Log10 (sliderValue) *20);
   }

}





    
     

