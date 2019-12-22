using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOldObjects : MonoBehaviour
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
		if (collision.gameObject.tag == "tag_ROAD")
		{
			Destroy(collision.gameObject);//衝突したRoadを削除
			GameObject.Find("GameMaster").GetComponent<CreateField>().UpdateIdOldestRoad();//Road最古idを更新
		}

		if (collision.gameObject.tag == "tag_WALL")
		{
			Destroy(collision.gameObject);//衝突したWallを削除
		}

		if (collision.gameObject.tag == "tag_TARGET")
		{
			Destroy(collision.gameObject);//衝突したTargetを削除
		}

		if (collision.gameObject.tag == "tag_ARROW")
		{
			Destroy(collision.gameObject);//衝突したTargetを削除
		}

		if (collision.gameObject.tag == "tag_BLOCK")
		{
			Destroy(collision.gameObject);//衝突したBlockを削除
			GameObject.Find("GameMaster").GetComponent<CreateField>().SetPermitCreateBlock();//ブロック生成許可
		}

		if (collision.gameObject.tag == "tag_ITEM")
		{
			Destroy(collision.gameObject);//衝突したItemを削除
			GameObject.Find("GameMaster").GetComponent<CreateField>().SetPermitCreateItem();//Item生成許可
		}
	}
}
