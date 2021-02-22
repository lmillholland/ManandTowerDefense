using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    HealthController healthController;
    public float zOffset = .25f;
    public bool isCastle = false;

    [Header("Status Icons")]
    public GameObject statusContainer;
    public Sprite poisionStatusIcon;

    void Awake()
    {
        healthController = GetComponent<HealthController>();

        // make HUD float above subject's head
        Mesh mesh = transform.parent.GetComponentInChildren<MeshFilter>().mesh;
        float zSize = mesh.bounds.extents.z;
        transform.localPosition += new Vector3(0f, 0f, zSize + zOffset);
    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }

    public void Initialize(int? level, float maxHealth)
    {
        if (!isCastle)
        {
            // something about level
        }
        healthController.Initialize(maxHealth);
    }

    public void AddStatusEffect(StatusType statusType)
    {
        // create status image
        GameObject imgObject = new GameObject(statusType.ToString());
        imgObject.transform.eulerAngles = new Vector3(90f, 0f, 0f);
        imgObject.transform.position = Vector3.zero;
        imgObject.transform.SetParent(statusContainer.transform);
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(statusContainer.transform);
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        trans.anchoredPosition = Vector3.zero;
        //trans.sizeDelta= new Vector2(150, 200); // custom size

        // actually create image
        Image image = imgObject.AddComponent<Image>();
        image.sprite = poisionStatusIcon; // TODO: make dynamic
    }

    public void RemoveStatusEffect(StatusType statusType)
    {
        var iconObj = statusContainer.transform.Find(statusType.ToString());
        Destroy(iconObj.gameObject);
    }
}
