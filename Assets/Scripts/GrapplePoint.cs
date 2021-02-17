using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [HideInInspector]
    public Grapple grapple;

    [HideInInspector]
    public bool grappled = false;

    [HideInInspector]
    public bool countdownBegun = false;

    [SerializeField]
    float grappleLifetime = 3;

    float elapsedTime = 0;

    [SerializeField]
    Text countdownText;

    void Start()
    {
        countdownText.text = "";
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            grapple.GrappleToPoint(gameObject);
            grappled = true;
            countdownBegun = true;
        }

        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    void Update()
    {
        if (countdownBegun)
        {
            elapsedTime += Time.deltaTime;
            countdownText.text = ((int) (grappleLifetime - elapsedTime) + 1).ToString();
        }
        
        if (elapsedTime >= grappleLifetime)
        {
            DestroyPoint();
        }
    }

    public void DestroyPoint()
    {
        if (grappled)
        {
            grapple.Ungrapple();
        }
        Destroy(gameObject);
    }
}
