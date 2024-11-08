using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charaCon;
    private Vector3 moveInput;

    public Transform camTrans;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    void Start()
    {

    }

    void Update()
    {
        // 移動処理
        Vector3 verMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + verMove;
        moveInput.Normalize();
        moveInput = moveInput * moveSpeed;

        charaCon.Move(moveInput * Time.deltaTime);

        // 右クリックが押されているときのみカメラを回転
        if (Input.GetMouseButton(1)) // 1は右クリック
        {
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }
            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }

            // プレイヤーの水平回転
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + mouseInput.x,
                transform.rotation.eulerAngles.z
            );

            // カメラの垂直回転
            camTrans.rotation = Quaternion.Euler(
                camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f)
            );
        }
    }
}
