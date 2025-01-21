using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float duration = 0.5f;  // 揺れの時間
    public float magnitude = 0.2f; // 揺れの強さ

    private Transform[] cameraChildren; // カメラの子オブジェクトリスト

    private void Start()
    {
        // 全ての子オブジェクトを取得
        int childCount = transform.childCount;
        if (childCount > 0)
        {
            cameraChildren = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                cameraChildren[i] = transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("CameraShake: カメラの子オブジェクトが見つかりません！");
        }
    }

    public IEnumerator Shake()
    {
        if (gameObject.GetComponent<SoundWave>().currentHealth <= 0)
        {
            duration = 0;
            yield break;
        }

        if (cameraChildren == null || cameraChildren.Length == 0) yield break;

        // 子オブジェクトの元のローカル位置を記憶
        Vector3[] originalLocalPositions = new Vector3[cameraChildren.Length];
        for (int i = 0; i < cameraChildren.Length; i++)
        {
            originalLocalPositions[i] = cameraChildren[i].localPosition;
        }

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            foreach (var cameraChild in cameraChildren)
            {
                float offsetX = Random.Range(-1f, 1f) * magnitude;
                float offsetY = Random.Range(-1f, 1f) * magnitude;
                float offsetZ = Random.Range(-1f, 1f) * magnitude;

                cameraChild.localPosition += new Vector3(offsetX, offsetY, offsetZ);
            }

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 元のローカル位置に戻す
        for (int i = 0; i < cameraChildren.Length; i++)
        {
            cameraChildren[i].localPosition = originalLocalPositions[i];
        }
    }

    public IEnumerator Shake(float customDuration, float customMagnitude)
    {
        if (gameObject.GetComponent<SoundWave>().currentHealth <= 0)
        {
            customDuration = 0;
            yield break;
        }

        if (cameraChildren == null || cameraChildren.Length == 0) yield break;

        // 子オブジェクトの元のローカル位置を記憶
        Vector3[] originalLocalPositions = new Vector3[cameraChildren.Length];
        for (int i = 0; i < cameraChildren.Length; i++)
        {
            originalLocalPositions[i] = cameraChildren[i].localPosition;
        }

        float elapsed = 0.0f;

        while (elapsed < customDuration)
        {
            foreach (var cameraChild in cameraChildren)
            {
                float offsetX = Random.Range(-1f, 1f) * customMagnitude;
                float offsetY = Random.Range(-1f, 1f) * customMagnitude;
                float offsetZ = Random.Range(-1f, 1f) * customMagnitude;

                cameraChild.localPosition += new Vector3(offsetX, offsetY, offsetZ);
            }

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 元のローカル位置に戻す
        for (int i = 0; i < cameraChildren.Length; i++)
        {
            cameraChildren[i].localPosition = originalLocalPositions[i];
        }
    }

}
