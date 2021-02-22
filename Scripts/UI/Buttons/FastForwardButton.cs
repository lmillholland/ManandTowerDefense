using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForwardButton : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Toggle()
    {
        /*
        Change LevelManager's isFastForwarding variable and change button sprite.
        */
        LevelManager.instance.isFastForwarding = !LevelManager.instance.isFastForwarding;
        if (LevelManager.instance.isFastForwarding)
            image.sprite = activeSprite;
        else
            image.sprite = inactiveSprite;
    }
}
