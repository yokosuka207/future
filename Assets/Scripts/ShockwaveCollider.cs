using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    public int damage = 10; // �Ռ��g���^����_���[�W��

    // �Ռ��g�����̃I�u�W�F�N�g�ɐG�ꂽ�Ƃ��ɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        // ���������I�u�W�F�N�g���uEnemy�v�^�O�����ꍇ�Ƀ_���[�W��^����
        if (other.CompareTag("Enemy"))
        {
            //Enemy enemy = other.GetComponent<Enemy>();
            //if (enemy != null)
            //{
              //  enemy.TakeDamage(damage); // �G�Ƀ_���[�W��^����
            //}
        }
    }
}
