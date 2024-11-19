using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // �ő�HP
    private int currentHealth;                    // ���݂�HP

    [SerializeField] private float speed = 1.0f;  // �ړ����x
    private Vector3 direction = new Vector3(10f, 0f, 0f);

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Ռ��g�Ƃ̓����蔻��
        if (other.gameObject.tag == "ShockWave")
            TakeDamage();

        // �S�[���Ƃ̏Փ˔���
        if(other.CompareTag("Goal"))
            speed = 0;
    }

    // �_���[�W�v�Z
    public void TakeDamage()
    {
        // �Ռ��g�̃_���[�W���p
        ShockwaveCollider ss;
        GameObject obj = GameObject.FindGameObjectWithTag("ShockWave");
        ss = obj.GetComponent<ShockwaveCollider>();

        // �_���[�W�v�Z
        currentHealth -= ss.Damege();

        Debug.Log(currentHealth);

        // ���S����
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}