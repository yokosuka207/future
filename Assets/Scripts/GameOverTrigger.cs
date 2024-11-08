using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string sceneName;

    // �g���K�[�R���C�_�[�ɑ��̃I�u�W�F�N�g���G�ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�I�u�W�F�N�g�� "Player" �^�O��t���Ă���ꍇ�ɂ̂ݏ�������
        if (other.CompareTag("Player"))
        {
            // �V�[���J��
            SceneManager.LoadScene(sceneName);
        }
    }
}
