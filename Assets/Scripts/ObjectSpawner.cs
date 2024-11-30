using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // 生成するオブジェクトの配列
    public CameraControll cameraController;              // カメラ切り替えスクリプト
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;
    public ObjectPlacementLimit[] placementLimits; // オブジェクトごとの設定
    public GameObject diffenceUI;

    private Material originalMaterial;
    private GameObject previewObject;        // 仮置きオブジェクト
    private bool isPreviewing = false;       // 仮置き状態フラグ
    private Vector3 previewWorldPosition;    // 仮置きオブジェクトのワールド座標
    private int currentObjectIndex = 0;      // 現在のオブジェクトインデックス
    private Dictionary<GameObject, ObjectPlacementLimit> placementLimitsDict;   // 辞書に変換して管理


    [System.Serializable]
    public class ObjectPlacementLimit
    {
        public GameObject objectType;  // オブジェクトの種類
        public int maxCount;          // この種類の最大設置数
        public int currentCount;      // 現在の設置数
    }


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

        // 仮置き中かつ previewObject が null でない場合のみ更新
        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }


    void Start()
    {
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

        isPreviewing = true; // 仮置き状態を設定
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
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            // 最大数を超えている場合は設置を無効化
            if (limit.currentCount >= limit.maxCount)
            {
                return;
            }

            // カウントを更新
            placementLimits[currentObjectIndex].currentCount++;

        }

        if (!previewObject.activeSelf) return;

        // 確定配置時にマテリアルを元に戻す
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }

        //UIの数値も更新
        diffenceUI.GetComponent<UIDiffenceCount>().CheckDiffenceNum(currentObjectIndex);

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

}
