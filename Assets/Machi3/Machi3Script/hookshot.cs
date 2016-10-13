using UnityEngine;
using System.Collections;

public class hookshot : MonoBehaviour {

    public GameObject BEAM;
    private ParticleSystem ps;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //shot
        GameObject beam = (GameObject)Instantiate(BEAM, transform.position, transform.rotation);
        ps = beam.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
        ps.Play();
        Destroy(beam, 5f);
        /*Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //カメラ座標から前方向にレイを飛ばす
        GetComponent<Rigidbody>().velocity = (ray.GetPoint(100) - transform.position).normalized * 5; //getpointで１００ｍ地点からカメラ座標を引いて，距離（方向）を出して正規化した後に任意の速度をかける
        transform.LookAt(ray.GetPoint(100));*/

        gameObject.SetActive(false);
    }
}
