//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    //�ȗ�

//    protected override void Damage()
//    {
//        //���Ƀ_���[�W��ԁi�����G���Ԓ��j�Ȃ�I��
//        if (damage)
//        {
//            return;
//        }

//        //�q�b�g�|�C���g��-1
//        //Hp�v���p�e�B�ɂ��AHP��0�ɂȂ�Ǝ����I��Dead()���Ă΂��i��BaseCharacterController�Q�Ɓj
//        Hp--;

//        if (Hp > 0)
//        {
//            StartCoroutine("DamageTimer");
//        }
//    }

//    //�ǉ�
//    //�_���[�W���󂯂��u�Ԃ̖��G���Ԃ̃^�C�}�[
//    protected IEnumerator DamageTimer()
//    {
//        //���Ƀ_���[�W��ԂȂ�I��
//        if (damage)
//        {
//            yield break;
//        }

//        damage = true;

//        animator.SetTrigger("Damage");

//        //���G���Ԓ��̓_��
//        for (int i = 0; i < 10; i++)
//        {
//            spriteRenderer.enabled = false;
//            yield return new WaitForSeconds(0.05f);

//            spriteRenderer.enabled = true;
//            yield return new WaitForSeconds(0.05f);
//        }

//        damage = false;
//    }

//    //�ǉ�
//    //�G�ɐG�ꂽ��_���[�W
//    protected void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            Damage();
//        }
//    }

//    //�ȗ�
//}
