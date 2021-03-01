﻿using System.Collections;
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
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                grapple.Pull(gameObject);
                grappled = true;
                countdownBegun = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                grapple.Swing(gameObject);
                grappled = true;
                countdownBegun = true;
            }

            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        }       
    }

    void OnMouseExit()
    {
        if (Time.timeScale != 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
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
