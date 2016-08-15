using UnityEngine;
using System.Collections;

public class subcamera_change : MonoBehaviour {
	Camera subcamera;
	Camera maincamera;
	Ray ray;
	RaycastHit raycashit;
	Transform unitychan;
	Transform original_maincamera;
	Transform lookatposition;
	float startcameraposition_y;
	float startcameraposition_x;
	float startcameraposition_z;
	public LayerMask mask;
	Vector3 offset;
	Vector3 temp;
	public static bool rotateNo=false;
	// Use this for initialization
	void Start () {
		maincamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		unitychan = GameObject.Find("unitychan").GetComponent<Transform> ();
		original_maincamera = GameObject.Find ("original_maincamera").GetComponent<Transform> ();
		lookatposition = GameObject.Find ("LookatPosition").GetComponent<Transform> ();
		startcameraposition_y = transform.position.y;
		startcameraposition_x = transform.position.x;
		startcameraposition_z = transform.position.z;
		offset = transform.position - unitychan.position;

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (lookatposition.position);
		float dis = Vector3.Distance (transform.position, unitychan.position);
		temp = transform.position;
		ray = new Ray (transform.position, transform.forward);
        //	Debug.DrawRay (transform.position, transform.forward * 6, Color.red);

        if (Physics.Raycast(ray, out raycashit, 5f, mask.value) && RideOnBuggy.buggy_on_ride == false) {

				transform.position += (transform.up * 3f * Time.deltaTime);
				transform.LookAt (lookatposition.position);

		}else{
			if (transform.localPosition.y > startcameraposition_y) {
			transform.position -= (transform.up * 3f *Time.deltaTime);
				transform.LookAt (lookatposition.position);
		
			}
		
		}
		if(dis > 8.5f){
			temp.x = original_maincamera.position.x;
			temp.z = original_maincamera.position.z;
			temp.y = original_maincamera.position.y;
			transform.position = temp;
			transform.LookAt (lookatposition.position);
		}
	}
}
