using UnityEngine;

public class Player : MonoBehaviour
{
    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ShockWave")) // �G�ƏՓ˂����ꍇ
        {

            Debug.Log("�J�����V�F�C�N");
            // ���C���J������h�炷
            StartCoroutine(mainCameraShake.Shake());

            // �T�u�J������h�炷
            StartCoroutine(subCameraShake.Shake());
        }
    }
}
