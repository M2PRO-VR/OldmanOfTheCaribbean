﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageKiller : MonoBehaviour
{

    public GameObject GateofBabylon;    //ゲート・オブ・バビロンオブジェクト
    private GameObject effect;
    private float intervalTime = 0;
    public float Emerge_Time;   //２秒ずっとその場所にいたら出現

    private float kill_intervalTime = 0;
    private float killTime = 4.0f;

    private bool emergence_flg = false; //出現ﾌﾗｸﾞ
    private GameObject Player;
    private bool killflg = false;


    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (killflg == true)
        {
            kill_intervalTime += Time.deltaTime;
            if (kill_intervalTime >= killTime)
            {
                HpBarCtrl.kirikae = false;
                ChangeWeaponsKey.weapon_f1 = false;
                ChangeWeaponsKey.weapon_f2 = false;
                SceneManager.LoadScene("lose");
                kill_intervalTime = 0;
            }
        }
    }

    void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            intervalTime += Time.deltaTime;
            if (intervalTime >= Emerge_Time)
            {
                if (emergence_flg == false)
                {
                    effect = (GameObject)Instantiate(GateofBabylon, Player.gameObject.transform.position, Player.gameObject.transform.rotation);
                    intervalTime = 0;
                    emergence_flg = true;
                    killflg = true;
                }
                //Debug.Log("hassei");
            }
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (emergence_flg == true)
            {
                Destroy(effect.gameObject);
                emergence_flg = false;
            }
            killflg = false;
            kill_intervalTime = 0;
            intervalTime = 0;
        }
    }

}
