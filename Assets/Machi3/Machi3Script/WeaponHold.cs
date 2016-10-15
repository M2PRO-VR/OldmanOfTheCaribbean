using UnityEngine;
using System.Collections;

public class WeaponHold : MonoBehaviour {

    public GameObject Knife;
    public GameObject Knife2;
    private GameObject KnifeCopy;
    private bool turnflg = false; //順番に投げる
    private bool instantiateflg = false;

    public static bool Trackflg = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (instantiateflg == true && Trackflg == true)
        {
            if (turnflg == false)
            {
                KnifeCopy = (GameObject)Instantiate(Knife, transform.position, transform.rotation);
                turnflg = true;
            }
            else if(turnflg == true)
            {
                KnifeCopy = (GameObject)Instantiate(Knife2, transform.position, transform.rotation);
                turnflg = false;
            }
            instantiateflg = false;
        }

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "palm" && Trackflg == false)
        {
            instantiateflg = true;
            Trackflg = true;
        }

    }

}
