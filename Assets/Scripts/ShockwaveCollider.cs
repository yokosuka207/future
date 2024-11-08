using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    public int damage = 10; // 衝撃波が与えるダメージ量

    // 衝撃波が他のオブジェクトに触れたときに呼び出される
    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトが「Enemy」タグを持つ場合にダメージを与える
        if (other.CompareTag("Enemy"))
        {
            //Enemy enemy = other.GetComponent<Enemy>();
            //if (enemy != null)
            //{
              //  enemy.TakeDamage(damage); // 敵にダメージを与える
            //}
        }
    }
}
