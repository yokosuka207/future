using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DiffenceSound : MonoBehaviour
{

    [SerializeField] private AudioClip sound1;
    [SerializeField] private GameObject soundObject;


    [SerializeField] private AudioMixerGroup audioMixer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShockWave"))
        {
            GameObject soundSpown = Instantiate(soundObject, this.gameObject.transform.position, Quaternion.identity);
            soundSpown.GetComponent<DiffenceSE>().SoundOneShot(sound1);
            //ƒTƒEƒ“ƒh‚ð—¬‚·
            //audioSource.PlayOneShot(sound1);
            //Debug.Log("soundShot");

            if (this.gameObject.CompareTag("ShrinkTrigger"))
            {
                Destroy(this.gameObject.transform.parent.gameObject);

            }
        }
    }
}
