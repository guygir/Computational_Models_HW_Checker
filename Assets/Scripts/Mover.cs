using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 target=new Vector3(0,0,-10);
    [SerializeField] float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    //private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget+(target- transform.position);
    }


}
