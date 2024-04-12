using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier, offsetY;

    private Transform cameraTransform;
    private float spriteWidth, startPosition;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cameraTransform = Camera.main.transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = cameraTransform.position.x * parallaxMultiplier;
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultiplier);

        // Infinite Scroll, left and right
        if (moveAmount > startPosition + spriteWidth)
        {
            startPosition += spriteWidth;
        }
        else if (moveAmount < startPosition - spriteWidth)
        {
            startPosition -= spriteWidth;
        }

        transform.position = new Vector3(startPosition + deltaX, transform.position.y, transform.position.z);
    }
}
