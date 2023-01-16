using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    [SerializeField] private Slider _staminaBar;
    private float _maxStamina = 100;
    public float _currentStamina;
    public bool _usingStamina;

    void Start()
    {
        _currentStamina = _maxStamina;
        _staminaBar.maxValue = _currentStamina;
        _staminaBar.value = _currentStamina;
        _usingStamina = false;
    }

    public bool HasStamina()
    {
        return Mathf.Floor(_currentStamina) > 0;
    }

    public bool UseStamina(float amount)
    {
        if(_currentStamina - amount >= 0)
        {
            _usingStamina = true;
            _currentStamina -= amount;
            _staminaBar.value = _currentStamina;
            return true;
        }
        else
        {
            Debug.Log("Not enough stamina");
            return false;
        }
    }

    public async void StopUsingStamina(float recoveryRate)
    {
        _usingStamina = false;
        await RecoverStamina(recoveryRate);
    }

    public async Task RecoverStamina(float amount)
    {
        await Task.Delay(1000);
        while(_currentStamina < _maxStamina && !_usingStamina)
        {
            _currentStamina += amount;
            _staminaBar.value = _currentStamina;
            await Task.Delay(100);
        }
    }


}
