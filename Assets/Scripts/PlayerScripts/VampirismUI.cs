using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VampirismAbility))]
public class VampirismUI : VampirismAbility
{
    [SerializeField] private Text _text;

    private const string AbilityAvailable = "Press E";
    private const string AbilityCasted = "Vampirism active";
    private const string AbilityOnCooldown = "Cooldown";

    private void Awake()
    {        
        _text.text = AbilityAvailable;
        AbilityActive += EnableAbility;
    }

    private void OnDestroy()
    {
        AbilityActive -= EnableAbility;
    }

    private void EnableAbility(bool isActive)
    {
        if (isActive)
        {
            StartCoroutine(UpdateUI());
        }       
    }

    private IEnumerator UpdateUI()
    {
        float duration = Duration;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _text.text = $"{AbilityCasted} ({Mathf.CeilToInt(duration - elapsedTime)})";
            yield return null;
        }

        StartCoroutine(CooldownUI());
    }

    private IEnumerator CooldownUI()
    {
        float cooldown = Cooldown;
        float elapsedTime = 0f;

        while (elapsedTime < cooldown)
        {
            elapsedTime += Time.deltaTime;
            _text.text = $"{AbilityOnCooldown} ({Mathf.CeilToInt(cooldown - elapsedTime)})";
            yield return null;
        }

        _text.text = AbilityAvailable;
    }
}
