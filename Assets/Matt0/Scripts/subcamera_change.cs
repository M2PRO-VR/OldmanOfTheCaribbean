using UnityEngine;
using System.Collections;

public class subcamera_change : MonoBehaviour {
	Camera subcamera;
	//naver used: Camera maincamera;
	//naver used: Ray ray;
	//naver used: RaycastHit raycashit;
	Transform unitychan;
	Transform original_maincamera;
	Transform lookatposition;
	//naver used: float startcameraposition_y;
	//naver used: float startcameraposition_x;
    //naver used: float startcameraposition_z;
	public LayerMask mask;
    //naver used: Vector3 offset;
	Vector3 temp;
	public static bool rotateNo=false;
	// Use this for initialization
	void Start () {
		//naver used: maincamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		unitychan = GameObject.Find("unitychan").GetComponent<Transform> ();
		original_maincamera = GameObject.Find ("original_maincamera").GetComponent<Transform> ();
		lookatposition = GameObject.Find ("LookatPosition").GetComponent<Transform> ();
		//naver used: startcameraposition_y = transform.position.y;
		//naver used: startcameraposition_x = transform.position.x;
        //naver used: startcameraposition_z = transform.position.z;
        //naver used: offset = transform.position - unitychan.position;

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (lookatposition.position);
		float dis = Vector3.Distance (transform.position, unitychan.position);
		temp = transform.position;
		//naver used: ray = new Ray (transform.position, transform.forward);
        //	Debug.DrawRay (transform.position, transform.forward * 6, Color.red);

        /*naver used: if (Physics.Raycast(ray, out raycashit, 5f, mask.value) && RideOnBuggy.buggy_on_ride == false) {

				transform.position += (transform.up * 3f * Time.deltaTime);
				transform.LookAt (lookatposition.position);

		}else{
			if (transform.localPosition.y > startcameraposition_y) {
			transform.position -= (transform.up * 3f *Time.deltaTime);
				transform.LookAt (lookatposition.position);
		
			}
		
		}*/
		if(dis > 8.5f){
			temp.x = original_maincamera.position.x;
			temp.z = original_maincamera.position.z;
			temp.y = original_maincamera.position.y;
			transform.position = temp;
			transform.LookAt (lookatposition.position);
		}
	}
}
