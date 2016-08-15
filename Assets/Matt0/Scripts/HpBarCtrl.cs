using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる
using UnityEngine.SceneManagement;

public class HpBarCtrl : MonoBehaviour
{

    private Slider _slider;
    public UnityEngine.UI.Text hp;
    public float PLAYER_HP; //プレイヤーの最大HP


    public static bool kirikae = false;
    public static float dmg;
    public static bool dmgflg = false;
    public static float dmgl;
    public static bool hanteiflg = false;
    public static float dmghozi;
    public static bool escapenow = false;   //UnitychanControllerから受け取り回避中かどうか

    private int _hp = 0;

    public static bool restoration_flg = false;    //アイテム拾ったら回復するフラグ
    private int restoration_amount = 300;    //回復量
    private int after_hp;
    private bool firstflg = false;

    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _slider.maxValue = PLAYER_HP;
    }

    void Update()
    {
        // HP上昇
        if (_hp < PLAYER_HP && kirikae == false)
        {
            _hp += ((int)PLAYER_HP/100);
            // HPゲージに値を設定
            _slider.value = _hp;
            hp.text = _hp + "/"+ _slider.maxValue;
            if (_hp >= _slider.maxValue)
            {
                _hp = (int)PLAYER_HP;
                _slider.value = _hp;
                hp.text = _hp + "/" + _slider.maxValue;
                kirikae = true;
            }

        }
        if (dmgflg == true&& hanteiflg==false && escapenow == false)
        {
            dmgl = _hp - dmg;   //AfterDamege = NowHP - Damage
            hanteiflg = true;

            dmgflg = false;
        }

        if (hanteiflg == true) //１ずつ減らすための判定
        {
            if (_hp == dmgl) { //ダメージ後と現在HPが同じならfalseにする

                hanteiflg = false;
                if (_hp <= 0)
                {
                    _hp = 0;
                    kirikae = false;
                    dmgflg = false;
                    hanteiflg = false;
                    escapenow = false;
                    ChangeWeaponsKey.weapon_f1 = false;
                    ChangeWeaponsKey.weapon_f2 = false;
                    SceneManager.LoadScene("lose");
                }
            }
            if (_hp != dmgl)
            {
                _hp -= 1;
                if (_hp < 0)
                {
                    _hp = 0;

                    hanteiflg = false;
                    if (_hp <= 0)
                    {
                        kirikae = false;
                        dmgflg = false;
                        hanteiflg = false;
                        escapenow = false;
                        ChangeWeaponsKey.weapon_f1 = false;
                        ChangeWeaponsKey.weapon_f2 = false;
                        SceneManager.LoadScene("lose");
                    }
                }
                _slider.value = _hp;
                hp.text = _hp + "/" + _slider.maxValue;
            }

        }

        //アイテム拾ったら全回復
        if (restoration_flg == true) {
            if (firstflg == false)
            {
                after_hp = _hp + (int)restoration_amount;
                firstflg = true;
            }
            if(after_hp >= PLAYER_HP)
            {
                after_hp = (int)PLAYER_HP;
            }

            if (_hp >= after_hp)
            {
                _hp = after_hp;
                _slider.value = _hp;
                hp.text = _hp + "/" + _slider.maxValue;
                restoration_flg = false;
                firstflg = false;
            }
            else
            {
                _hp += ((int)PLAYER_HP / 100);
                // HPゲージに値を設定
                _slider.value = _hp;
                hp.text = _hp + "/" + _slider.maxValue;
            }
        }

        if(escapenow == true)
        {
            dmgflg = false;
        }

    }

    public static void Damage(float Damage, bool dmflg)
    {
        dmg = Damage;
        dmgflg = dmflg;
    }

}
