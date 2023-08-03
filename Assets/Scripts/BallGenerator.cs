using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class BallGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject ballObj;

    int cnt = 0;
    const int MAXCNT = 60;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cnt++;
        cnt %= MAXCNT;
        if (cnt == 0)
        {
            GameObject gameObject = Instantiate(ballObj);
            gameObject.transform.parent = this.transform;
            gameObject.transform.localPosition = Vector3.zero;
            // カラーをランダムに設定
            gameObject.GetComponent<BallObject>().color = Enum.GetValues(typeof(GameResources.BallColor)).Cast<GameResources.BallColor>().ToList()[UnityEngine.Random.Range(0, 4)];

            // 角度を付けてボールを落とす
            gameObject.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60.0f, 60.0f)) * Vector3.down * 10f, ForceMode.Impulse);
        }
    }
}
