using Sample;
using Sirenix.OdinInspector;
using System;

public class EquipmentSetter
{
    public event Action<string> OnEquipped;

    public event Action<string> OnUnequipped;

    private readonly Character _character;

    private readonly Inventory _inventory;

    private readonly Equipment.Equipment _equipment;

    private readonly CharacterStatNames _statNames;

    public EquipmentSetter(
        Character character,
        Inventory inventory,
        Equipment.Equipment equipment,
        CharacterStatNames statNames)
    {
        _character = character;
        _inventory = inventory;
        _equipment = equipment;
        _statNames = statNames;
    }

    [Button]
    public void UseEquipment(string itemName)
    {
        if (_inventory.FindItem(itemName, out Item newItem))
        {
            EquipmentComponent newEquipment =
                newItem.GetComponent<EquipmentComponent>();

            string newStat = _statNames.GetName(newEquipment.CharacterStat);

            if (_equipment.TryGetItem(newEquipment.Type, out Item currItem))
            {
                var currEquipment = currItem.GetComponent<EquipmentComponent>();

                string currStat = _statNames.GetName(currEquipment.CharacterStat);

                if (currStat == newStat)
                {
                    UpdateStat(newStat, newEquipment.Value - currEquipment.Value);
                }
                else
                {
                    UpdateStat(currStat, -currEquipment.Value);
                    UpdateStat(newStat, newEquipment.Value);
                }

                _equipment.ChangeItem(newEquipment.Type, newItem);

                OnUnequipped?.Invoke(currItem.Name);
            }
            else
            {
                UpdateStat(newStat, newEquipment.Value);

                _equipment.AddItem(newEquipment.Type, newItem);
            }

            OnEquipped?.Invoke(itemName);
        }

        else
        {
            throw new Exception($"there is no equipment item {itemName} in inventory");
        }
    }

    [Button]
    public void RemoveEquipment(string itemName)
    {
        if (_inventory.FindItem(itemName, out Item newItem))
        {
            EquipmentComponent component =
                newItem.GetComponent<EquipmentComponent>();

            string stat = _statNames.GetName(component.CharacterStat);

            if (_equipment.TryGetItem(component.Type, out Item item))
            {
                _equipment.RemoveItem(component.Type, item);
                UpdateStat(stat, -component.Value);
            }
            else
            {
                throw new Exception($"there is no item {itemName} in character's equipment");
            }

            OnUnequipped?.Invoke(itemName);
        }
        else
        {
            throw new Exception($"there is no item {itemName} in inventory");
        }
    }

    private void UpdateStat(string statName, int addValue)
    {
        _character.SetStat(statName, _character.GetStat(statName) + addValue);
    }
}