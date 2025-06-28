using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public int bgmIndex;
    public int ambIndex;
    public bool autoPlayBGM;
    public bool autoPlayAMB;
    // Start is called before the first frame update
    void Start()
    {
        if(autoPlayAMB) AudioManager.Instance?.PlayAmb(ambIndex);
        if(autoPlayBGM) AudioManager.Instance?.PlayBGM(bgmIndex);
    }

    public void PlaySfx(int index)
    {
        AudioManager.Instance?.PlaySFX(index);
    }
}
