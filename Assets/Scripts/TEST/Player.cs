using UnityEngine;

public class Player : MonoBehaviour
{
    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ShockWave")) // 敵と衝突した場合
        {

            Debug.Log("カメラシェイク");
            // メインカメラを揺らす
            StartCoroutine(mainCameraShake.Shake());

            // サブカメラを揺らす
            StartCoroutine(subCameraShake.Shake());
        }
    }
}
