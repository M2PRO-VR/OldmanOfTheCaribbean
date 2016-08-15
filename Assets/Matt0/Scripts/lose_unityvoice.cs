using UnityEngine;
using System.Collections;

public class lose_unityvoice : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip lose_voice;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		audiosource.PlayOneShot (lose_voice);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
