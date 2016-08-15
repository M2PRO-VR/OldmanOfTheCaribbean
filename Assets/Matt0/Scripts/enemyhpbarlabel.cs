using UnityEngine;
using System.Collections;


public class enemyhpbarlabel : MonoBehaviour
{
    public GameObject GUIWatchPoint; //target
    public GameObject NameObject;   //targetオブジェクトの名前
    private GameObject NameChaserObject;    //target_Name_Chaser_Object
    private Vector3 ObjectPoint;// オブジェクトのワールド座標
    private Vector3 ObjectPointPlus;// オブジェクトのちょっと上の座標
    private Vector3 GUIScreenPoint;// オブジェクトのスクリーン座標

    private GameObject PlayerObj;   //プレイヤーオブジェクト
    private float distance_enemy;   //プレイヤーとの距離
    private float view_enemy = 30;   //敵の名前見える距離
    private bool viewenemyflg = false;  //敵距離で表示フラグ

    private float ScreenHeight = Screen.height;

    //カメラに写っているかのフラグ
    private bool renderflg = false;

    
    void Start()
    {
        NameChaserObject = GUIWatchPoint.gameObject.transform.FindChild("TargetNameChaser").gameObject;
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
    }


    void OnGUI()
    {
        if (renderflg == true && viewenemyflg == true)
        {
            //GUIの色変更等
            GUIStyle guistyle = new GUIStyle();
            GUIStyleState stylestate = new GUIStyleState();
            stylestate.textColor = Color.red;
            guistyle.normal = stylestate;


            //名前表示
            //Rect(position,"text",style)入力　←　座標等
            GUI.Label(new Rect(GUIScreenPoint.x - 60, ScreenHeight - GUIScreenPoint.y, 120, 40), NameObject.gameObject.name, guistyle);

        }
    }

    void Update()
    {
        ScreenHeight = Screen.height;

        // Objectの座標を代入
        //名前表示したいオブジェクトの子要素にあるTargetNameChaserの座標を取得して表示
        ObjectPoint = NameChaserObject.transform.position;
        ObjectPointPlus = new Vector3(0, 0, 0) + ObjectPoint;

        GUIScreenPoint = Camera.main.WorldToScreenPoint(ObjectPointPlus);


        distance_enemy = Vector3.Distance(PlayerObj.transform.position, GUIWatchPoint.transform.position);

        if (distance_enemy <= view_enemy)
        {
            viewenemyflg = true;

            e_hpbarSlider viewcameraflg = gameObject.GetComponent<e_hpbarSlider>();
            viewcameraflg.view_ENEMYflg = true;
        }
        else {
            viewenemyflg = false;

            e_hpbarSlider viewcameraflg = gameObject.GetComponent<e_hpbarSlider>();
            viewcameraflg.view_ENEMYflg = false;
        }

        renderflg = false;
    }

    //Rendererついているオブジェの判定(カメラに写っているかどうか)
    void OnWillRenderObject()
    {
        if (Camera.current.tag == "MainCamera") {
            renderflg = true;
        }
    }

}