using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // 最大HP
    private int currentHealth;                    // 現在のHP

    [SerializeField] private float speed = 1.0f;  // 移動速度
    [SerializeField] private GameObject target;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝撃波との当たり判定
        if (other.gameObject.tag == "ShockWave")
            TakeDamage();

        // ゴールとの衝突判定
        if(other.CompareTag("Goal"))
            speed = 0;
    }

    // ダメージ計算
    public void TakeDamage()
    {
        // 衝撃波のダメージ引用
        ShockwaveCollider ss;
        GameObject obj = GameObject.FindGameObjectWithTag("ShockWave");
        ss = obj.GetComponent<ShockwaveCollider>();

        // ダメージ計算
        currentHealth -= ss.Damage();

        Debug.Log(currentHealth);

        // 死亡処理
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}