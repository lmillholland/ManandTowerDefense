using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remains : MonoBehaviour
{
    public float lifespan = .5f;
    RemainsPiece[] remainsPieces;
    
    void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        // allow pieces to tumble for a bit
        yield return new WaitForSeconds(lifespan);

        // fade out each piece
        remainsPieces = GetComponentsInChildren<RemainsPiece>();
        foreach (var piece in remainsPieces)
            piece.BeginFadeOut();

        // destroy this after all constituent pieces disappear
        var fadeTime = remainsPieces[0].fadePerSecond;
        yield return new WaitForSeconds(fadeTime);
        Destroy(gameObject);
    }
}
