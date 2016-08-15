using UnityEngine;
using System.Collections;

public class ChangeWeapons : MonoBehaviour
{
    //拾う武器、アイテムにスクリプトつける。
    public GameObject pick_up_item; //拾うものの武器アイコン

    // Use this for initialization
    void Start()
    {
        pick_up_item.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //  接触時に呼ばれるコールバック
    void OnTriggerEnter(Collider hit)
    {
        //接触対象はPlayerタグですか？
        if (hit.CompareTag("Player"))
        {
            pick_up_item.gameObject.SetActive(true);
            ChangeWeaponsKey.weapon_f2 = true;      
            Destroy(gameObject);
        }
    }

}