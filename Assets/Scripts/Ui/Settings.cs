using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
public class Settings : MonoBehaviour {
   public AudioMixer masterMixer;
   public AudioMixer musicMixer;
   public AudioMixer sfxMixer;
   public AudioMixer uiMixer;



  


  public void SetMusic (float volume)
  {
      musicMixer.SetFloat("volume", volume);
     
  }
   public void SetMaster(float volume)
  {
      masterMixer.SetFloat("volume", volume);
     
  }
   public void SetSFX(float volume)
  {
       sfxMixer.SetFloat("volume", volume);

  }
   public void SetUI(float volume)
  {
       uiMixer.SetFloat("volume", volume);

  }
  
}





    
     

