using UnityEngine;

public class Player : MonoBehaviour
{
    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShockWave")) // �G�ƏՓ˂����ꍇ
        {
            // ���C���J������h�炷
            StartCoroutine(mainCameraShake.Shake());

            // �T�u�J������h�炷
            StartCoroutine(subCameraShake.Shake());
        }
    }
}
