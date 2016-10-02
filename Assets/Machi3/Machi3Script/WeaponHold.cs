using UnityEngine;
using System.Collections;

public class WeaponHold : MonoBehaviour {

    public GameObject Knife;
    private GameObject KnifeCopy;
    private bool instantiateflg = false;

    public static bool Trackflg = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (instantiateflg == true && Trackflg == true)
        {
            KnifeCopy = (GameObject)Instantiate(Knife, transform.position, transform.rotation);
            instantiateflg = false;
        }

        if(Trackflg == true)
        {
            KnifeCopy.transform.position = transform.position;
            KnifeCopy.transform.eulerAngles = transform.transform.eulerAngles + new Vector3(180,0,0);
            KnifeCopy.transform.position = KnifeCopy.transform.localPosition + new Vector3(0, -0.05f, 0);
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
