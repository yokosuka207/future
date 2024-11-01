using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // �ő�HP
    public int currentHealth;  // ���݂�HP

    void Start()
    {
        // HP���ő�HP�ŏ�����
        currentHealth = maxHealth;
    }

    // �_���[�W���󂯂鏈��
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("�_���[�W���󂯂܂���: " + damage + " ���݂�HP: " + currentHealth);

        // HP��0�ȉ��ɂȂ����玀�S����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ���S����
    private void Die()
    {
        Debug.Log("�L�����N�^�[�����S���܂���");
        // �K�v�ɉ����ăI�u�W�F�N�g��j��
        Destroy(gameObject);

        // �܂��́A�Q�[���I�[�o�[��ʂ�\������Ȃǂ̏�����ǉ��\
    }
 
}
