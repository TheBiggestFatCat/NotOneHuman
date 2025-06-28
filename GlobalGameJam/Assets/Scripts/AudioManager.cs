using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource_AMB;
    public AudioSource audioSource_SFX;
    public AudioSource audioSource_BGM;
    public List<AudioClip> AMB_List;
    public List<AudioClip> SE_List;
    public List<AudioClip> BGM_List;
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }
    
    public void PlayAmb(int index)
    {
        if(audioSource_AMB.isPlaying)
        {
            audioSource_AMB.Stop();
        }
        audioSource_AMB.clip = AMB_List[index];
        audioSource_AMB.Play();
    }

    public void PlaySFX(int index)
    {
        if(SE_List[index] == null)
        {
            Debug.Log("²¥·ÅÒôÐ§Îª¿Õ£¬index = " + index);
            return;
        }
        audioSource_SFX.PlayOneShot(SE_List[index]);
    }

    public void StopAmb()
    {
        if (audioSource_AMB.isPlaying)
        {
            audioSource_AMB.Stop();
        }
    }

    public void PlayBGM(int index)
    {
        if (audioSource_BGM.isPlaying)
        {
            audioSource_BGM.Stop();
        }
        audioSource_BGM.clip = BGM_List[index];
        audioSource_BGM.Play();
    }

    public void StopBGM()
    {
        if (audioSource_BGM.isPlaying)
        {
            audioSource_BGM.Stop();
        }
    }
}
