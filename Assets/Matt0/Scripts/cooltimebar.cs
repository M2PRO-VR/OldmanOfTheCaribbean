using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cooltimebar : MonoBehaviour {
	public Image cooltime;
	public static bool ctStart=false;
	public static bool cooltimeNow=false;
	float addValue = 0f;
	float alpha = 0f;

	//cooltimeNow が　true　のときはクールタイムに入ってる
	//cooltimeNow が　false のときにスキルを使えるようにすれば大丈夫
	// Use this for initialization
	void Start () {
		cooltime = GameObject.Find ("cooltimebar").GetComponent<Image> ();
		ctStart = false;
		cooltime.fillAmount = 1f;
		cooltimeNow = false;
		cooltime.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(ctStart){
			cooltime.enabled = true;
			changePicturesUpdate ();
		}
	
	}
	//黒い球のクールタイム
	void changePicturesUpdate(){
		cooltimeNow = true;
		alpha = cooltime.fillAmount;
		addValue = Time.deltaTime/15f ;
		alpha -= addValue;
		cooltime.fillAmount = alpha;

		if(cooltime.fillAmount <= 0){
			ctStart = false;
			cooltime.fillAmount = 1;
			cooltimeNow = false;
			cooltime.enabled = false;
		}
	}

		
	public static void cooltime_start(bool start){
		ctStart = start;
	}

}
