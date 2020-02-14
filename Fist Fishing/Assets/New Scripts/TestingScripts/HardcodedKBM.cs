﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HardcodedKBM : MonoBehaviour
{
    public KeyConfiguration m_KBMKeyConfig;



    // Start is called before the first frame update
    void Start()
    {

        m_KBMKeyConfig = new KeyConfiguration();
        m_KBMKeyConfig.m_ConfigName = "KBM";
        /*button
    Action
    SecondaryAction
    OpenInventory
    MainMenu
    HoldPickup
    FleeOrCameraModeSwap
    */
        ActionDefinition action = new ActionDefinition();
        action.SetInternalID(ActionID.Action);
        action.SetHumanID("Action");
        action.SetDescription("Action");
        action.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        action.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keyAction = new KeyCodeOrDirectionCode();
        keyAction.key = KeyCode.E;
        m_KBMKeyConfig.m_allTheInputs.Add(action, keyAction);

        ActionDefinition action2 = new ActionDefinition();
        action2.SetInternalID(ActionID.SecondaryAction);
        action2.SetHumanID("SecondaryAction");
        action2.SetDescription("SecondaryAction");
        action2.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        action2.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keySecondaryAction = new KeyCodeOrDirectionCode();
        keySecondaryAction.key = KeyCode.R;
        m_KBMKeyConfig.m_allTheInputs.Add(action2, keySecondaryAction);

        ActionDefinition inventory = new ActionDefinition();
        inventory.SetInternalID(ActionID.OpenInventory);
        inventory.SetHumanID("OpenInventory");
        inventory.SetDescription("OpenInventory");
        inventory.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        inventory.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keyinventory = new KeyCodeOrDirectionCode();
        keyinventory.key = KeyCode.I;
        m_KBMKeyConfig.m_allTheInputs.Add(inventory, keyinventory);

        ActionDefinition holdPickup = new ActionDefinition();
        holdPickup.SetInternalID(ActionID.HoldPickup);
        holdPickup.SetHumanID("HoldPickup");
        holdPickup.SetDescription("HoldPickup");
        holdPickup.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        holdPickup.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keyHoldPickup = new KeyCodeOrDirectionCode();
        keyHoldPickup.key = KeyCode.Mouse0;
        m_KBMKeyConfig.m_allTheInputs.Add(holdPickup, keyHoldPickup);

        ActionDefinition mainmenu = new ActionDefinition();
        mainmenu.SetInternalID(ActionID.MainMenu);
        mainmenu.SetHumanID("MainMenu");
        mainmenu.SetDescription("MainMenu");
        mainmenu.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        mainmenu.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keymainmenu = new KeyCodeOrDirectionCode();
        keymainmenu.key = KeyCode.I;
        m_KBMKeyConfig.m_allTheInputs.Add(mainmenu, keymainmenu);

        ActionDefinition FleeOrCameraModeSwap = new ActionDefinition();
        FleeOrCameraModeSwap.SetInternalID(ActionID.FleeOrCameraModeSwap);
        FleeOrCameraModeSwap.SetHumanID("FleeOrCameraModeSwap");
        FleeOrCameraModeSwap.SetDescription("FleeOrCameraModeSwap");
        FleeOrCameraModeSwap.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        FleeOrCameraModeSwap.SetActionType(ActionType.Button);
        KeyCodeOrDirectionCode keyFlee = new KeyCodeOrDirectionCode();
        keyFlee.key = KeyCode.T;
        m_KBMKeyConfig.m_allTheInputs.Add(FleeOrCameraModeSwap, keyFlee);





        /*1axis
            DivingBellRaiseLower
        */

        /*2axis
            CameraRotation
            MovementOrInventoryNavigation
            Targeting
            AttackSwap
            BaitSwap
            */


        ActionDefinition CameraRotation = new ActionDefinition();
        CameraRotation.SetInternalID(ActionID.CameraRotation);
        CameraRotation.SetHumanID("CameraRotation");
        CameraRotation.SetDescription("CameraRotation");
        CameraRotation.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        CameraRotation.SetActionType(ActionType.TwoAxis);
        KeyCodeOrDirectionCode keyCam = new KeyCodeOrDirectionCode();
        keyCam.direction = ALInput.DirectionCode.LookInput;
        m_KBMKeyConfig.m_allTheInputs.Add(CameraRotation, keyCam);

        ActionDefinition MovementOrInventoryNavigation = new ActionDefinition();
        MovementOrInventoryNavigation.SetInternalID(ActionID.MovementOrInventoryNavigation);
        MovementOrInventoryNavigation.SetHumanID("MovementOrInventoryNavigation");
        MovementOrInventoryNavigation.SetDescription("MovementOrInventoryNavigation");
        MovementOrInventoryNavigation.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        MovementOrInventoryNavigation.SetActionType(ActionType.TwoAxis);
        KeyCodeOrDirectionCode keymovemenu = new KeyCodeOrDirectionCode();
        keymovemenu.direction = ALInput.DirectionCode.MoveInput;
        m_KBMKeyConfig.m_allTheInputs.Add(MovementOrInventoryNavigation, keymovemenu);

        ActionDefinition Targeting = new ActionDefinition();
        Targeting.SetInternalID(ActionID.Targeting);
        Targeting.SetHumanID("Targeting");
        Targeting.SetDescription("Targeting");
        Targeting.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        Targeting.SetActionType(ActionType.TwoAxis);
        KeyCodeOrDirectionCode keyTargeting = new KeyCodeOrDirectionCode();
        keyTargeting.direction = ALInput.DirectionCode.MoveInput;
        m_KBMKeyConfig.m_allTheInputs.Add(Targeting, keyTargeting);

        ActionDefinition AttackSwap = new ActionDefinition();
        AttackSwap.SetInternalID(ActionID.AttackSwap);
        AttackSwap.SetHumanID("AttackSwap");
        AttackSwap.SetDescription("AttackSwap");
        AttackSwap.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        AttackSwap.SetActionType(ActionType.TwoAxis);
        KeyCodeOrDirectionCode keyAttackSwap = new KeyCodeOrDirectionCode();
        keyAttackSwap.direction = ALInput.DirectionCode.MoveInput;
        m_KBMKeyConfig.m_allTheInputs.Add(AttackSwap, keyAttackSwap);

        ActionDefinition BaitSwap = new ActionDefinition();
        BaitSwap.SetInternalID(ActionID.BaitSwap);
        BaitSwap.SetHumanID("BaitSwap");
        BaitSwap.SetDescription("BaitSwap");
        BaitSwap.SetContexts(ContextGroup.Battle | ContextGroup.Swimming | ContextGroup.MenuNavigation | ContextGroup.BoatTravel | ContextGroup.InventoryShop);
        BaitSwap.SetActionType(ActionType.TwoAxis);
        KeyCodeOrDirectionCode keyBaitSwap = new KeyCodeOrDirectionCode();
        keyBaitSwap.direction = ALInput.DirectionCode.MoveInput;
        m_KBMKeyConfig.m_allTheInputs.Add(BaitSwap, keyBaitSwap);


        /*3axis
            MenuAndShopMenuNavigationPageSelect
            */














    }

// Update is called once per frame
void Update()
    {
        
    }
}
