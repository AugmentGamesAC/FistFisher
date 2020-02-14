using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatInfo : CombatInfo
{
    public PlayerInstance m_playerInstance = new PlayerInstance();

    public PinWheel<CombatMoveInfo> m_attackPinwheel = new PinWheel<CombatMoveInfo>();
}
