using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private KeyCode ActivateKey = KeyCode.None; // 自爆ボタン
    [SerializeField] private KeyCode upKey = KeyCode.UpArrow; // 上移動キー
    [SerializeField] private KeyCode downKey = KeyCode.DownArrow; // 下移動キー
    [SerializeField] private KeyCode selectKey = KeyCode.Return; // 選択キー
    [SerializeField] private Image[] menuItems; // 項目画像（縦に並べる）
    [SerializeField] private Vector2[] menuItemPositions; // 各項目のXY座標
    [SerializeField] private Image selectorFrame; // 選択枠
    [SerializeField] private string[] scenes; // 各項目で遷移するシーン名
    [SerializeField] private float selectorOffset = 10f; // 枠のマージン

    private int currentIndex = 0; // 現在の選択インデックス
    private SceneChange sceneChange; // フェード付きシーン遷移

    public int HP = 100; // プレイヤーのHP
    public GameObject gameOverUI; // ゲームオーバーUI
    public Image blackScreen; // 黒い背景（Image）
    public Slider gauge; // ゲージ (Slider)
    public int time = 0; // 時間変数 (0～100)

    private bool isGameOver = false;
    private bool isFading = false;

    private void Start()
    {

        // 各項目の座標を設定
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] != null)
            {
                menuItems[i].rectTransform.anchoredPosition = menuItemPositions[i];
            }
            else
            {
                Debug.LogWarning($"menuItems[{i}] が設定されていません");
            }
        }

        // 初期位置に選択枠を設定
        if (selectorFrame != null)
        {
            UpdateSelectorPosition();
        }
        else
        {
            Debug.LogWarning("selectorFrame が設定されていません");
        }


    }
    void Update()
    {
        // HPが0以下でゲームオーバーを一度だけ実行
        if (HP <= 0 && !isGameOver)
        {
            GameOver();
        }

        if (isGameOver)
        {
            HandleGameOverInput();
            // SceneChangeコンポーネントを取得
            sceneChange = GetComponent<SceneChange>();
        }
        else if (!isGameOver)
        {
            // ゲージが進行する (時間を増加)
            time = Mathf.Clamp(time + 1, 0, 60000);
            //gauge.value = time / 100f;
            
        }

        //自爆装置　HP管理系を作り終えたら消すこと
        if (ActivateKey != KeyCode.None && Input.GetKeyDown(ActivateKey) && !isFading)
        {
            HP = 0;
        }

    }

    void GameOver()
    {
        if (blackScreen == null || gameOverUI == null || gauge == null)
        {
            Debug.LogError("必要なオブジェクトが設定されていません");
            return;
        }
        isGameOver = true;

        // ゲーム画面を一時停止
        Time.timeScale = 0;

        // 黒い画面をフェードイン
        StartCoroutine(FadeInBlackScreen());

        // ゲージを固定し、ゲームオーバーUIを有効化
        gameOverUI.SetActive(true);
        gauge.value = time / 100f;
       
    }

    // 黒い画面をフェードイン
    IEnumerator FadeInBlackScreen()
    {
        float alpha = 0;
        Color color = blackScreen.color;

        while (alpha < 0.65f)
        {
            alpha += Time.unscaledDeltaTime / 1; // 2秒でフェードイン
            color.a = alpha;
            blackScreen.color = color;
            yield return null;
        }

        // 最終的にアルファ値を0.5に設定
        color.a = 0.65f;
        blackScreen.color = color;

    }

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

    private void HandleGameOverInput()
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
            Time.timeScale = 1;
            if (sceneChange != null && !string.IsNullOrEmpty(scenes[currentIndex]))
            {
                sceneChange.SetSceneName(scenes[currentIndex]); // 遷移先シーンを設定
                sceneChange.ChangeScene(scenes[currentIndex]); // フェード付きシーン遷移を実行
            }
        }
    }
}
