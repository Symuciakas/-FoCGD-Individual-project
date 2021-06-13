using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovementScript : MonoBehaviour
{
    private float length, startPos, currentPos;
    public float moveVal;
    void Start()
    {
        startPos = transform.position.x;
        currentPos = startPos;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = currentPos + moveVal;
        transform.position = new Vector3(currentPos, transform.position.y, transform.position.z);
        if (startPos - currentPos >= length)
        {
            currentPos = startPos;
        }
    }
}