using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // �ő�HP
    public int currentHealth;                    // ���݂�HP

    [SerializeField] private float speed = 1.0f;  // �ړ����x
    [SerializeField] private GameObject target;

    [SerializeField] private Slider HPbar;

    [SerializeField] private Canvas GameOverUI;

    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    //public CameraShake mainCameraShake;
    //public CameraShake subCameraShake;

    private void Start()
    {
        currentHealth = maxHealth;
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

            //�Q�[���I�[�o�[UI���N��
            GameOverUI.gameObject.SetActive(true);

            
        }
    }


}