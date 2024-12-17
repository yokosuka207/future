using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // 生成するオブジェクトの配列
    public CameraControll cameraController;              // カメラ切り替えスクリプト
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;
    public ObjectPlacementLimit[] placementLimits; // オブジェクトごとの設定
    [SerializeField] private GameObject diffenceUI;
    [SerializeField] private GameObject player;

    private Material originalMaterial;
    private GameObject previewObject;        // 仮置きオブジェクト
    private bool isPreviewing = false;       // 仮置き状態フラグ
    private Vector3 previewWorldPosition;    // 仮置きオブジェクトのワールド座標
    private int currentObjectIndex = 0;      // 現在のオブジェクトインデックス
    private Dictionary<GameObject, ObjectPlacementLimit> placementLimitsDict;   // 辞書に変換して管理
    private Dictionary<GameObject, int> objectUsageCounts = new Dictionary<GameObject, int>();

    private Vector2 leftStickInput = Vector2.zero;
    private Vector2 rightStickInput = Vector2.zero;

    [System.Serializable]
    public class ObjectPlacementLimit
    {
        public GameObject objectType;  // オブジェクトの種類
        public int maxCount;          // この種類の最大設置数
        public int currentCount;      // 現在の設置数
    }


    void Update()
    {
        

        if(player.GetComponent<SoundWave>().currentHealth > 0)
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

            // 仮置き中かつ previewObject が null でない場合のみ更新
            if (isPreviewing && previewObject != null)
            {
                UpdatePreviewPosition();
            }
        }
        
    }


    void Start()
    {
        // シーンをまたいでオブジェクトを保持する
        DontDestroyOnLoad(gameObject);

        placementLimitsDict = new Dictionary<GameObject, ObjectPlacementLimit>();

        // 初期オブジェクトを数字キー「1」に対応
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            currentObjectIndex = 0; // 配列の最初のオブジェクトを設定
        }

        // 配列から辞書に変換
        foreach (var limit in placementLimits)
        {
            if (limit != null && limit.objectType != null)
            {
                placementLimitsDict[limit.objectType] = limit;
                limit.currentCount = 0; // 初期化
                objectUsageCounts[limit.objectType] = 0; // 使用回数を初期化
            }
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
        // 現在選択中のオブジェクトを取得
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        // 設置数制限を確認
        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            if (limit.currentCount >= limit.maxCount)
            {
                isPreviewing = false; // 仮置き状態を解除
                return; // 仮置きオブジェクトを生成しない
            }
        }

        // 仮置きオブジェクトを生成
        previewObject = Instantiate(currentObject);

        // 半透明マテリアルを設定
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;

            // 半透明化するためのマテリアルを作成
            Material transparentMat = new Material(originalMaterial);
            Color color = transparentMat.color;
            color.a = 0.5f; // アルファ値を変更して半透明に
            transparentMat.color = color;
            renderer.material = transparentMat;
        }

        // 仮置き状態にするためにColliderを無効化
        Collider collider = previewObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        isPreviewing = true; // 仮置き状態を設定
    }





    void UpdatePreviewPosition()
    {
        //// 現在のアクティブなカメラを取得
        //Camera activeCamera = cameraController.ActiveCamera;

        //// マウス位置をスクリーン座標として取得
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = spawnDistance; // カメラからの距離を設定

        //// スクリーン座標をワールド座標に変換
        //previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);

        //// 仮置きオブジェクトの位置を更新
        //previewObject.transform.position = previewWorldPosition;
        //previewObject.transform.rotation = Quaternion.identity;

        // 現在のアクティブなカメラを取得
        Camera activeCamera = cameraController.ActiveCamera;

        // マウス入力が有効な場合
        if (Input.mousePresent)
        {
            // マウス位置をスクリーン座標として取得
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = spawnDistance; // カメラからの距離を設定

            // スクリーン座標をワールド座標に変換
            previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);
        }

        // コントローラーの左スティック入力が有効な場合
        if (Gamepad.current != null && leftStickInput != Vector2.zero)
        {
            // コントローラーの左スティック入力をカメラの方向に変換
            Vector3 controllerOffset = new Vector3(leftStickInput.x, leftStickInput.y, 0) * spawnDistance;
            previewWorldPosition = activeCamera.transform.position + activeCamera.transform.TransformDirection(controllerOffset);
        }

        // 仮置きオブジェクトの位置を更新
        previewObject.transform.position = previewWorldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            // 最大数を超えている場合は設置を無効化
            if (limit.currentCount >= limit.maxCount)
            {
                return;
            }

            // カウントを更新
            limit.currentCount++;

            // 使用回数を更新
            if (objectUsageCounts.ContainsKey(currentObject))
            {
                objectUsageCounts[currentObject]++;
            }
            else
            {
                objectUsageCounts[currentObject] = 1;
            }
        }

        if (!previewObject.activeSelf) return;

        // 確定配置時にColliderを有効化
        Collider collider = previewObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            //UI表示スクリプトに個数チェックを指示する
            diffenceUI.GetComponent<UIDiffenceCount>().CheckDiffenceNum(currentObjectIndex);
        }

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

    public void ResetPlacementCounts()
    {
        foreach (var limit in placementLimits)
        {
            limit.currentCount = 0;
        }
    }

    public void OnSwitchObject(InputValue value)
    {
        float direction = value.Get<float>(); // -1: 左ショルダー, 1: 右ショルダー
        currentObjectIndex = Mathf.Clamp(currentObjectIndex + (int)direction, 0, objectsToSpawn.Length - 1);

        if (isPreviewing)
        {
            Destroy(previewObject);
            ShowPreview();
        }
    }

    public void OnPlaceObject(InputValue value)
    {
        if (value.isPressed)
        {
            if (!isPreviewing)
                ShowPreview();
            else
                PlaceObject();
        }
    }

    public void OnPreviewMove(InputValue value)
    {
        leftStickInput = value.Get<Vector2>();
    }

    public void OnAdjustDistance(InputValue value)
    {
        rightStickInput = value.Get<Vector2>();
        spawnDistance += rightStickInput.y * scrollSpeed * Time.deltaTime;
        spawnDistance = Mathf.Clamp(spawnDistance, spawnDistanceMin, spawnDistanceMax);
    }
}
