using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // X軸周りのカメラ回転速度.
    [SerializeField] float xRotationSpeed = 5f;
    // Y軸周りのカメラ回転速度.
    [SerializeField] float yRotationSpeed = 5f;
    // マウスクリックを開始した位置.
    Vector3 startMousePosition = Vector3.zero;
    // クリック開始時点でのカメラの角度.
    Vector3 startCameraRotation = Vector3.zero;
    // リジッドボディ.
    Rigidbody rigid = null;

    void Start()
    {
        // リジッドボディの取得.
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // クリックの開始.
        if (Input.GetMouseButtonDown(0) == true)
        {
            // マウスの位置とカメラの角度を保管.
            startMousePosition = Input.mousePosition;
            startCameraRotation = Camera.main.gameObject.transform.localRotation.eulerAngles;
        }

        // クリック中（ドラッグ）.
        if (Input.GetMouseButton(0) == true)
        {
            // 現時点のマウス位置を取得.
            var currentMousePosition = Input.mousePosition;
            // クリック開始位置からの差分を算出.
            var def = (currentMousePosition - startMousePosition);
            // 現在のカメラ角度.
            var currentCameraRotation = Camera.main.transform.localRotation.eulerAngles;
            // 回転角度を算出.
            currentCameraRotation.x = startCameraRotation.x + (def.y * xRotationSpeed * 0.01f);
            currentCameraRotation.y = startCameraRotation.y + (-def.x * yRotationSpeed * 0.01f);
            // カメラに適用.
            Camera.main.transform.localRotation = Quaternion.Euler(currentCameraRotation);
        }

        // クリック終了.
        if (Input.GetMouseButtonUp(0) == true)
        {
            // 保管した値をリセット.
            startMousePosition = Vector3.zero;
            startCameraRotation = Vector3.zero;
        }
    }
}
