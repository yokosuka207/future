using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;  // �Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // �����I�t�Z�b�g�l
    [SerializeField] private float followSpeed = 5f; // �Ǐ]���x

    void Start()
    {
        // �J�����������I�t�Z�b�g�ʒu�ɔz�u
        if (target != null)
        {
            transform.position = target.transform.position + offset;
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
            // �^�[�Q�b�g�̈ʒu�ɃI�t�Z�b�g���������ʒu�փJ�������ړ�
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
    }

    private void Reset()
    {
        // �^�[�Q�b�g���ݒ肳��Ă���ꍇ�A�����I�t�Z�b�g���v�Z
        if (target != null)
        {
            offset = transform.position - target.transform.position;
        }
    }
}
