using UnityEngine;
using System.Collections;

public class WarpStar : MonoBehaviour {

    public GameObject warp_position; //ワープ先
    private GameObject Player; //Playerオブジェクト
    private float intervalTime;
    private float set_TIME = 1.5f;
    public GameObject Warp_effect; //ワープエフェクト
    private GameObject effect;  //ワープエフェクト格納

    public static bool cant_Warpflg = false;  //敵感知するとワープできなくなる(WarpAwareEnemy.csより受け取り)

    private bool emergence_flg = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider hit)
    {
            //接触対象はPlayerタグですか？
            if (hit.CompareTag("Player"))
            {
                Player = hit.gameObject;
                intervalTime += Time.deltaTime;

                //Warp_effect.gameObject.SetActive(true);
                //Warp_effect.GetComponent<EffekseerEmitter>().enabled = true;
                if (emergence_flg == false)
                {
                    effect = (GameObject)Instantiate(Warp_effect, gameObject.transform.position + new Vector3(0, 0.5f, 0), gameObject.transform.rotation);
                    emergence_flg = true;
                }
                if (intervalTime >= set_TIME)
                {
                    Warp();
                    //Warp_effect.gameObject.SetActive(false);
                    //Warp_effect.GetComponent<EffekseerEmitter>().enabled = false;
                    intervalTime = 0.0f;
                }
            }
    }

    void OnTriggerExit(Collider hit)
    {
            if (hit.CompareTag("Player"))
            {
                intervalTime = 0.0f;
                //Warp_effect.gameObject.SetActive(false);
                //Warp_effect.GetComponent<EffekseerEmitter>().enabled = false;
                Destroy(effect.gameObject);
                emergence_flg = false;
            }
    }

    private void Warp()
    {
        Player.transform.position = warp_position.gameObject.transform.position;
    }
}
