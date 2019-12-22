using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "tag_TARGET")
		{
			GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundHitTarget();//効果音再生
			GameObject.Find("GameMaster").GetComponent<ManageScore>().AddScore();//スコア加算処理をコール
			GameObject.Find("GameMaster").GetComponent<ManageArrow>().AddArrows(3);//矢の本数加算処理をコール
			GameObject.Find("GameMaster").GetComponent<ControlUI>().DispInc(3);//得点表示
		}

		if(collision.gameObject.tag == "tag_WALL")
		{
			GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundHitWall();//壁衝突時の効果音再生
		}

		if(collision.gameObject.tag == "tag_ITEM")
		{
			GameObject.Find("AudioPlayer").GetComponent<PlaySounds>().PlaySoundHitItem();//効果音再生
			GameObject.Find("Item").GetComponent<ManageAnimItem>().SetFlgItemHit();//ヒット時のアニメーション再生
			GameObject.Find("GameMaster").GetComponent<ShootArrow>().AddNumArrowPattern();//アイテム効果処理
		}
	}
}
