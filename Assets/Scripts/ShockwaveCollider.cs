using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    [SerializeField] private int damage; // �_���[�W��
    [SerializeField] private float growthRate = 1.0f; // �g�呬�x
    [SerializeField] private float maxScale = 5.0f; // �ő�X�P�[��

    private SphereCollider sphereCollider;
    // �_���[�W�ʂ�Ԃ��v���p�e�B
    public int Damage()
    {
        return damage;
    }

    private void Start()
    {
        maxScale = this.transform.parent.GetComponent<ShockwaveSpawnerDemo>().duration;
        // SphereCollider���擾
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("ShockwaveCollider requires a SphereCollider.");
        }
    }
    private void Update()
    {
        // Shockwave�̃X�P�[�������X�Ɋg��
        if (transform.localScale.x < maxScale)
        {
            float scaleIncrement = growthRate * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);

            // �R���C�_�[�̔��a���X�P�[���ɓ���
            if (sphereCollider != null)
            {
                sphereCollider.radius = transform.localScale.x / 2.0f;
            }
        }
    }

    // �g���K�[�R���C�_�[�ő��̃I�u�W�F�N�g�ƏՓ˂����Ƃ��ɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        // �Փ˂������肪"Damageable"�^�O�������Ă��邩�m�F
        if (other.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<ShockwaveSpawnerDemo>().SoundBlock();
        }

        if (other.CompareTag("Diffence"))
        {
            this.transform.parent.GetComponent<ShockwaveSpawnerDemo>().SoundBlock();
        }
    }
}
