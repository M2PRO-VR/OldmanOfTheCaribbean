using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cooltimebar2 : MonoBehaviour {
	public Image cooltime2;
	public static bool ctStart2=false;
	public static bool cooltime2Now=false;
	float addValue = 0f;
	float alpha = 0f;

	//cooltimeNow が　true　のときはクールタイムに入ってる
	//cooltimeNow が　false のときにスキルを使えるようにすれば大丈夫
	// Use this for initialization
	void Start () {
		cooltime2 = GameObject.Find ("cooltimebar2").GetComponent<Image> ();
		ctStart2 = false;
		cooltime2.fillAmount = 1f;
		cooltime2Now = false;
		cooltime2.enabled = false;

	}

	// Update is called once per frame
	void Update () {
		if(ctStart2){
			cooltime2.enabled = true;
			changePicturesUpdate ();
		}

	}
	//黒い球のクールタイム
	void changePicturesUpdate(){
		cooltime2Now = true;
		alpha = cooltime2.fillAmount;
		addValue = Time.deltaTime/7f ;
		alpha -= addValue;
		cooltime2.fillAmount = alpha;

		if(cooltime2.fillAmount <= 0){
			ctStart2 = false;
			cooltime2.fillAmount = 1;
			cooltime2Now = false;
			cooltime2.enabled = false;
		}
	}


	public static void cooltime2_start(bool start){
		ctStart2 = start;
	}
}
