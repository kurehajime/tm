using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> touchBallList;

    [SerializeField]
    GameObject deleteEffectObj;

    [SerializeField]
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            touchBallList = new List<GameObject>();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            var h = Physics.RaycastAll(ray, 100.0f);
            if (h.Length > 0)
            {
                //タッチしたボールが選択状態でないとき
                if (h[0].collider.tag == "Ball" && !h[0].collider.GetComponent<BallObject>().isTouch)
                {
                    if (h[0].collider.GetComponent<BallObject>().color == GameResources.BallColor.bomb)
                    {
                        //爆発！
                        h[0].collider.GetComponent<BallObject>().Explosion(deleteEffectObj);
                    }
                    else
                    {
                        h[0].collider.GetComponent<BallObject>().isTouch = true;
                        touchBallList.Add(h[0].collider.gameObject);
                    }
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (touchBallList.Count != 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                var h = Physics.RaycastAll(ray, 100.0f);
                if (h.Length > 0)
                {
                    // 「ボールである」かつ「選択したことのあるボールでない」かつ「最初にタッチしたボールの色と同じ」とき実行
                    if (h[0].collider.tag == "Ball"
                        && !h[0].collider.GetComponent<BallObject>().isTouch
                        && touchBallList[0].GetComponent<BallObject>().color == h[0].collider.GetComponent<BallObject>().color)
                    {
                        h[0].collider.GetComponent<BallObject>().isTouch = true;
                        touchBallList.Add(h[0].collider.gameObject);
                    }
                    // 「ボールである」かつ「最初にタッチしたボールの色とは違う」ときReleaseObject関数を実行
                    else if (h[0].collider.tag == "Ball"
                        && touchBallList[0].GetComponent<BallObject>().color != h[0].collider.GetComponent<BallObject>().color)
                    {
                        ReleaseObject();
                    }
                }
                else
                {
                    //ボールをタッチしていない時は消去判定を行う
                    ReleaseObject();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
    }
    public void ReleaseObject()
    {
        var cnt = touchBallList.Count;
        //離したらマテリアルの色を戻す
        foreach (GameObject go in touchBallList)
        {
            //選択状態を解除
            go.GetComponent<BallObject>().isTouch = false;
            //3個以上なら消す
            if (cnt >= 3)
            {
                GameObject delObj = Instantiate(deleteEffectObj);
                delObj.transform.position = go.transform.position;
                Destroy(go);
            }
        }
        touchBallList.Clear();
        if (cnt >= 3)
        {
            scoreManager.AddScore((int)Mathf.Pow(2, cnt));
        }
    }
}
