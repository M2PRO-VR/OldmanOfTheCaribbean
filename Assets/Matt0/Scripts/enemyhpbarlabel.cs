using UnityEngine;
using System.Collections;


public class enemyhpbarlabel : MonoBehaviour{


    // target, targetオブジェクトの名前
    public GameObject GUIWatchPoint, NameObject;
    // target_Name_Chaser_Object, プレイヤーオブジェクト
    private GameObject /*naver used: NameChaserObject,*/ PlayerObj; 
    // オブジェクトのワールド座標, オブジェクトのちょっと上の座標, オブジェクトのスクリーン座標
    //naver used: private Vector3 ObjectPoint, ObjectPointPlus, GUIScreenPoint;
    // プレイヤーとの距離, 敵の名前見える距離
    private float distance_enemy, view_enemy = 30/*naver used:, ScreenHeight = Screen.height*/;
    // 敵距離で表示フラグ, カメラに写っているかのフラグ
    private bool viewenemyflg = false, renderflg = false;


    void Start(){

        //naver used: NameChaserObject = GUIWatchPoint.gameObject.transform.FindChild("TargetNameChaser").gameObject;
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
    }


    void OnGUI(){

        if(renderflg == true && viewenemyflg == true){

            // GUIの色変更等
            GUIStyle guistyle = new GUIStyle();
            GUIStyleState stylestate = new GUIStyleState();
            stylestate.textColor = Color.red;
            guistyle.normal = stylestate;

            // 名前表示
            // Rect(position, "text", style)入力　←　座標等
            // GUI.Label(new Rect(GUIScreenPoint.x - 60, ScreenHeight - GUIScreenPoint.y, 120, 40), NameObject.gameObject.name, guistyle);
        }
    }


    void Update(){

        //naver used: ScreenHeight = Screen.height;

        // Objectの座標を代入
        // 名前表示したいオブジェクトの子要素にあるTargetNameChaserの座標を取得して表示
        //naver used: ObjectPoint = NameChaserObject.transform.position;
        //naver used: ObjectPointPlus = new Vector3(0, 0, 0) + ObjectPoint;

        //naver used: GUIScreenPoint = Camera.main.WorldToScreenPoint(ObjectPointPlus);

        distance_enemy = Vector3.Distance(PlayerObj.transform.position, GUIWatchPoint.transform.position);

        if (distance_enemy <= view_enemy){

            viewenemyflg = true;

            e_hpbarSlider viewcameraflg = gameObject.GetComponent<e_hpbarSlider>();
            viewcameraflg.view_ENEMYflg = true;

        }else{
            
            viewenemyflg = false;

            e_hpbarSlider viewcameraflg = gameObject.GetComponent<e_hpbarSlider>();
            viewcameraflg.view_ENEMYflg = false;
        }

        renderflg = false;
    }

    // Rendererついているオブジェの判定(カメラに写っているかどうか)
    void OnWillRenderObject(){

        if(Camera.current.tag == "MainCamera"){
            renderflg = true;
        }
    }
}