using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject subCamera;

    public Camera ActiveCamera { get; private set; }
    public ObjectSpawner objectSpawner;

    private InputAction switchCameraAction;
    private bool isGamepadConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        subCamera.SetActive(false);
        ActiveCamera = mainCamera.GetComponent<Camera>();

        //var inputActionAsset = new InputActionAsset();  // InputActionAssetを指定
        //switchCameraAction = inputActionAsset.FindAction("Player/camera");  // "UI" アクションマップ内の "SwitchCamera" を取得

        //// Actionのイベント登録
        //switchCameraAction.performed += _ => SwitchCamera();

        isGamepadConnected = Gamepad.current != null;
    }

    // Update is called once per frame
    void Update()
    {
        isGamepadConnected = Gamepad.current != null;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            subCamera.SetActive(!subCamera.activeSelf);

            ActiveCamera = mainCamera.activeSelf ? mainCamera.GetComponent<Camera>() : subCamera.GetComponent<Camera>();

            objectSpawner.RefreshPreviewObject();
        }

        if(isGamepadConnected && switchCameraAction.triggered)
        {
            SwitchCamera();
        }
    }

    // カメラ切り替え処理
    private void SwitchCamera()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        subCamera.SetActive(!subCamera.activeSelf);

        ActiveCamera = mainCamera.activeSelf ? mainCamera.GetComponent<Camera>() : subCamera.GetComponent<Camera>();

        // ObjectSpawnerの仮置きオブジェクト位置を更新
        objectSpawner.RefreshPreviewObject();
    }
}
