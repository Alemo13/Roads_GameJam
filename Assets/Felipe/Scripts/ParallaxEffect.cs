using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0f;
    
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - lastCameraPosition.x) * parallaxMultiplier;
        transform.Translate(new Vector3(deltaX, 0, 0));
        lastCameraPosition = cameraTransform.position;
    }
}
