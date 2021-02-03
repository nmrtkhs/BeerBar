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
    public Sprite defaultSprite;
    float elapsedtime = 0;
    float timeLimit = 3;
    private bool isEmpty = true;
    private bool isHalf = false;
    private bool isFull = false;
    [SerializeField]
    private int BeerType;
    private Vector3 defaultPos;
    float resetTime = 0f;
    const float resetLimit = 1.0f;
    bool isReset = false;
    private GameDirector gameDirector;
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        defaultPos = transform.position;
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (BeerType)
        {
            case 1:// ピルスナー
                Debug.Log(BeerType);
                half = Resources.Load<Sprite>("pilsner.half");
                full = Resources.Load<Sprite>("pilsner.full");
                break;
            case 2:// フルーツビア
                half = Resources.Load<Sprite>("fruitbeer.half");
                full = Resources.Load<Sprite>("fruitbeer.full");
                break;
            case 3://　ラガー
                half = Resources.Load<Sprite>("lager.half");
                full = Resources.Load<Sprite>("lager.full");
                break;
            case 4:// エール
                half = Resources.Load<Sprite>("ale.half");
                full = Resources.Load<Sprite>("ale.full");
                break;
            case 5:// スタウト
                half = Resources.Load<Sprite>("stout.half");
                full = Resources.Load<Sprite>("stout.full");
                break;
            case 6:// ヴァイツェン
                half = Resources.Load<Sprite>("weizen.half");
                full = Resources.Load<Sprite>("weizen.full");
                break;
            default:
                break;
        }
    }

    void OnMouseDown()
    {
        if (isReset) return;
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position); //オブジェクトのポジションからスクリーン座標を取りたい時に使用する
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //ドラッグする時にオブジェクトがカーソルに自動でセンタリングしないようにする。マウスの位置とオブジェクトの基準点の差をoffsetに代入し、ドラッグが始まったらオフセットする。
    }

    void OnMouseDrag()
    {
        if (isReset) return;
        if (!isHalf)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); //ドラッグ時のマウス位置をスクリーン座標に変換する
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset; //上記にクリックした場所の差を足すことによって、オブジェクトを移動する座標位置を求める
            transform.position = currentPosition; //オブジェクトの位置を変更する
        }
    }

    void Update()
    {
        if (isReset)
        {
            resetTime += Time.deltaTime;
            if (resetTime > resetLimit)
            {
                isReset = false;
            }
        }
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
            string strBeerType = BeerType.ToString();
            if (other.gameObject.name == strBeerType)
            {
                MainSpriteRenderer.sprite = half;
                isEmpty = false;
                isHalf = true;
            } else {
                transform.position = defaultPos;
                isReset = true;
                resetTime = .0f;
            }
        }

        if (isFull)
        {
            var go = other.gameObject;
            if (go.name.Contains("customer"))
            {
                if (go.GetComponent<CustomerController1>().currentOrder + 1 == BeerType)
                {
                    // 正解
                    gameDirector.ScoreUp();
                }
                else
                {
                    // 不正解
                    gameDirector.ScoreDown();
                }
                Destroy(go);
                // 初期化
                MainSpriteRenderer.sprite = defaultSprite;
                transform.position = defaultPos;
                isFull = false;
                isEmpty = true;
                isReset = true;
                resetTime = .0f;
                elapsedtime = .0f;
            }
        }

    }
}
