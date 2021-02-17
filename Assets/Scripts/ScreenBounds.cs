using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D screenDetector = GetComponent<BoxCollider2D>();

        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = (screenHalfHeight * Screen.width / Screen.height);  

        screenDetector.size = new Vector2(screenHalfWidth * 2, screenHalfHeight * 2);
    }

}
