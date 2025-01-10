using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DiffenceSE : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 10);
    }

    public void SoundOneShot(AudioClip sound)
    {
        //Debug.Log(sound);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.PlayOneShot(sound);
    }
}
