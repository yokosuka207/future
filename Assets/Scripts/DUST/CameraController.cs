using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // X������̃J������]���x.
    [SerializeField] float xRotationSpeed = 5f;
    // Y������̃J������]���x.
    [SerializeField] float yRotationSpeed = 5f;
    // �}�E�X�N���b�N���J�n�����ʒu.
    Vector3 startMousePosition = Vector3.zero;
    // �N���b�N�J�n���_�ł̃J�����̊p�x.
    Vector3 startCameraRotation = Vector3.zero;
    // ���W�b�h�{�f�B.
    Rigidbody rigid = null;

    void Start()
    {
        // ���W�b�h�{�f�B�̎擾.
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �N���b�N�̊J�n.
        if (Input.GetMouseButtonDown(0) == true)
        {
            // �}�E�X�̈ʒu�ƃJ�����̊p�x��ۊ�.
            startMousePosition = Input.mousePosition;
            startCameraRotation = Camera.main.gameObject.transform.localRotation.eulerAngles;
        }

        // �N���b�N���i�h���b�O�j.
        if (Input.GetMouseButton(0) == true)
        {
            // �����_�̃}�E�X�ʒu���擾.
            var currentMousePosition = Input.mousePosition;
            // �N���b�N�J�n�ʒu����̍������Z�o.
            var def = (currentMousePosition - startMousePosition);
            // ���݂̃J�����p�x.
            var currentCameraRotation = Camera.main.transform.localRotation.eulerAngles;
            // ��]�p�x���Z�o.
            currentCameraRotation.x = startCameraRotation.x + (def.y * xRotationSpeed * 0.01f);
            currentCameraRotation.y = startCameraRotation.y + (-def.x * yRotationSpeed * 0.01f);
            // �J�����ɓK�p.
            Camera.main.transform.localRotation = Quaternion.Euler(currentCameraRotation);
        }

        // �N���b�N�I��.
        if (Input.GetMouseButtonUp(0) == true)
        {
            // �ۊǂ����l�����Z�b�g.
            startMousePosition = Vector3.zero;
            startCameraRotation = Vector3.zero;
        }
    }
}
