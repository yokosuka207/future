using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;      // 追従するターゲットオブジェクト
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // カメラの初期オフセット
    [SerializeField] private float followSpeed = 5f; // 追従速度
    [SerializeField] private Vector3 rotationAngles = new Vector3(30, 0, 0); // カメラの回転角度

    void Start()
    {
        if (target != null)
        {
            // 初期位置を設定
            transform.position = target.transform.position + offset;

            // 初期回転を設定
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
            // ターゲットに基づいてカメラ位置を計算
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            // カメラ回転を線形補間で変更
            Quaternion targetRotation = Quaternion.Euler(rotationAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
        }
    }

    private void Reset()
    {
        if (target != null)
        {
            // 初期オフセットと回転角度をターゲットに基づいて設定
            offset = transform.position - target.transform.position;
            rotationAngles = transform.rotation.eulerAngles;
        }
    }
}
