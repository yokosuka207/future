using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;  // 追従するターゲットオブジェクト
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // 初期オフセット値
    [SerializeField] private float followSpeed = 5f; // 追従速度

    void Start()
    {
        // カメラを初期オフセット位置に配置
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
            // ターゲットの位置にオフセットを加えた位置へカメラを移動
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
    }

    private void Reset()
    {
        // ターゲットが設定されている場合、初期オフセットを計算
        if (target != null)
        {
            offset = transform.position - target.transform.position;
        }
    }
}
