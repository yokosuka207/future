using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private GameObject effectObj;
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject saveData;

    // �g���K�[�R���C�_�[�ɑ��̃I�u�W�F�N�g���G�ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�I�u�W�F�N�g�� "Player" �^�O��t���Ă���ꍇ�ɂ̂ݏ�������
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GoalDirection());
        }
    }

    private IEnumerator GoalDirection()
    {
        // �S�[���I�u�W�F�N�g�𖳌����������ɁA�ڂɌ����Ȃ�����
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();

        if (meshRenderer != null) meshRenderer.enabled = false; // �����Ȃ�����
        if (collider != null) collider.enabled = false;         // �����蔻��𖳌���

        // �p�[�e�B�N���𐶐�
        GameObject particle = Instantiate(effectObj, gameObject.transform.position, Quaternion.identity);

        // �p�[�e�B�N���̍Đ����I���܂őҋ@
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            yield return new WaitForSeconds(ps.main.duration);
        }

        // �p�[�e�B�N�����폜
        Destroy(particle);

        //�@�N���A���̃f�[�^��ۑ�
        saveData.GetComponent<ObjectSpawner>().ClearDataSave();

        // �V�[����J��
        StartCoroutine(sceneManager.GetComponent<SceneChange>().ChangeScene(sceneName));
        //SceneManager.LoadScene(sceneName);
    }
}
