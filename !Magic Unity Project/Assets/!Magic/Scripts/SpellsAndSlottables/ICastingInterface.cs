using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum InteractionType
{
    StartCasting,
    StopCasting,
    BypassAiming
};
/// <summary>
/// interface that botha SpellDescriptor and Spell will implemnet, Spell will manage which spell descriptor is used. 
/// </summary>
public interface ICastingInterface
{
    float m_manaCostPerSecond { get; }
    void BeginAiming();
    void Cancel();
    bool Interact(InteractionType interaction);
}
