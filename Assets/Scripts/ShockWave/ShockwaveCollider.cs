using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    private float growthRate;
    private float maxGrowthRate;
    private float growthAcceleration;
    private float growthDeceleration;
    private float maxScale;

    [SerializeField] private int damage = 10; // ダメージ量
    [SerializeField] private float minGrowthRate = 0.0001f; // 最小拡大速度
    [SerializeField] private float shrinkRate = 100.0f; // 縮小速度
    [SerializeField] private float minScale = 1.0f; // 最小スケール
    [SerializeField] private float accelerationBoost = 100.0f; // 加速処理で一時的に増加する拡大速度

    private SphereCollider sphereCollider;
    private float currentGrowthRate; // 現在の拡大速度
    private bool isShrinking = false; // 縮小中かどうかのフラグ

    // ダメージ量を返すプロパティ
    public int Damage()
    {
        return damage;
    }

    private void SyncColliderToScale()
    {
        if (sphereCollider != null)
        {
            sphereCollider.radius = Mathf.Round(transform.localScale.x / 1.0f * 1000f) / 1000f; // 小数点以下3桁で丸める
        }
    }

    private void Start()
    {
        // SphereColliderを取得
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("ShockwaveCollider requires a SphereCollider.");
        }
    }

    private void Update()
    {
        if (isShrinking)
        {
            Shrink();
        }
        else
        {
            Expand();
        }
    }

    private void Expand()
    {
        // 拡大速度を加速または減速（上限と下限の間で制御）
        if (currentGrowthRate < maxGrowthRate)
        {
            currentGrowthRate += growthAcceleration * Time.deltaTime;
        }
        else if (currentGrowthRate > 0)
        {
            currentGrowthRate -= growthDeceleration * Time.deltaTime;
        }

        // Shockwaveのスケールを徐々に拡大
        if (transform.localScale.x < maxScale)
        {
            float scaleIncrement = currentGrowthRate * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
        }
        else
        {
            isShrinking = true;
        }
    }

    internal void Initialize(ShockwaveSettings settings)
    {
        this.growthRate = settings.growthRate;
        this.maxGrowthRate = settings.maxGrowthRate;
        this.growthAcceleration = settings.growthAcceleration;
        this.growthDeceleration = settings.growthDeceleration;
        this.maxScale = settings.maxScale;

        this.currentGrowthRate = this.growthRate;
    }

    private void Shrink()
    {
        if (transform.localScale.x > minScale)
        {
            float scaleDecrement = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(scaleDecrement, scaleDecrement, scaleDecrement);

            SyncColliderToScale(); // スケールとコライダーを同期
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"タグのオブジェクトに衝突した場合
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shockwave hit the Player!");
            
        }

        // "Diffence"タグのオブジェクトに衝突した場合
        if (other.CompareTag("Diffence"))
        {
            Debug.Log("Shockwave hit a Diffence and will be destroyed.");
            Destroy(gameObject);
        }

        // "Acceleration"タグのオブジェクトに衝突した場合にスケール加速処理
        if (other.CompareTag("Acceleration"))
        {
            AccelerateScale();
        }

        // "Deceleration"タグのオブジェクトに衝突した場合にスケール減速処理
        if (other.CompareTag("Deceleration"))
        {
            DecelerateScale();
        }

        // "ShrinkTrigger"タグのオブジェクトに衝突した場合に縮小開始
        if (other.CompareTag("ShrinkTrigger"))
        {
            Debug.Log("Shockwave collided with a ShrinkTrigger and will start shrinking.");
            isShrinking = true; // 縮小モードを有効にする
        }
    }

    // スケール加速処理
    private void AccelerateScale()
    {
        currentGrowthRate += accelerationBoost;

        // 上限を超えないように調整
        if (currentGrowthRate > maxGrowthRate)
        {
            currentGrowthRate = maxGrowthRate;
        }

        Debug.Log($"Scale growth rate temporarily boosted: {currentGrowthRate}");
    }

    // スケール減速処理
    private void DecelerateScale()
    {
        currentGrowthRate -= accelerationBoost;

        // 下限を下回らないように調整
        if (currentGrowthRate < minGrowthRate)
        {
            currentGrowthRate = minGrowthRate;
        }

        Debug.Log($"Scale growth rate temporarily reduced: {currentGrowthRate}");
    }

}