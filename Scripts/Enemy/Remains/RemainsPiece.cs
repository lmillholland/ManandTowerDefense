using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsPiece : RemainsPieceObject
{
    public float fadePerSecond = 1f;
    Material material;
    Collider collider;
    bool isFading = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        /*
        Fade out & destroy.
        */
        if (isFading)
        {
            var color = material.color;
            var fadeAmount = fadePerSecond * Time.deltaTime;
            material.color = new Color(color.r, color.g, color.b, color.a - fadeAmount);
            if (material.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void BeginFadeOut()
    {
        isFading = true;
    }
}
