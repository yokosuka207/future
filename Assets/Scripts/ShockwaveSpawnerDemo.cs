using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ShockwaveSpawnerDemo : MonoBehaviour
{
    public GameObject shockwavePrefab; // �Ռ��g�̃v���n�u
    public float duration; // �e�Ռ��g�̎������ԃ��X�g
    public float startTime; // �e�Ռ��g�̔����J�n���ԃ��X�g

    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioMixerGroup audioMixer;
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;
        StartCoroutine(SpawnShockwaveWithDelay(this.transform.position, duration, startTime));
        
        //audioSource.spatialize = true;
        //audioSource.spatializePostEffects = true;
        //audioSource.bypassReverbZones = true;

    }

    private IEnumerator SpawnShockwaveWithDelay(Vector3 position, float duration, float delay)
    {
        // �J�n���Ԃ܂őҋ@
        yield return new WaitForSeconds(delay);

        // �Ռ��g�̃C���X�^���X�𐶐�
        GameObject shockwave = Instantiate(shockwavePrefab, position, Quaternion.identity);
        shockwave.transform.parent = this.transform;

        //���[�_�[�Ɉڂ��p
        GameObject.FindWithTag("Radar").GetComponent<RadarController>().SpownRadarShock(shockwave);

        //�T�E���h�𗬂�
        audioSource.PlayOneShot(sound1);

        //Invoke(nameof(SoundOff), duration);
        
    }

    public void SoundOff()
    {
        audioSource.Stop();
        // �������Ԍ�ɍ폜
        Destroy(this.gameObject);
    }

    public void SoundBlock()
    {
        audioSource.Stop();
        // �������Ԍ�ɍ폜
        Destroy(this.gameObject);
    }
}
