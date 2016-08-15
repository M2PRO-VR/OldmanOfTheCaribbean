using UnityEngine;
using System.Collections;

public class car : MonoBehaviour {
	public GameObject effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Enemy"){
		GameObject effect_start = (GameObject)Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
			other.gameObject.SendMessage ("DamageProcess", 200f);
		Destroy (this.gameObject);
		}
	}
}
