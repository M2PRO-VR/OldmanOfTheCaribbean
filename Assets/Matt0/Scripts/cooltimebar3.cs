using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cooltimebar3 : MonoBehaviour
{
    public Image cooltime3;
    public static bool ctStart3 = false;
    public static bool cooltime3Now = false;
    float addValue = 0f;
    float alpha = 0f;

    //cooltimeNow が　true　のときはクールタイムに入ってる
    //cooltimeNow が　false のときにスキルを使えるようにすれば大丈夫
    // Use this for initialization
    void Start()
    {
        cooltime3 = GameObject.Find("cooltimebar3").GetComponent<Image>();
        ctStart3 = false;
        cooltime3.fillAmount = 1f;
        cooltime3Now = false;
        cooltime3.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (ctStart3)
        {
            cooltime3.enabled = true;
            changePicturesUpdate();
        }

    }
    //黒い球のクールタイム
    void changePicturesUpdate()
    {
        cooltime3Now = true;
        alpha = cooltime3.fillAmount;
        addValue = Time.deltaTime / 7f;
        alpha -= addValue;
        cooltime3.fillAmount = alpha;

        if (cooltime3.fillAmount <= 0)
        {
            ctStart3 = false;
            cooltime3.fillAmount = 1;
            cooltime3Now = false;
            cooltime3.enabled = false;
        }
    }


    public static void cooltime3_start(bool start)
    {
        ctStart3 = start;
    }
}
