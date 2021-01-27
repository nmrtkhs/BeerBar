using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    SpriteRenderer MainSpriteRenderer;
    public Sprite half;
    public Sprite full;
    float elapsedtime=0;
    float timeLimit = 3;
    private bool isEmpty = true;
    private bool isHalf = false;
    private bool isFull = false;

    void Start()
    {

        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position); //オブジェクトのポジションからスクリーン座標を取りたい時に使用する
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); 
        //ドラッグする時にオブジェクトがカーソルに自動でセンタリングしないようにする。マウスの位置とオブジェクトの基準点の差をoffsetに代入し、ドラッグが始まったらオフセットする。
    }
  
    void OnMouseDrag()
    {
         if (!isHalf)
        {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); //ドラッグ時のマウス位置をスクリーン座標に変換する
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;　//上記にクリックした場所の差を足すことによって、オブジェクトを移動する座標位置を求める
        transform.position = currentPosition; //オブジェクトの位置を変更する
        }
    }

    void Update()
    {  
       if (isHalf)
       {   
       elapsedtime += Time.deltaTime; //経過時間
        if (timeLimit < elapsedtime)
        {
            MainSpriteRenderer.sprite = full;
            isHalf = false;
            isFull = true;
        }
       }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEmpty)
        {
            MainSpriteRenderer.sprite = half;
            isEmpty = false;
            isHalf = true;
        }
        
        if (isFull)
        {
           Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "customer1(Clone)")
            {
                Destroy(gameObject);
            }
        }
       
    }    
}
