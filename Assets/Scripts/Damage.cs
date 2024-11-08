using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10; // ダメージ量

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たったらダメージを与える
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
