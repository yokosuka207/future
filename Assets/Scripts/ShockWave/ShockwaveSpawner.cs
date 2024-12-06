using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public GameObject shockwavePrefab; // 衝撃波のプレハブ
    public List<Vector3> spawnPositions; // 各衝撃波の生成位置リスト
    public List<float> durations; // 各衝撃波の持続時間リスト
    public List<float> startTimes; // 各衝撃波の発生開始時間リスト

    void Start()
    {
        // 各位置に対してコルーチンを開始し、発生開始時間を適用
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            float startTime = (i < startTimes.Count) ? startTimes[i] : 0f; // 開始時間がリストにない場合は0秒
            float duration = (i < durations.Count) ? durations[i] : 5.0f; // 持続時間がリストにない場合は5秒
            Vector3 position = spawnPositions[i];

            StartCoroutine(SpawnShockwaveWithDelay(position, duration, startTime));
        }
    }

    private IEnumerator SpawnShockwaveWithDelay(Vector3 position, float duration, float delay)
    {
        // 開始時間まで待機
        yield return new WaitForSeconds(delay);

        // 衝撃波のインスタンスを生成
        GameObject shockwave = Instantiate(shockwavePrefab, position, Quaternion.identity);

        // 持続時間後に削除
        Destroy(shockwave, duration);
    }
}
