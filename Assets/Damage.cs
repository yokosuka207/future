using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10; // �_���[�W��

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɓ���������_���[�W��^����
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
