using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlockSound : MonoBehaviour
{
    [SerializeField] private AudioClip blockSound;
    private AudioSource audioSource1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShockWave"))
        {
            audioSource1.PlayOneShot(blockSound);
        }
    }

}
