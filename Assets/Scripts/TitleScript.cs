using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Image[] menuItems; // 項目画像（縦に並べる）
    [SerializeField] private Vector2[] menuItemPositions; // 各項目のXY座標
    [SerializeField] private Image selectorFrame; // 選択枠
    [SerializeField] private string[] scenes; // 各項目で遷移するシーン名
    [SerializeField] private float selectorOffset = 10f; // 枠のマージン
    [SerializeField] private KeyCode upKey = KeyCode.UpArrow; // 上移動キー
    [SerializeField] private KeyCode downKey = KeyCode.DownArrow; // 下移動キー
    [SerializeField] private KeyCode selectKey = KeyCode.Return; // 選択キー

    private int currentIndex = 0; // 現在の選択インデックス
    private SceneChange sceneChange; // フェード付きシーン遷移

    private void Start()
    {
        if (menuItems.Length != scenes.Length || menuItems.Length != menuItemPositions.Length)
        {
            Debug.LogError("項目画像、シーン名、または座標の数が一致していません！");
            return;
        }

        // 各項目の座標を設定
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] != null)
            {
                menuItems[i].rectTransform.anchoredPosition = menuItemPositions[i];
            }
        }

        // 初期位置に選択枠を設定
        UpdateSelectorPosition();

        // SceneChangeコンポーネントを取得
        sceneChange = GetComponent<SceneChange>();
    }

    private void Update()
    {
        // 上下キーで選択項目を変更
        if (Input.GetKeyDown(upKey))
        {
            Debug.Log("項目の選択を上に移動します");
            currentIndex = (currentIndex - 1 + menuItems.Length) % menuItems.Length; // 循環する
            UpdateSelectorPosition();
        }
        else if (Input.GetKeyDown(downKey))
        {
            Debug.Log("項目の選択を下に移動します");
            currentIndex = (currentIndex + 1) % menuItems.Length; // 循環する
            UpdateSelectorPosition();
        }

        // Enterキーで選択項目に対応するシーンをロード
        if (Input.GetKeyDown(selectKey))
        {
            Debug.Log("項目を選択しました");
            if (sceneChange != null && !string.IsNullOrEmpty(scenes[currentIndex]))
            {
                sceneChange.SetSceneName(scenes[currentIndex]); // 遷移先シーンを設定
                //sceneChange.ChangeScene(); // フェード付きシーン遷移
            }
        }
    }

    // 選択枠の位置を更新する
    private void UpdateSelectorPosition()
    {
        if (selectorFrame != null && menuItems[currentIndex] != null)
        {
            RectTransform menuItemRect = menuItems[currentIndex].rectTransform;
            RectTransform selectorRect = selectorFrame.rectTransform;

            // 選択枠を項目画像に合わせて移動
            selectorRect.anchoredPosition = menuItemRect.anchoredPosition;

            // 選択枠のサイズを項目画像に合わせて調整
            selectorRect.sizeDelta = menuItemRect.sizeDelta + new Vector2(selectorOffset, selectorOffset);
        }
    }
}