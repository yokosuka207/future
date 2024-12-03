using UnityEngine;

public class Player : MonoBehaviour
{
    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShockWave")) // 敵と衝突した場合
        {
            // メインカメラを揺らす
            StartCoroutine(mainCameraShake.Shake());

            // サブカメラを揺らす
            StartCoroutine(subCameraShake.Shake());
        }
    }
}
