using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {
    //ボタン押すと生成するスクリプト
    public GameObject bomb;

    private GameObject bombCopy;
    private GameObject hand_palm;
    private bool instantiateflg = false;

    // Use this for initialization
    void Start () {
        hand_palm = GameObject.Find("RigidRoundHand_R").gameObject.transform.FindChild("palm").gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        bombCopy = (GameObject)Instantiate(bomb, hand_palm.transform.position + new Vector3(0,0,0.3f), hand_palm.transform.rotation);
        gameObject.SetActive(false);
	}
}
