using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class e_hpbarSlider : MonoBehaviour
{


    public GameObject GUIWatchPoint2; //target

    private Canvas hpbar;    //hpbar   
    private GameObject NameChaserObject2;    //target_Name_Chaser_Object

    private Camera rotatecamera; //正面向く為のカメラ変数
    

    //カメラに写っているかのフラグ
    private bool renderflg = false;


    public Slider h_pbar;
    public static float currentvalue;

    private float sub_enemymaxhp;   //Enemy_POPから値受け取り
    private float sub_enemycurrenthp;   //Enemy_POPから値受け取り

    //プレイヤー距離で写すフラグ
    public bool view_ENEMYflg;

    public float Enemy_hp_height_x; //HP表示のx軸位置
    public float Enemy_hp_height_y; //HP表示のy軸位置
    public float Enemy_hp_height_z; //HP表示のx軸位置

    // Use this for initialization
    void Start()
    {
        rotatecamera = Camera.main;
        hpbar = GUIWatchPoint2.gameObject.transform.Find("ene_hp").GetComponent<Canvas>();

        NameChaserObject2 = GUIWatchPoint2.gameObject.transform.FindChild("TargetNameChaser").gameObject;

        h_pbar = hpbar.gameObject.transform.FindChild("Panel").gameObject.transform.FindChild("e_hp").GetComponent<Slider>();
        WeaponAttack small_slider = GetComponent<WeaponAttack>();
        small_slider.sub_hpbar = h_pbar;
        small_slider.sub_hpbar.maxValue = sub_enemymaxhp;
        small_slider.sub_hpbar.value = sub_enemycurrenthp;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.gameObject.SetActive(renderflg);
        hpbar.transform.position = NameChaserObject2.transform.position + new Vector3(Enemy_hp_height_x, Enemy_hp_height_y, Enemy_hp_height_z);
        hpbar.transform.rotation = rotatecamera.transform.rotation;
        renderflg = false;

    }

    //カメラに映っていないときにhpバー消す
    //映っている時のメソッド
    void OnWillRenderObject()
    {
        if (Camera.current.tag == "MainCamera")
        {
            if (view_ENEMYflg == true)
            {
                renderflg = true;
            }
        }
    }

    public void EnemyInfo_SubMaxHP(float submaxhp)
    {
        sub_enemymaxhp = submaxhp;
    }

    public void EnemyInfo_SubCurrentHP(float subcurrenthp)
    {
        sub_enemycurrenthp = subcurrenthp;
    }
}
