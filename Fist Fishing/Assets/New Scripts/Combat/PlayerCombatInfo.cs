using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// info tracker for player for when in combat
/// </summary>
public class PlayerCombatInfo : CombatInfo
{
    public PlayerInstance m_playerInstance = new PlayerInstance();

    public PinWheel<CombatMoveInfo> m_attackPinwheel = new PinWheel<CombatMoveInfo>();
}
