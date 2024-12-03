using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // 注視するターゲット（プレイヤー）
    public float distance = 10f; // ターゲットからの距離
    public float rotationSpeed = 50f; // 回転速度

    public Camera mainCamera; // メインカメラ
    public Camera subCamera; // メインカメラ
    public Camera orbitCamera; // サブカメラ（このスクリプトがアタッチされているカメラ）

    private float currentAngle = 0f; // 現在の回転角度
    private bool hasSwitched = false; // カメラ切り替えが行われたか

    void Start()
    {
        // サブカメラを有効化し、メインカメラを無効化（初期設定）
        if (mainCamera != null)
        {
            mainCamera.enabled = false;
        }
        if (subCamera != null)
        {
            subCamera.enabled = false;
        }
        if (orbitCamera != null)
        {
            orbitCamera.enabled = true;
        }
    }

    void Update()
    {
        // 一周後、カメラ切り替え
        if (currentAngle >= 360f && !hasSwitched)
        {
            SwitchToMainCamera();
            return; // 処理終了
        }

        // 時間に応じてカメラを回転
        currentAngle += rotationSpeed * Time.deltaTime;

        // 一周（360度）を超えたら、値をリセット（過剰な増加を防ぐ）
        if (currentAngle >= 360f)
        {
            currentAngle = 360f;
        }

        float radians = currentAngle * Mathf.Deg2Rad;

        // 新しいカメラの位置を計算
        Vector3 newPosition = new Vector3(
            target.position.x + Mathf.Sin(radians) * distance,
            target.position.y,
            target.position.z + Mathf.Cos(radians) * distance
        );

        // カメラを新しい位置に移動し、ターゲットを向く
        transform.position = newPosition;
        transform.LookAt(target);
    }

    void SwitchToMainCamera()
    {
        // メインカメラを有効化し、サブカメラを無効化
        if (mainCamera != null)
        {
            mainCamera.enabled = true;
        }
        if (subCamera != null)
        {
            subCamera.enabled = true;
        }
        if (orbitCamera != null)
        {
            orbitCamera.enabled = false;
        }

        hasSwitched = true; // 切り替え済みフラグを設定
    }
}
