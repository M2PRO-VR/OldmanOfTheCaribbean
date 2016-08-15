using UnityEngine;
using System.Collections;

public class DiedColor : MonoBehaviour {

    public bool diedcolor_flg = false; //WeaponAttackのDIEDによって切換
    private float intervalTime;
    private float set_DIETIME = 2.0f;
    private Color ChangeColor;  //色変更用

    public float R = 0.005f; //red
    public float G = 0.0005f; //green
    public float B = 0.0005f; //blue
    public float A = 0; //Transparency(透明度)

    // Use this for initialization
    void Start () {
        ChangeColor = new Color(R, G, B, A);
	}
	
	// Update is called once per frame
	void Update () {
	    if(diedcolor_flg == true)
        {
            intervalTime += Time.deltaTime;

            gameObject.GetComponent<Renderer>().material.color -= ChangeColor;
            //Debug.Log(gameObject.GetComponent<Renderer>().material.color);

            if (intervalTime >= set_DIETIME)
            {
                diedcolor_flg = false;
                intervalTime = 0.0f;
            }
        }
	}

}
