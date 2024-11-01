using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���̃����[�h�p

public class GameOverTrigger : MonoBehaviour
{
    // �g���K�[�R���C�_�[�ɑ��̃I�u�W�F�N�g���G�ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�I�u�W�F�N�g�� "Player" �^�O��t���Ă���ꍇ�ɂ̂ݏ�������
        if (other.CompareTag("Player"))
        {
            EndGame(); // �Q�[���I������
        }
    }

    // �Q�[�����I�������郁�\�b�h
    private void EndGame()
    {
        Debug.Log("�Q�[���I�[�o�[");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditor�̎��s���~���鏈��
#else
        Application.Quit();                                // �Q�[�����I�����鏈��
#endif
        // ���Ԃ��~���ăQ�[���̓�����~
        // Time.timeScale = 0;

        // �������́A�V�[���������[�h���ăQ�[�������Z�b�g����ꍇ
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
