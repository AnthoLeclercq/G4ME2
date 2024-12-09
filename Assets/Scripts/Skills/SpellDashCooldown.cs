using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellDashCooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldownDash;
    [SerializeField] private TMP_Text textCooldownDash;

    private bool isCooldownDash = false;
    private float cooldownTimeDash = 3.0f; //SameTimeThanPlayerMovementDash
    private float cooldownTimerDash = 0.0f;

    public void Start()
    {
        imageCooldownDash.fillAmount = 0.0f;
        textCooldownDash.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            UseSpell();

        if(isCooldownDash)
            ApplyCooldown();
    }

    private void ApplyCooldown()
    {
        cooldownTimerDash -= Time.deltaTime;

        if (cooldownTimerDash < 0.0f)
        {
            isCooldownDash = false;
            textCooldownDash.gameObject.SetActive(false);
            imageCooldownDash.fillAmount = 0.0f;
        }
        else
        {
            textCooldownDash.text = Mathf.RoundToInt(cooldownTimerDash).ToString();
            imageCooldownDash.fillAmount = cooldownTimerDash / cooldownTimeDash;
        }
    }

    private void UseSpell()
    {
        if(!isCooldownDash)
        {
            isCooldownDash = true;
            textCooldownDash.gameObject.SetActive(true);
            cooldownTimerDash = cooldownTimeDash;
        }
    }
}
