﻿using UnityEngine;
using System.Collections;

public class OldManCreator : MonoBehaviour{

    public GameObject obj; // キャラクターのプレハブを格納する
    public float interval; // キャラクターを生成する間隔
    private float time; // 経過時間を計る
    private int count; // キャラクター生成数をカウントする
    public int maxpops; // キャラクター生成数

	void Start(){

        time = interval;
    }

	void Update(){

        time += Time.deltaTime;

        if(time >= interval && count < maxpops){

            time = 0;

            GameObject oldman = Instantiate(obj);
            oldman.transform.localPosition = new Vector3(Random.Range(-4f, 4f), -4f,Random.Range(20f,30f));

            count++;
        }
    }
}
