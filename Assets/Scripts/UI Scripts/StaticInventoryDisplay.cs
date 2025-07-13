using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    protected InventoryHolder InventoryHolder;
    [SerializeField] protected InventorySlot_UI[] slots;

    protected virtual void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;
    }
    
    protected virtual void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }

    private void RefreshStaticDisplay()
    {
        if (InventoryHolder != null)
        {
            inventorySystem = InventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem, 0);
    }

    protected virtual void Start()
    {
        RefreshStaticDisplay();
    }
    
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        for (int i = 0; i < InventoryHolder.Offset; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
