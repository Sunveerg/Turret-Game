using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10f;
    private float _currentHealth;

    [SerializeField] private Image _healthbarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar(_maxHealth, _currentHealth);
    }

    public void ReduceHealth(float damage)
    {
        Debug.Log("Reduce health");
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }

    public void HealHealth(float heal)
    {
        if (_currentHealth < 10.0f)
        {
            _currentHealth += heal;

            UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }
}
