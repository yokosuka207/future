using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public float startDelay = 3.0f;           // 開始までの遅延時間（秒）
    public float initialRadius = 1.0f;        // 初期の半径
    public float maxRadius = 5.0f;            // 最大の半径
    public float expansionSpeed = 1.0f;       // 半径の拡大速度
    public int durability = 100;              // 耐久値
    public int decayRate = 1;                 // 時間経過による減少値

    private SphereCollider outerCollider;     // 外周部分のコライダー
    private float currentRadius;              // 現在の半径
    private float timeElapsed;                // 経過時間
    private bool isExpanding = false;         // 拡大開始フラグ

    private void Start()
    {
        // 初期化
        currentRadius = initialRadius;
        timeElapsed = 0f;

        // コライダーを設定
        outerCollider = gameObject.AddComponent<SphereCollider>();
        outerCollider.isTrigger = true;
        outerCollider.radius = initialRadius;

        // 見た目のスケールを初期半径に合わせる
        transform.localScale = Vector3.one * initialRadius * 2;

        // 拡大開始の遅延タイマーを設定
        Invoke(nameof(StartExpansion), startDelay);
    }

    private void StartExpansion()
    {
        isExpanding = true; // 拡大開始フラグを立てる
    }

    private void Update()
    {
        // 拡大が開始されていない場合は処理を停止
        if (!isExpanding)
            return;
        // 時間経過で耐久値を減少
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1.0f)
        {
            durability -= decayRate;
            timeElapsed = 0f;
        }

        // 球の拡大
        if (currentRadius < maxRadius)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            outerCollider.radius = currentRadius;

            // オブジェクトのスケールを更新
            transform.localScale = Vector3.one * currentRadius * 2;
        }

        // 耐久値がゼロになったら消滅
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)//接触時の処理
    {

    }
}
