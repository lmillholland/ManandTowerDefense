using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance = null;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    [Header("Cursors")]
    public Texture2D baseCursorTexture;
    public Texture2D toolCursorTexture;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(baseCursorTexture, hotSpot, cursorMode);
    }

    public void SetToolCursor()
    {
        Cursor.SetCursor(toolCursorTexture, hotSpot, cursorMode);
    }
}
