using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private GameObject effectObj;
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject saveData;

    // トリガーコライダーに他のオブジェクトが触れたときに呼び出されるメソッド
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーオブジェクトに "Player" タグを付けている場合にのみ処理する
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GoalDirection());
        }
    }

    private IEnumerator GoalDirection()
    {
        // ゴールオブジェクトを無効化する代わりに、目に見えなくする
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();

        if (meshRenderer != null) meshRenderer.enabled = false; // 見えなくする
        if (collider != null) collider.enabled = false;         // 当たり判定を無効化

        // パーティクルを生成
        GameObject particle = Instantiate(effectObj, gameObject.transform.position, Quaternion.identity);

        // パーティクルの再生が終わるまで待機
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            yield return new WaitForSeconds(ps.main.duration);
        }

        // パーティクルを削除
        Destroy(particle);

        //　クリア時のデータを保存
        saveData.GetComponent<ObjectSpawner>().ClearDataSave();

        // シーンを遷移
        StartCoroutine(sceneManager.GetComponent<SceneChange>().ChangeScene(sceneName));
        //SceneManager.LoadScene(sceneName);
    }
}
