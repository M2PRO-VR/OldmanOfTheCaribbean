using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			HpBarCtrl.kirikae = false;
			SceneManager.LoadScene ("Win");
		}

	}
}
