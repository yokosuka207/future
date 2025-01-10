using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // �ő�HP
    public int currentHealth;                    // ���݂�HP

    [SerializeField] private float speed = 1.0f;  // �ړ����x
    [SerializeField] private GameObject target;

    [SerializeField] private GameObject HPbar;

    [SerializeField] private Canvas GameOverUI;

    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioMixerGroup audioMixer;
    AudioSource audioSource;

    //public CameraShake mainCameraShake;
    //public CameraShake subCameraShake;

    private void Start()
    {
        currentHealth = maxHealth;

        //�I�[�f�B�I�\�[�X���������悤�ɂ���
        audioSource = this.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;

        //�T�E���h�𗬂�
        audioSource.PlayOneShot(sound1);
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Ռ��g�Ƃ̓����蔻��
        if (other.gameObject.tag == "ShockWave")
            //TakeDamage();

        // �S�[���Ƃ̏Փ˔���
        if(other.CompareTag("Goal"))
            speed = 0;
    }

    // �_���[�W�v�Z
    public void TakeDamage(int damage)
    {
        // �_���[�W�v�Z
        currentHealth -= damage;

        //HP�o�[�̌���
        HPbar.GetComponent<HPBarController>().HealthDecreese(damage);


        // ���C���J������h�炷
        StartCoroutine(mainCameraShake.Shake());

        // �T�u�J������h�炷
        StartCoroutine(subCameraShake.Shake());


        Debug.Log(currentHealth);

        // ���S����
        if (currentHealth <= 0)
        {
            //�Q�[���𒆒f
            Time.timeScale = 0;

            //���y���~
            audioSource.Stop();

            //�Q�[���I�[�o�[UI���N��
            GameOverUI.gameObject.SetActive(true);

            
        }
    }


}