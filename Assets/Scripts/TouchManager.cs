using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> touchBallList;
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
                    h[0].collider.GetComponent<BallObject>().isTouch = true;
                    touchBallList.Add(h[0].collider.gameObject);
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
                    if (h[0].collider.tag == "Ball"
                    && !h[0].collider.GetComponent<BallObject>().isTouch)
                    {
                        h[0].collider.GetComponent<BallObject>().isTouch = true;
                        touchBallList.Add(h[0].collider.gameObject);
                    }
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
                Destroy(go);
            }
        }
        touchBallList.Clear();
    }
}
