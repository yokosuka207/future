using UnityEngine;
using UnityEngine.SceneManagement; // シーンのリロード用

public class GameOverTrigger : MonoBehaviour
{
    // トリガーコライダーに他のオブジェクトが触れたときに呼び出されるメソッド
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーオブジェクトに "Player" タグを付けている場合にのみ処理する
        if (other.CompareTag("Player"))
        {
            EndGame(); // ゲーム終了処理
        }
    }

    // ゲームを終了させるメソッド
    private void EndGame()
    {
        Debug.Log("ゲームオーバー");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
#else
        Application.Quit();                                // ゲームを終了する処理
#endif
        // 時間を停止してゲームの動作を停止
        // Time.timeScale = 0;

        // もしくは、シーンをリロードしてゲームをリセットする場合
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
