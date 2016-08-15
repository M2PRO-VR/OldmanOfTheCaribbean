using UnityEngine;
using System.Collections;

public class clearobject : MonoBehaviour {
	public GameObject Clearobject;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Clearobject.name == "clearobject1t" || Clearobject.name == "clearobject1_1t") {
			transform.Rotate (new Vector3 (0f, 3f, 0f));
		}else if (Clearobject.name == "clearobject2t" || Clearobject.name == "clearobject1_1t2") {
			transform.Rotate (new Vector3 (0f, -3f, 0f));
		}else if(Clearobject.name == "clearobject3"){
			transform.Rotate (new Vector3(0f, 3f, 0f));
		}else if(Clearobject.name == "clearobject4"){
			transform.Rotate (new Vector3(0f, -3f, 0f));
		}else if(Clearobject.name == "clearobject5"){
			transform.Rotate (new Vector3(0f, -3f, 0f));
		}else if(Clearobject.name == "clearobject6"){
			transform.Rotate (new Vector3(0f, 3f, 0f));

		}else if(Clearobject.name == "clearobject7"){
			transform.Rotate (new Vector3(0f, -3f, 0f));
		}else if(Clearobject.name == "clearobject8"){
			transform.Rotate (new Vector3(0f, 3f, 0f));
		}
	}
}
