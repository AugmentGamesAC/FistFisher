using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IMagicUser
{
    float m_health { get; set; }
    float m_mana { get; set; }
    float m_healthMax { get; set; }
    float m_manaMax { get; set; }
    float m_manaRegen { get; set; }

    float m_actualMana { get; set; } //current state of the small inner bar that tracks where it's at
    bool m_predictingManaUsage { get; set; } //if the bar is currently predicting mana usage before the spell is finally cast

    void TakeDamage(float damage);
    void TakeDamage(float damage, float duration);
    void UseMana(float mana);
}

