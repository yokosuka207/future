using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // 生成するオブジェクトの配列
    public CameraControll cameraController;              // カメラ切り替えスクリプト
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;

    private Material originalMaterial;
    private GameObject previewObject;        // 仮置きオブジェクト
    private bool isPreviewing = false;       // 仮置き状態フラグ
    private Vector3 previewWorldPosition;    // 仮置きオブジェクトのワールド座標
    private int currentObjectIndex = 0;      // 現在のオブジェクトインデックス

    void Update()
    {
        HandleObjectSwitch(); // テンキー入力でオブジェクトを切り替え
        HandleMouseScroll();

        if (Input.GetMouseButtonDown(0))
        {
            if (!isPreviewing)
            {
                ShowPreview();
            }
            else
            {
                PlaceObject();
            }
        }

        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }

    void Start()
    {
        // 初期オブジェクトを数字キー「1」に対応
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            currentObjectIndex = 0; // 配列の最初のオブジェクトを設定
        }
    }

    void HandleObjectSwitch()
    {
        // 配列が空の場合は処理しない
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            return;
        }

        // 数字キーで切り替え
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            // 数字キー1がインデックス0に対応
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentObjectIndex = i;

                // 仮置きオブジェクトが存在している場合は再生成
                if (isPreviewing)
                {
                    Destroy(previewObject);
                    ShowPreview();
                }
            }
        }
    }



    void HandleMouseScroll()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        spawnDistance += scrollInput * scrollSpeed;
        spawnDistance = Mathf.Clamp(spawnDistance, spawnDistanceMin, spawnDistanceMax);
    }

    void ShowPreview()
    {
        // 現在のオブジェクトを仮置きとして生成
        previewObject = Instantiate(objectsToSpawn[currentObjectIndex]);

        // プレハブの元のマテリアルを取得して色を変更
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material; // 元のマテリアルを保存

            // 新しいマテリアルを作成して元の色を半透明に変更
            Material previewMaterial = new Material(originalMaterial);
            Color originalColor = originalMaterial.color; // 元の色を取得
            originalColor.a = 0.5f; // 半透明に設定
            previewMaterial.color = originalColor;

            renderer.material = previewMaterial; // 仮置きオブジェクトに設定
        }

        isPreviewing = true;
    }


    void UpdatePreviewPosition()
    {
        // 現在のアクティブなカメラを取得
        Camera activeCamera = cameraController.ActiveCamera;

        // マウス位置をスクリーン座標として取得
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = spawnDistance; // カメラからの距離を設定

        // スクリーン座標をワールド座標に変換
        previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);

        // 仮置きオブジェクトの位置を更新
        previewObject.transform.position = previewWorldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        if (!previewObject.activeSelf) return;

        // 確定配置時にマテリアルを元に戻す
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }

        previewObject = null;
        isPreviewing = false;
    }

    public void RefreshPreviewObject()
    {
        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }
}
