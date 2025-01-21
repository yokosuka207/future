using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float duration = 0.5f;  // �h��̎���
    public float magnitude = 0.2f; // �h��̋���

    private Transform[] cameraChildren; // �J�����̎q�I�u�W�F�N�g���X�g

    private void Start()
    {
        // �S�Ă̎q�I�u�W�F�N�g���擾
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
            Debug.LogError("CameraShake: �J�����̎q�I�u�W�F�N�g��������܂���I");
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

        // �q�I�u�W�F�N�g�̌��̃��[�J���ʒu���L��
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

        // ���̃��[�J���ʒu�ɖ߂�
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

        // �q�I�u�W�F�N�g�̌��̃��[�J���ʒu���L��
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

        // ���̃��[�J���ʒu�ɖ߂�
        for (int i = 0; i < cameraChildren.Length; i++)
        {
            cameraChildren[i].localPosition = originalLocalPositions[i];
        }
    }

}
