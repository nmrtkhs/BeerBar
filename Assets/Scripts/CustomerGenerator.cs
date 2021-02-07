using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CustomerGenerator : MonoBehaviour
{
    public GameObject[] customers;
    float span = 3.0f;
    float delta = 0;
    GameObject[] realCustomers;
    void Start()
    {
      realCustomers = new GameObject[3];
    }
    // Update is called once per frame
    void Update()
    {
      this.delta += Time.deltaTime;
      if (this.delta > this.span)
      {
        // 客がテーブルにいるかどうかをチェック
        List<int> checkCustomers = new List<int>();
        for (var i = 0; i < realCustomers.Length; ++i) {
          if (realCustomers[i] == null) {
            checkCustomers.Add(i);
          }
        }
        // いないテーブルに対して客を生成する
        if (checkCustomers.Any()) {
          int tableNo = checkCustomers[ Random.Range(0, checkCustomers.Count) ];
          realCustomers[tableNo] = Instantiate(customers[tableNo]) as GameObject;
          this.delta = 0;
          realCustomers[tableNo].transform.position = new Vector3(tableNo * 5.0f - 6.5f, -3.4f, 0);
        }
        else {
          this.delta = 0; //deltaを0に戻す
        }
      }
    }
}