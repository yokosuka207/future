using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string sceneName;

    // トリガーコライダーに他のオブジェクトが触れたときに呼び出されるメソッド
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーオブジェクトに "Player" タグを付けている場合にのみ処理する
        if (other.CompareTag("Player"))
        {
            // シーン遷移
            SceneManager.LoadScene(sceneName);
        }
    }
}
