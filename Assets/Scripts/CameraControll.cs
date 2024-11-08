using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    [SerializeField] private GameObject mainCamera;      //���C���J�����i�[�p
    [SerializeField] private GameObject subCamera;       //�T�u�J�����i�[�p 


    //�Ăяo�����Ɏ��s�����֐�
    void Start()
    {
        //�T�u�J�������A�N�e�B�u�ɂ���
        subCamera.SetActive(false);
    }


    //�P�ʎ��Ԃ��ƂɎ��s�����֐�
    void Update()
    {
        //�X�y�[�X�L�[�������ꂽ��A�T�u�J�������A�N�e�B�u�ɂ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �e�J�����I�u�W�F�N�g�̗L���t���O���t�](true��false,false��true)������
            mainCamera.SetActive(!mainCamera.activeSelf);
            subCamera.SetActive(!subCamera.activeSelf);
        }
    }
}