using UnityEngine;
using System.Collections;
using UnityEngine.VR;
public class OculusCameraPositionAdjust : MonoBehaviour {

    private GameObject Parent;
    private GameObject Player;
    // Use this for initialization
    void Start () {
        Debug.Log("VRDevice.model = " + VRDevice.model);
        Debug.Log("VRSettings.loadedDeviceName = " + VRSettings.loadedDeviceName);

        Parent = gameObject.transform.parent.gameObject; //LMHeadMountedRig
        Player = GameObject.FindGameObjectWithTag("Player");
        //別視点の映像を表示する↓方法
        //UnityEngine.VR.VRSettings.showDeviceView = false;
    }

    // Update is called once per frame
    void Update() {
        //0ボタンでカメラの位置トラッキングをリセットする
        if (Input.GetKeyDown(KeyCode.Alpha0) && (VRDevice.isPresent)) {
            InputTracking.Recenter();
        }

        transform.position = new Vector3(Parent.transform.position.x, Parent.transform.position.y, Parent.transform.position.z);        
        transform.rotation = InputTracking.GetLocalRotation(VRNode.CenterEye);
    }
}
