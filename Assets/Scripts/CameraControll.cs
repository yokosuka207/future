using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject subCamera;

    public Camera ActiveCamera { get; private set; }
    public ObjectSpawner objectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        subCamera.SetActive(false);
        ActiveCamera = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            subCamera.SetActive(!subCamera.activeSelf);

            ActiveCamera = mainCamera.activeSelf ? mainCamera.GetComponent<Camera>() : subCamera.GetComponent<Camera>();

            objectSpawner.RefreshPreviewObject();
        }
    }
}
