   using UnityEngine;
   using System.Collections;

public class test : MonoBehaviour
{
    #region 変数
    public GameObject bulletPrefab;//飛ばすオブジェクト変数
    public float speed; //球の速度
    RaycastHit hit;//レイがヒットした座標
    #endregion

    void Update () {
        transform.Rotate(new Vector3(0,1,0),5*Time.deltaTime); //カメラ回転（y軸に対して）
        if (Input.GetButtonDown("Fire1"))//左クリックされたら
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);//オブジェクトのインスタンス化
            bullet.transform.position = transform.position;//オブジェクトの座標をアタッチしているオブジェクト（カメラ）の座標に合わせる
            Ray ray = new Ray(transform.position,transform.forward); //カメラ座標から前方向にレイを飛ばす
           // Vector3 direction = (transform.position + Vector3.forward - transform.position).normalized;
           // bullet.GetComponent<Rigidbody>().velocity = direction * 10;
            bullet.GetComponent<Rigidbody>().velocity = (ray.GetPoint(100) - transform.position).normalized * speed; //getpointで１００ｍ地点からカメラ座標を引いて，距離（方向）を出して正規化した後に任意の速度をかける
        }
	}       
}

/*参考にしたサイト
 * http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q14134312524 rayとveloctiyを使った方向の指定方法
 * http://spi8823.hatenablog.com/entry/2015/05/31/025903　回転
*/