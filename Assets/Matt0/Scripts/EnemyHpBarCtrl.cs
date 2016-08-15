using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class EnemyHpBarCtrl : MonoBehaviour
{
    public static float dmg=0;
    public static int enehp=0;
    public static float maxenehp=0;
    public static bool enemyflg=false;
    public static bool dmgflg = false;
    public static int dmgl;
    public static bool itizutuherasuhantei = false;
    public static bool range;
    public UnityEngine.UI.Text enetext;
    Slider slider;

    public static float CureAmount=0;
    public static bool Restoreflg = false;
    private float hozon = 0;
    public static bool itizutuhuyasuhantei = false;

    void Start()
    {
        slider = GameObject.Find("EnemyHp").GetComponent<Slider>();
        slider.gameObject.SetActive(false);
        enetext.gameObject.SetActive(false);
    }

    void Update()
    {
        //ダメージ処理可視化
        if (dmgflg == true && itizutuherasuhantei==false)   
        {
            slider.gameObject.SetActive(true);
            enetext.gameObject.SetActive(true);
            itizutuherasuhantei = true;
            dmgflg = false;
            slider.maxValue = maxenehp;
            slider.value = enehp;
            enetext.text = enehp + "/"+maxenehp;
            dmgl = (int)(enehp - dmg);   //AfterDamege = NowHP - Damage
        }

        if (itizutuherasuhantei == true)
        {

            if (enehp == dmgl)
            {
                itizutuherasuhantei = false;
                if (enehp <= 0)
                {
                    slider.gameObject.SetActive(false);
                    enetext.gameObject.SetActive(false);
                    enehp = 0;
                    itizutuherasuhantei = false;
                    WeaponAttack.EnemyHP(true);
                }
            }
            else
            {
                enehp -= 1;
                if (enehp <= 0)
                {
                    slider.gameObject.SetActive(false);
                    enetext.gameObject.SetActive(false);
                    enehp = 0;
                    itizutuherasuhantei = false;
                    WeaponAttack.EnemyHP(true);
                }
                slider.value = enehp;
                enetext.text = enehp + "/" + maxenehp;
            }
        }

        if (range == true)
        {
            slider.gameObject.SetActive(false);
            enetext.gameObject.SetActive(false);
            range = false;
        }

        //回復処理可視化
        if (Restoreflg == true && itizutuhuyasuhantei == false)
        {
            slider.gameObject.SetActive(true);
            enetext.gameObject.SetActive(true);
            itizutuhuyasuhantei = true;
            Restoreflg = false;
            slider.maxValue = maxenehp;
            slider.value = enehp;
            enetext.text = enehp + "/" + maxenehp;
            hozon = enehp + CureAmount;   //AfterDamege = NowHP + Restore
            if (hozon >= maxenehp)
            {
                hozon = maxenehp;
            }
        }
        if (itizutuhuyasuhantei == true)
        {

            if (enehp >= hozon)
            {
                enehp = (int)hozon;
                slider.value = enehp;
                enetext.text = enehp + "/" + maxenehp;
                itizutuhuyasuhantei = false;
            }
            else
            {
                enehp += enehp/100;
                slider.value = enehp;
                enetext.text = enehp + "/" + maxenehp;
            }
        }

    }

    public static void EnemyDamage(float Damage, bool Dmflg, float MaxEnemyhp, float Enemyhp)
    {
        dmg = Damage;
        dmgflg = Dmflg;
        enehp = (int)Enemyhp;
        maxenehp = MaxEnemyhp;
    }

    public static void EnemyRestore(float Cure, bool restoreflg, float MaxEnemyhp, float Enemyhp)
    {
        CureAmount = Cure;
        Restoreflg = restoreflg;
        enehp = (int)Enemyhp;
        maxenehp = MaxEnemyhp;
    }

    public static void RangeEnemy(bool bol)
    {
        range = bol;
    }
}