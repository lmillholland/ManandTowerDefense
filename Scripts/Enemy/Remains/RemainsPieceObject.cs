using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsPieceObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Ignore collisions with everything except Remains
        var tag = collision.gameObject.tag;
        if (tag != "Remains" && tag != "Ground")
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
    }
}
