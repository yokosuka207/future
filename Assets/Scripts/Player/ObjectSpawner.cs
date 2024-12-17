using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // ��������I�u�W�F�N�g�̔z��
    public CameraControll cameraController;              // �J�����؂�ւ��X�N���v�g
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;
    public ObjectPlacementLimit[] placementLimits; // �I�u�W�F�N�g���Ƃ̐ݒ�
    [SerializeField] private GameObject diffenceUI;
    [SerializeField] private GameObject player;

    private Material originalMaterial;
    private GameObject previewObject;        // ���u���I�u�W�F�N�g
    private bool isPreviewing = false;       // ���u����ԃt���O
    private Vector3 previewWorldPosition;    // ���u���I�u�W�F�N�g�̃��[���h���W
    private int currentObjectIndex = 0;      // ���݂̃I�u�W�F�N�g�C���f�b�N�X
    private Dictionary<GameObject, ObjectPlacementLimit> placementLimitsDict;   // �����ɕϊ����ĊǗ�
    private Dictionary<GameObject, int> objectUsageCounts = new Dictionary<GameObject, int>();

    private Vector2 leftStickInput = Vector2.zero;
    private Vector2 rightStickInput = Vector2.zero;

    [System.Serializable]
    public class ObjectPlacementLimit
    {
        public GameObject objectType;  // �I�u�W�F�N�g�̎��
        public int maxCount;          // ���̎�ނ̍ő�ݒu��
        public int currentCount;      // ���݂̐ݒu��
    }


    void Update()
    {
        

        if(player.GetComponent<SoundWave>().currentHealth > 0)
        {
            HandleObjectSwitch(); // �e���L�[���͂ŃI�u�W�F�N�g��؂�ւ�
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

            // ���u�������� previewObject �� null �łȂ��ꍇ�̂ݍX�V
            if (isPreviewing && previewObject != null)
            {
                UpdatePreviewPosition();
            }
        }
        
    }


    void Start()
    {
        // �V�[�����܂����ŃI�u�W�F�N�g��ێ�����
        DontDestroyOnLoad(gameObject);

        placementLimitsDict = new Dictionary<GameObject, ObjectPlacementLimit>();

        // �����I�u�W�F�N�g�𐔎��L�[�u1�v�ɑΉ�
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            currentObjectIndex = 0; // �z��̍ŏ��̃I�u�W�F�N�g��ݒ�
        }

        // �z�񂩂玫���ɕϊ�
        foreach (var limit in placementLimits)
        {
            if (limit != null && limit.objectType != null)
            {
                placementLimitsDict[limit.objectType] = limit;
                limit.currentCount = 0; // ������
                objectUsageCounts[limit.objectType] = 0; // �g�p�񐔂�������
            }
        }
    }




    void HandleObjectSwitch()
    {
        // �z�񂪋�̏ꍇ�͏������Ȃ�
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            return;
        }

        // �����L�[�Ő؂�ւ�
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            // �����L�[1���C���f�b�N�X0�ɑΉ�
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentObjectIndex = i;

                // ���u���I�u�W�F�N�g�����݂��Ă���ꍇ�͍Đ���
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
        // ���ݑI�𒆂̃I�u�W�F�N�g���擾
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        // �ݒu���������m�F
        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            if (limit.currentCount >= limit.maxCount)
            {
                isPreviewing = false; // ���u����Ԃ�����
                return; // ���u���I�u�W�F�N�g�𐶐����Ȃ�
            }
        }

        // ���u���I�u�W�F�N�g�𐶐�
        previewObject = Instantiate(currentObject);

        // �������}�e���A����ݒ�
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;

            // �����������邽�߂̃}�e���A�����쐬
            Material transparentMat = new Material(originalMaterial);
            Color color = transparentMat.color;
            color.a = 0.5f; // �A���t�@�l��ύX���Ĕ�������
            transparentMat.color = color;
            renderer.material = transparentMat;
        }

        // ���u����Ԃɂ��邽�߂�Collider�𖳌���
        Collider collider = previewObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        isPreviewing = true; // ���u����Ԃ�ݒ�
    }





    void UpdatePreviewPosition()
    {
        //// ���݂̃A�N�e�B�u�ȃJ�������擾
        //Camera activeCamera = cameraController.ActiveCamera;

        //// �}�E�X�ʒu���X�N���[�����W�Ƃ��Ď擾
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = spawnDistance; // �J��������̋�����ݒ�

        //// �X�N���[�����W�����[���h���W�ɕϊ�
        //previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);

        //// ���u���I�u�W�F�N�g�̈ʒu���X�V
        //previewObject.transform.position = previewWorldPosition;
        //previewObject.transform.rotation = Quaternion.identity;

        // ���݂̃A�N�e�B�u�ȃJ�������擾
        Camera activeCamera = cameraController.ActiveCamera;

        // �}�E�X���͂��L���ȏꍇ
        if (Input.mousePresent)
        {
            // �}�E�X�ʒu���X�N���[�����W�Ƃ��Ď擾
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = spawnDistance; // �J��������̋�����ݒ�

            // �X�N���[�����W�����[���h���W�ɕϊ�
            previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);
        }

        // �R���g���[���[�̍��X�e�B�b�N���͂��L���ȏꍇ
        if (Gamepad.current != null && leftStickInput != Vector2.zero)
        {
            // �R���g���[���[�̍��X�e�B�b�N���͂��J�����̕����ɕϊ�
            Vector3 controllerOffset = new Vector3(leftStickInput.x, leftStickInput.y, 0) * spawnDistance;
            previewWorldPosition = activeCamera.transform.position + activeCamera.transform.TransformDirection(controllerOffset);
        }

        // ���u���I�u�W�F�N�g�̈ʒu���X�V
        previewObject.transform.position = previewWorldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            // �ő吔�𒴂��Ă���ꍇ�͐ݒu�𖳌���
            if (limit.currentCount >= limit.maxCount)
            {
                return;
            }

            // �J�E���g���X�V
            limit.currentCount++;

            // �g�p�񐔂��X�V
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

        // �m��z�u����Collider��L����
        Collider collider = previewObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            //UI�\���X�N���v�g�Ɍ��`�F�b�N���w������
            diffenceUI.GetComponent<UIDiffenceCount>().CheckDiffenceNum(currentObjectIndex);
        }

        // �m��z�u���Ƀ}�e���A�������ɖ߂�
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
        float direction = value.Get<float>(); // -1: ���V�����_�[, 1: �E�V�����_�[
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
