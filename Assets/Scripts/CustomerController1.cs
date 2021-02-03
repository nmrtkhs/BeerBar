﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerController1 : MonoBehaviour
{
    public GameObject[] orders;
    public GameObject customer1;
    float span = 3.0f;
    float delta = 0;
    GameObject[] realOrders;
    bool isOrderFinished = false;
    public int currentOrder;
    GameObject currentOrderImage;
    void Start()
    {
        realOrders = new GameObject[6];
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            List<int> checkCustomers = new List<int>(); //Listの宣言
            for (var i = 0; i < realOrders.Length; ++i)
            {
                if (realOrders[i] == null)
                    checkCustomers.Add(i);
            }
        }
        {
            this.delta += Time.deltaTime;
            if (!isOrderFinished && this.delta > span)
            {
                currentOrder = Random.Range(0, orders.Length);
                currentOrderImage = Instantiate(orders[currentOrder]) as GameObject; //プレハブをランダムに生成する
                Vector2 customer1 = transform.position; //客の位置を取得する
                currentOrderImage.transform.position = new Vector3(transform.position.x + 2.2f, transform.position.y, transform.position.z); //生成したプレハブを客の隣に配置する
                isOrderFinished = true;
            }
        }

    }

    void OnDestroy()
    {
      Destroy(currentOrderImage);
    }
}
