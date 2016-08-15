using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HP_Restore_Counter : MonoBehaviour {

    public static bool Restore_preserve_flg = false; //HP_Restorationからドーナツ拾った時フラグ受け取り
    public static bool button_flg = false;  //過去に押したというフラグ

    private int donut = 0;  //ドーナツの数

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = donut + "個";
    }
	
	// Update is called once per frame
	void Update () {
	    if(Restore_preserve_flg == true)
        {
            donut++;
            this.GetComponent<Text>().text = donut + "個";
            Restore_preserve_flg = false;
        }

        if ((Input.GetKeyDown(KeyCode.Q) || button_flg == true) && cooltimebar3.cooltime3Now == false)
        {
            if (donut > 0)
            {
                donut = donut - 1;
                HpBarCtrl.restoration_flg = true;
                cooltimebar3.cooltime3_start(true);
                this.GetComponent<Text>().text = "" + donut + "個";
            }
        }
        button_flg = false;
        Debug.Log(button_flg);
    }

}
