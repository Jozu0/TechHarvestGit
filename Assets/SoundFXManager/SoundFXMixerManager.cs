using UnityEngine;
using UnityEngine.Audio;
public class SoundFXMixerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] private AudioMixer audioMixer;


   void Start()
   {
       if (audioMixer == null)
       {
           
       }
   }
   public void SetFXVolume(float volume)
   {
       audioMixer.SetFloat("FXVolume", Mathf.Log10(volume) * 20);
   }
   
   public void SetMusicVolume(float volume)
   {
       audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
   }
   
}
