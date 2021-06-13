using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def2Controller : MonoBehaviour
{
    private float lastCloudHeight;
    private float movementSpeed = 3f;
    public Rigidbody2D rb;
    private Vector2 directions;
    private bool canJump;
    void Start()
    {
        canJump = true;
        rb = FindObjectOfType<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (transform.position.y + 8f > lastCloudHeight)
        {
            print("Generate at " + lastCloudHeight);
            lastCloudHeight = lastCloudHeight + 4f;
            GameObject SunNClouds1 = (GameObject)Instantiate(Resources.Load("CutsceneObjects/SunNClouds"), new Vector3(0, lastCloudHeight, 0), transform.rotation);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*movementSpeed, rb.velocity.y);
            if(Input.GetAxisRaw("Vertical") != 0 && canJump)
            {
                canJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 10f);
                StartCoroutine(enableDelayed());
            }
        }
    }

    IEnumerator enableDelayed()
    {
        yield return new WaitForSeconds(2f);
        canJump = true;
    }
}
