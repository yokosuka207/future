using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShockwaveSettings
{
    public Vector3 position;
    public float duration = 5.0f;
    public float startTime = 1.0f;
    public float growthRate = 0.1f;             // 初期拡大速度
    public float maxGrowthRate = 15.0f;         // 拡大速度の上限
    public float growthAcceleration = 0.1f;     // 拡大加速度
    public float growthDeceleration = 0.001f;   // 拡大減速度
    public float maxScale = 5.0f;               // 最大スケール
}

public class ShockwaveSpawner : MonoBehaviour
{ 
    public GameObject shockwavePrefab; // 衝撃波のプレハブ
    public List<ShockwaveSettings> shockwaveSettings; // 衝撃波ごとの設定リスト

    void Start()
    {
        foreach (var settings in shockwaveSettings)
        {
            StartCoroutine(SpawnShockwaveWithDelay(settings));
        }
    }
    private IEnumerator SpawnShockwaveWithDelay(ShockwaveSettings settings)
    {
        yield return new WaitForSeconds(settings.startTime);

        GameObject shockwave = Instantiate(shockwavePrefab, settings.position, Quaternion.identity);

        //レーダーに映す
        GameObject.FindWithTag("Radar").GetComponent<RadarController>().SpownRadarShock(shockwave);

        ShockwaveCollider collider = shockwave.GetComponent<ShockwaveCollider>();
        if (collider != null)
        {
            collider.Initialize(settings);
        }

        Destroy(shockwave, settings.duration);
    }
}
