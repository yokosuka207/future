using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    [SerializeField] private int damage; // ダメージ量
    [SerializeField] private float growthRate = 1.0f; // 拡大速度
    [SerializeField] private float maxScale = 5.0f; // 最大スケール

    private SphereCollider sphereCollider;
    // ダメージ量を返すプロパティ
    public int Damage()
    {
        return damage;
    }

    private void Start()
    {
        maxScale = this.transform.parent.GetComponent<ShockwaveSpawnerDemo>().duration;
        // SphereColliderを取得
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("ShockwaveCollider requires a SphereCollider.");
        }
    }
    private void Update()
    {
        // Shockwaveのスケールを徐々に拡大
        if (transform.localScale.x < maxScale)
        {
            float scaleIncrement = growthRate * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);

            // コライダーの半径をスケールに同期
            if (sphereCollider != null)
            {
                sphereCollider.radius = transform.localScale.x / 2.0f;
            }
        }
    }

    // トリガーコライダーで他のオブジェクトと衝突したときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        // 衝突した相手が"Damageable"タグを持っているか確認
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
