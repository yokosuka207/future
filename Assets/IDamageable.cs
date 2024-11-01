//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    //省略

//    protected override void Damage()
//    {
//        //既にダメージ状態（＝無敵時間中）なら終了
//        if (damage)
//        {
//            return;
//        }

//        //ヒットポイントを-1
//        //Hpプロパティにより、HPが0になると自動的にDead()が呼ばれる（※BaseCharacterController参照）
//        Hp--;

//        if (Hp > 0)
//        {
//            StartCoroutine("DamageTimer");
//        }
//    }

//    //追加
//    //ダメージを受けた瞬間の無敵時間のタイマー
//    protected IEnumerator DamageTimer()
//    {
//        //既にダメージ状態なら終了
//        if (damage)
//        {
//            yield break;
//        }

//        damage = true;

//        animator.SetTrigger("Damage");

//        //無敵時間中の点滅
//        for (int i = 0; i < 10; i++)
//        {
//            spriteRenderer.enabled = false;
//            yield return new WaitForSeconds(0.05f);

//            spriteRenderer.enabled = true;
//            yield return new WaitForSeconds(0.05f);
//        }

//        damage = false;
//    }

//    //追加
//    //敵に触れたらダメージ
//    protected void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            Damage();
//        }
//    }

//    //省略
//}
