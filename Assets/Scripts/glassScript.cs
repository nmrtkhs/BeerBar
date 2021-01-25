using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position); //オブジェクトのポジションからスクリーン座標を取りたい時に使用する
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); 
        //ドラッグする時にオブジェクトがカーソルに自動でセンタリングしないようにする。マウスと位置とオブジェクトの基準点の差をoffsetに代入し、ドラッグが始まったらオフセットする。
    }
  
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); //ドラッグ時のマウス位置をスクリーン座標に変換する
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;　// //上記にクリックした場所の差を足すことによって、オブジェクトを移動する座標位置を求める
        transform.position = currentPosition; //オブジェクトの位置を変更する
    }
}
