using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellAttack1Cooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldownAttack;
    [SerializeField] private TMP_Text textCooldownAttack;

    private bool isCooldownAttack = false;
    private float cooldownTimeAttack = 0.3f; //SameTimeThanPlayerCombatAttack1
    private float cooldownTimerAttack = 0.0f;

    public void Start()
    {
        imageCooldownAttack.fillAmount = 0.0f;
        textCooldownAttack.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            UseSpell();

        if(isCooldownAttack)
            ApplyCooldown();
    }

    private void ApplyCooldown()
    {
        cooldownTimerAttack -= Time.deltaTime;

        if (cooldownTimerAttack < 0.0f)
        {
            isCooldownAttack = false;
            textCooldownAttack.gameObject.SetActive(false);
            imageCooldownAttack.fillAmount = 0.0f;
        }
        else
        {
            textCooldownAttack.text = Mathf.RoundToInt(cooldownTimerAttack).ToString();
            imageCooldownAttack.fillAmount = cooldownTimerAttack / cooldownTimeAttack;
        }
    }

    private void UseSpell()
    {
        if(!isCooldownAttack)
        {
            isCooldownAttack = true;
            textCooldownAttack.gameObject.SetActive(true);
            cooldownTimerAttack = cooldownTimeAttack;
        }
    }
}
