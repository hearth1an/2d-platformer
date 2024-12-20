using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VampirismAbility))]
public class VampirismUI : MonoBehaviour
{
    [SerializeField] private Text _text;    

    private const string AbilityAvailable = "Press E";
    private const string AbilityCasted = "Vampirism active";
    private const string AbilityOnCooldown = "Cooldown";

    private VampirismAbility _vampirismAbility;

    private void Awake()
    {   
        _vampirismAbility = GetComponent<VampirismAbility>();
        _text.text = AbilityAvailable;
        _vampirismAbility.AbilityActive += EnableAbility;
    }

    private void OnDestroy()
    {
        _vampirismAbility.AbilityActive -= EnableAbility;
    }

    private void EnableAbility()
    {
        StartCoroutine(UpdateUI());
    }

    private IEnumerator UpdateUI()
    {
        float duration = _vampirismAbility.Duration;
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
        float cooldown = _vampirismAbility.Cooldown;
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
