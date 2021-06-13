using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiController : MonoBehaviour
{
    private float lastCloudHeight;
    private float movementSpeed = 3f;
    public Rigidbody2D chibiRb;
    private Vector2 directions;
    public Animator chibiAnimator;
    private AudioSource battleMusic;
    private SaveManager sm = new SaveManager();

    public bool characterEnabled;

    private void Start()
    {
        characterEnabled = false;
        battleMusic = GetComponent<AudioSource>();
        battleMusic.volume = sm.GetVolume();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            directions.y = Input.GetAxisRaw("Vertical");
            directions.x = 0;
        }
        else
        {
            directions.x = Input.GetAxisRaw("Horizontal");
            directions.y = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            float f = sm.GetVolume();
            if (f < 1f)
            {
                f = f + 0.1f;
            }
            sm.SetVolume(f);
            battleMusic.volume = sm.GetVolume();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            float f = sm.GetVolume();
            if (f > 0f)
            {
                f = f - 0.1f;
            }
            sm.SetVolume(f);
            battleMusic.volume = sm.GetVolume();
        }
    }

    private void FixedUpdate()
    {
        if (characterEnabled)
        {
            directions.Normalize();
            chibiRb.MovePosition(chibiRb.position + directions * movementSpeed * Time.fixedDeltaTime);

            chibiAnimator.SetFloat("Horizontal", directions.x);
            chibiAnimator.SetFloat("Vertical", directions.y);
            chibiAnimator.SetFloat("Speed", directions.sqrMagnitude);
        }
    }

    public void EnableDelayed(float f)
    {
        StartCoroutine(ThawDelayed(f));
    }

    IEnumerator ThawDelayed(float f)
    {
        yield return new WaitForSeconds(f);
        SetEnabled(true);
    }

    public void SetEnabled(bool b)
    {
        characterEnabled = b;
        if (!b)
        {
            chibiAnimator.SetFloat("Horizontal", 0);
            chibiAnimator.SetFloat("Vertical", 0);
            chibiAnimator.SetFloat("Speed", 0);
            chibiRb.velocity = new Vector2(0f, 0f);
        }
    }
}