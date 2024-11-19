using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;      // �Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // �J�����̏����I�t�Z�b�g
    [SerializeField] private float followSpeed = 5f; // �Ǐ]���x
    [SerializeField] private Vector3 rotationAngles = new Vector3(30, 0, 0); // �J�����̉�]�p�x

    void Start()
    {
        if (target != null)
        {
            // �����ʒu��ݒ�
            transform.position = target.transform.position + offset;

            // ������]��ݒ�
            transform.rotation = Quaternion.Euler(rotationAngles);
        }
        else
        {
            Debug.LogWarning("Target is not assigned in CameraFollow script.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // �^�[�Q�b�g�Ɋ�Â��ăJ�����ʒu���v�Z
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            // �J������]����`��ԂŕύX
            Quaternion targetRotation = Quaternion.Euler(rotationAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
        }
    }

    private void Reset()
    {
        if (target != null)
        {
            // �����I�t�Z�b�g�Ɖ�]�p�x���^�[�Q�b�g�Ɋ�Â��Đݒ�
            offset = transform.position - target.transform.position;
            rotationAngles = transform.rotation.eulerAngles;
        }
    }
}
