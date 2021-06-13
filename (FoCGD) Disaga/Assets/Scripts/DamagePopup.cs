using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public float disapearTimer;
    Color c = new Color();

    private void Update()
    {
        transform.position += new Vector3(0, 2f * Time.deltaTime);

        disapearTimer -= Time.deltaTime;
        if (disapearTimer <= 0)
        {
            c.a -= 2f * Time.deltaTime;
            textMeshPro.color = c;
            if (c.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Setup(int damage, float r, float g, float b, float a)
    {
        textMeshPro.SetText(damage.ToString());
        c.r = r;
        c.g = g;
        c.b = b;
        c.a = a;
        textMeshPro.color = c;
        disapearTimer = 0.4f;
    }

}