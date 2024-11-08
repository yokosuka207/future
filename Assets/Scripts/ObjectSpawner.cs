using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;          // 生成するオブジェクトのPrefab
    public Camera mainCamera;                 // カメラを設定
    public float spawnDistance = 5.0f;        // カメラからの初期生成距離
    public float scrollSpeed = 2.0f;          // ホイールスクロールの感度
    public float spawnDistanceMin = 1.0f;     // 距離の最小値（インスペクターで設定可能）
    public float spawnDistanceMax = 20.0f;    // 距離の最大値（インスペクターで設定可能）
    public Material transparentMaterial;      // 半透明のマテリアル
    private Material originalMaterial;        // 元のマテリアルを保存
    private GameObject previewObject;         // 仮の半透明オブジェクト
    private bool isPreviewing = false;        // 仮のオブジェクトが表示中かどうか

    void Update()
    {
        HandleMouseScroll(); // マウスホイールで距離調整

        if (Input.GetMouseButtonDown(0)) // 左クリックを確認
        {
            if (!isPreviewing)
            {
                // 一度目のクリックで仮の半透明オブジェクトを生成
                ShowPreview();
            }
            else
            {
                // 二度目のクリックでオブジェクトを確定
                PlaceObject();
            }
        }

        if (isPreviewing && previewObject != null)
        {
            // 仮のオブジェクトをマウス位置に追従
            UpdatePreviewPosition();
        }
    }

    void HandleMouseScroll()
    {
        // マウスホイールのスクロール量を取得
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // スクロール入力に応じてspawnDistanceを調整
        spawnDistance += scrollInput * scrollSpeed;

        // インスペクターで設定した距離の最小・最大値に制限
        spawnDistance = Mathf.Clamp(spawnDistance, spawnDistanceMin, spawnDistanceMax);
    }

    void ShowPreview()
    {
        // 仮の半透明オブジェクトを生成
        previewObject = Instantiate(objectToSpawn);

        // 元のマテリアルを保存し、半透明マテリアルを適用
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;  // 元のマテリアルを保存
            renderer.material = transparentMaterial; // 半透明マテリアルを適用
        }

        isPreviewing = true;
    }

    void UpdatePreviewPosition()
    {
        // カメラから一定の距離にあるマウス位置のワールド座標を取得
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = spawnDistance;  // カメラからの距離を設定

        // カメラのビューポート内にマウス位置があるか確認
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(mousePosition);
        if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
        {
            previewObject.SetActive(false); // ビューポート外なら非表示にする
            return;
        }

        previewObject.SetActive(true); // ビューポート内なら表示する

        // マウス位置に基づき、ワールド座標で仮のオブジェクトの位置を設定
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        previewObject.transform.position = worldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        // ビューポート外でクリックした場合は配置しない
        if (!previewObject.activeSelf) return;

        // 確定するために元のマテリアルを戻し、状態をリセット
        if (previewObject != null)
        {
            Renderer renderer = previewObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = originalMaterial;  // 元のマテリアルに戻す
            }
        }
        previewObject = null;
        isPreviewing = false;
    }
}
