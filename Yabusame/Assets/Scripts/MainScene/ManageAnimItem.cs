using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAnimItem : MonoBehaviour
{
	private Animator AnimatorItem;

    // Start is called before the first frame update
    void Start()
    {
		AnimatorItem = GameObject.Find("Item").GetComponent<Animator>();
		AnimatorItem.SetBool("flg_item_hit", false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetFlgItemHit()
	{
		AnimatorItem.SetBool("flg_item_hit", true);
	}
}
