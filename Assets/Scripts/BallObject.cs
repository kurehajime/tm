using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    [SerializeField]
    public Renderer renderer;

    [SerializeField]
    public bool isTouch = false;

    [SerializeField]
    public GameResources.BallColor color;

    // Start is called before the first frame update
    void Start()
    {
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            GetComponent<BallObject>().renderer.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0f));
        }
        else
        {
            GetComponent<BallObject>().renderer.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0f));
        }
    }
    void OnCollisionEnter(Collision other)
    {
    }
    public void ChangeColor()
    {
        switch (color)
        {
            case GameResources.BallColor.red:
                GetComponent<BallObject>().renderer.material.SetColor("_Color", Color.red);
                break;
            case GameResources.BallColor.blue:
                GetComponent<BallObject>().renderer.material.SetColor("_Color", Color.blue);
                break;
            case GameResources.BallColor.green:
                GetComponent<BallObject>().renderer.material.SetColor("_Color", Color.green);
                break;
            case GameResources.BallColor.purple:
                GetComponent<BallObject>().renderer.material.SetColor("_Color", new Color(1, 0, 1));
                break;
        }
    }
}
