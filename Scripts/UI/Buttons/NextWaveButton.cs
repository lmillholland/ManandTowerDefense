using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    bool isActive = true;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Activate()
    {
        // TODO: check if next wave is ready to be sent
        isActive = true;
        image.sprite = activeSprite;
    }

    public void Deactivate()
    {
        isActive = false;
        image.sprite = inactiveSprite;
    }

    public void StartNextWave()
    {
        if (isActive)
        {
            Deactivate();
            StartCoroutine(WaveManager.instance.StartNextWave());
        }
    }
}
