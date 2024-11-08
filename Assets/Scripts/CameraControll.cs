using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    [SerializeField] private GameObject mainCamera;      //メインカメラ格納用
    [SerializeField] private GameObject subCamera;       //サブカメラ格納用 


    //呼び出し時に実行される関数
    void Start()
    {
        //サブカメラを非アクティブにする
        subCamera.SetActive(false);
    }


    //単位時間ごとに実行される関数
    void Update()
    {
        //スペースキーが押されたら、サブカメラをアクティブにする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 各カメラオブジェクトの有効フラグを逆転(true→false,false→true)させる
            mainCamera.SetActive(!mainCamera.activeSelf);
            subCamera.SetActive(!subCamera.activeSelf);
        }
    }
}