using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    [Header("Damage Text Settings")]
    public float damageRiseSpeed = 3f;
    public float damageRiseDuration = 1f;
    public float damageFadeSpeed = .2f;
    
    [Header("UI References")]
    public Image healthBarImage;
    public GameObject damageTextPrefab;

    public void UpdateHealth(float damage, float healthPercent, bool isCritical)
    {
        // Decrease health bar and have text indicating damage fly out
        StartCoroutine(ShowDamageText(damage));
        healthBarImage.fillAmount = healthPercent;
    }

    IEnumerator ShowDamageText(float damage)
    {
        // Create text object
        var damageText = Instantiate(damageTextPrefab, transform.position, transform.rotation);
        damageText.transform.parent = transform;
        var damageTextMesh = damageText.GetComponent<TextMeshProUGUI>();

        // Assign damage to text
        var damageToDisplay = Mathf.Max(Mathf.Round(damage), 1f);
        damageTextMesh.text = damageToDisplay.ToString();

        // Rise and fade out damage text
        var damageRiseCounter = damageRiseDuration;
        var alphaChange = 255f / damageRiseDuration;
        while (damageRiseCounter >= 0)
        {
            damageRiseCounter -= Time.deltaTime;
            damageText.transform.position += new Vector3(0f, 0f, Time.deltaTime * damageRiseSpeed);
            damageTextMesh.color = new Color(damageTextMesh.color.r, 
                                             damageTextMesh.color.g,
                                             damageTextMesh.color.b,
                                             damageTextMesh.color.a - damageFadeSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Destroy text
        Destroy(damageText);
    }
}
