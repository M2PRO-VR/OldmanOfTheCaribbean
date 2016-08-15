using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeWeaponsKey : MonoBehaviour
{

    public GameObject Weapon1;  //生成武器その１
    public GameObject Weapon2;  //生成武器その２
	public Transform spawn1;
	public Transform spawn2;
    public float Weapon1_ATKPOW;
    public float Weapon2_ATKPOW;

    private GameObject weapon1;
    private GameObject weapon2;


    private bool Weaponkirikae1 = false;
    private bool Weaponkirikae2 = false;
    private bool flag1 = true;
    private bool flag2 = true;
    public static bool Button1=false;
    public static bool Button2=false;

	public static bool have_weapon1 = false;
	public static bool have_weapon2 = false;

    public static bool weapon_f1 = false;
    public static bool weapon_f2 = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (((Input.GetKeyDown(KeyCode.F1)&&flag1==true) || (Button1==true && flag1==true)) && weapon_f1 == true)
        {
			have_weapon1 = true;
			have_weapon2 = false;
            flag1 = false;
            flag2 = true;
            Button1 = false;
            Destroy(weapon2);
            weapon1 = (GameObject)Instantiate(Weapon1, spawn1.position, spawn1.rotation);

            Weaponkirikae1 = true;
            Weaponkirikae2 = false;
        }

        if (Weaponkirikae1==true)
        {
           
            weapon1.transform.position = spawn1.position;
            weapon1.transform.rotation = spawn1.rotation;

            WeaponAttack.WeaponDamageAmount = Weapon1_ATKPOW;
        }

        if (((Input.GetKeyDown(KeyCode.F2) && flag2 == true) || (Button2 == true && flag2 == true)) && weapon_f2 == true)
        {
			have_weapon1 = false;
			have_weapon2 = true;
            flag1 = true;
            flag2 = false;
            Button2 = false;
            Destroy(weapon1);
            weapon2 = (GameObject)Instantiate(Weapon2, spawn2.position, spawn2.rotation);

            Weaponkirikae1 = false;
            Weaponkirikae2 = true;
        }

        if (Weaponkirikae2==true)
        {
          
            weapon2.transform.position = spawn2.position;
            weapon2.transform.rotation = spawn2.rotation;
            WeaponAttack.WeaponDamageAmount = Weapon2_ATKPOW;
        }

    }

    public static void button1(bool button1)//ボタン１からの判定を受け取る
    {
        Button1 = button1;      
    }

    public static void button2(bool button2)//ボタン2からの判定を受け取る
    {
        Button2 = button2;
    }

}
