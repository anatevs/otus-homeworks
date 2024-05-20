using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Sample;
using Equipment;
using System;

[TestFixture]
public class TestEquipment
{
    private CharacterStatNames _statNames;
    private Character _character;
    private Inventory _inventory;
    private Equipment.Equipment _equipment;
    private EquipmentSetter _equipmentSetter;

    private Item _swordItem;
    private Item _staffItem;
    private Item _axeItem;

    [SetUp]
    public void Setup()
    {
        _equipment = new Equipment.Equipment();

        _statNames = new CharacterStatNames();

        _character = new Character(
            new KeyValuePair<string, int>(_statNames.GetName(CharacterStat.Speed), 0),
            new KeyValuePair<string, int>(_statNames.GetName(CharacterStat.Defence), 0),
            new KeyValuePair<string, int>(_statNames.GetName(CharacterStat.Damage), 0)
            );

        _swordItem = CreateEquipment("Sword", EquipmentType.RIGHT_HAND, CharacterStat.Damage, 10);
        _staffItem = CreateEquipment("Staff", EquipmentType.RIGHT_HAND, CharacterStat.Defence, 10);
        _axeItem = CreateEquipment("Axe", EquipmentType.RIGHT_HAND, CharacterStat.Damage, 15);

        _inventory = new Inventory(_swordItem, _staffItem, _axeItem);

        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);
    }

    [Test]
    public void WhenEquip_AndInventoryHasThisEquipment_EquipmentShouldBeSettedToCharacter()
    {
        var item = _swordItem;
        var component = GetEquipmentComponent(item);

        _equipmentSetter.UseEquipment(item.Name);

        Assert.IsTrue(_equipment.HasItemOfType(component.Type));
        Assert.IsTrue(_equipment.HasItem(item));
    }

    [Test]
    public void WhenEquip_AndInventoryHasNotThisEquipment_EquipmentShouldBeSettedToCharacter()
    {
        var item = _swordItem;
        _inventory.RemoveItem(item);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        var ex = Assert.Throws<Exception>(() => _equipmentSetter.UseEquipment(item.Name));
        Assert.AreEqual($"there is no equipment item {item.Name} in inventory", ex.Message);
    }

    [Test]
    public void WhenUnequip_AndCharacterHasThisEquipment_EquipmentShuoldBeRemovedFromCharacter()
    {
        var item = _swordItem;
        var component = GetEquipmentComponent(item);

        _equipmentSetter.UseEquipment(item.Name);
        _equipmentSetter.RemoveEquipment(item.Name);

        Assert.IsFalse(_equipment.HasItemOfType(component.Type));
        Assert.IsFalse(_equipment.HasItem(item));
    }

    [Test]
    public void WhenUnequip_AndCharacterHasNotThisEquipment_EquipmentShuoldBeRemovedFromCharacter()
    {
        var item = _swordItem;
        var component = GetEquipmentComponent(item);

        Assert.IsFalse(_equipment.HasItemOfType(component.Type));
        Assert.IsFalse(_equipment.HasItem(item));
        var ex = Assert.Throws<Exception>(() => _equipmentSetter.RemoveEquipment(item.Name));
        Assert.AreEqual($"there is no item {item.Name} in character's equipment", ex.Message);
    }

    [Test]
    public void WhenUnequip_AndInventoryHasNotThisEquipment_EquipmentShuoldBeRemovedFromCharacter()
    {
        var item = _swordItem;
        _inventory.RemoveItem(item);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        var ex = Assert.Throws<Exception>(() => _equipmentSetter.RemoveEquipment(item.Name));
        Assert.AreEqual($"there is no item {item.Name} in inventory", ex.Message);
    }

    [Test]
    public void WhenEquip_AndThisEquipmentIsInInventory_CharacterStatShouldBeIncreaseByEquipVal()
    {
        var item = _swordItem;
        var component = GetEquipmentComponent(item);

        int prevStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));
        _equipmentSetter.UseEquipment(item.Name);
        int currStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        Assert.AreEqual(currStatVal - prevStatVal, component.Value);
    }

    [Test]
    public void WhenUnequip_CharacterStatShouldDecreaseByEquipVal()
    {
        var item = _swordItem;
        var component = GetEquipmentComponent(item);

        _equipmentSetter.UseEquipment(item.Name);
        int prevStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        _equipmentSetter.RemoveEquipment(item.Name);
        int currStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        Assert.AreEqual(prevStatVal - currStatVal, component.Value);
    }

    [Test]
    public void WhenEquipWithType_AndCharacterHasThisType_CharacterEquipmentShuoldBeChanged()
    {
        var components = EquipmentComponents(_swordItem, _staffItem);
        var type = components[_swordItem.Name].Type;

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var prevEquipment = _equipment.GetItem(type);

        _equipmentSetter.UseEquipment(_staffItem.Name);
        var currEquipment = _equipment.GetItem(type);


        Assert.AreNotEqual(prevEquipment, currEquipment);
    }

    [Test]
    public void WhenEquipWithTypeAndStat_AndCharacterHasThisTypeAndStat_StatShouldDecreaseByPrevAndIncreaseByNew()
    {
        var components = EquipmentComponents(_swordItem, _axeItem);
        string stat = _statNames.GetName(components[_swordItem.Name].CharacterStat);

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var prevStatVal = _character.GetStat(stat);

        _equipmentSetter.UseEquipment(_axeItem.Name);
        var currStatVal = _character.GetStat(stat);

        var expectedStatVal = prevStatVal
            - components[_swordItem.Name].Value
            + components[_axeItem.Name].Value;

        Assert.AreEqual(currStatVal, expectedStatVal);
    }

    private Item CreateEquipment(string name, EquipmentType type, CharacterStat stat, int statVal)
    {
        var config = ScriptableObject.CreateInstance<ItemConfig>();
        var component = new EquipmentComponent(type, stat, statVal);
        config.item = new Item(name, ItemFlags.EQUPPABLE, component);
        var item = config.item.Clone();

        return item;
    }

    private Dictionary<string, EquipmentComponent> EquipmentComponents(params Item[] items)
    {
        Dictionary<string, EquipmentComponent> res = new();

        foreach (var item in items)
        {
            var component = item.GetComponent<EquipmentComponent>();
            res.Add(item.Name, component);
        }
        return res;
    }

    private EquipmentComponent GetEquipmentComponent(Item item)
    {
        var components = EquipmentComponents(item);
        return components[item.Name];
    }
}