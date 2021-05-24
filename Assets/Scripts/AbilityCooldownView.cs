using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Sword _ability;

    private void Update()
    {
        _slider.value = _ability.CoolDown / _ability.CoolDownTime;
    }

    public void OnPlayerDied()
    {
        gameObject.SetActive(false);
    }
}
