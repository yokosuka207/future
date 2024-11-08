using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 diff;       //�J�����ƃv���C���[�̋���
    [SerializeField] private GameObject target;  //�Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    public float followSpeed;   //�Ǐ]����X�s�[�h

    void Start()
    {
        diff = target.transform.position - this.transform.position;     //�J�����ƃv���C���[�̏����̋������w��
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, target.transform.position - diff, Time.deltaTime * followSpeed);     //���`��Ԋ֐��ɂ��J�����̈ړ�
    }
}
