using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // 最大HP
    public int currentHealth;  // 現在のHP

    void Start()
    {
        // HPを最大HPで初期化
        currentHealth = maxHealth;
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("ダメージを受けました: " + damage + " 現在のHP: " + currentHealth);

        // HPが0以下になったら死亡処理
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡処理
    private void Die()
    {
        Debug.Log("キャラクターが死亡しました");
        // 必要に応じてオブジェクトを破壊
        Destroy(gameObject);

        // または、ゲームオーバー画面を表示するなどの処理を追加可能
    }
 
}
