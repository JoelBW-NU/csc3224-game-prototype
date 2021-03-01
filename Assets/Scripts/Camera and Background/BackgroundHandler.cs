using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer background = GetComponent<SpriteRenderer>();

        float backgroundWidth = background.sprite.bounds.size.x;
        float backgroundHeight = background.sprite.bounds.size.y;

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = (screenHeight * Screen.width / Screen.height);

        transform.localScale = new Vector3(screenWidth / backgroundWidth, screenHeight / backgroundHeight, 0);
    }
}
