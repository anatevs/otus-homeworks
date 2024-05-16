using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Sample;
using Equipment;

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
    }

    [Test]
    public void WhenEquip_EquipmentShouldBeSettedToCharacter()
    {
        var components = SingleTestSetup(_swordItem);
        var type = components[_swordItem.Name].Type;

        _equipmentSetter.UseEquipment(_swordItem.Name);

        Assert.IsTrue(_equipment.HasItemOfType(type));
        Assert.IsTrue(_equipment.HasItem(_swordItem));
    }

    [Test]
    public void WhenUnequip_EquipmentShuoldBeRemovedFromCharacter()
    {
        var components = SingleTestSetup(_swordItem);
        var type = components[_swordItem.Name].Type;

        _equipmentSetter.UseEquipment(_swordItem.Name);
        _equipmentSetter.RemoveEquipment(_swordItem.Name);

        Assert.IsFalse(_equipment.HasItemOfType(type));
        Assert.IsFalse(_equipment.HasItem(_swordItem));
    }

    [Test]
    public void WhenEquip_CharacterStatShouldBeIncreaseByEquipVal()
    {
        var components = SingleTestSetup(_swordItem);
        var component = components[_swordItem.Name];

        int prevStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));
        _equipmentSetter.UseEquipment(_swordItem.Name);
        int currStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        Assert.AreEqual(currStatVal - prevStatVal, component.Value);
    }

    [Test]
    public void WhenUnEquip_CharacterStatShouldDecreaseByEquipVal()
    {
        var components = SingleTestSetup(_swordItem);
        var component = components[_swordItem.Name];

        _equipmentSetter.UseEquipment(_swordItem.Name);
        int prevStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        _equipmentSetter.RemoveEquipment(_swordItem.Name);
        int currStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        Assert.AreEqual(prevStatVal - currStatVal, component.Value);
    }

    [Test]
    public void WhenEquipWithType_AndCharacterHasThisType_CharacterEquipmentShuoldBeChanged()
    {
        var components = SingleTestSetup(_swordItem, _staffItem);

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
        var components = SingleTestSetup(_swordItem, _axeItem);

        string stat = _statNames.GetName(components[_swordItem.Name].CharacterStat);

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var prevStatVal = _character.GetStat(stat);

        _equipmentSetter.UseEquipment(_axeItem.Name);
        var currStatVal = _character.GetStat(stat);

        Assert.AreEqual(currStatVal, prevStatVal 
            - components[_swordItem.Name].Value
            + components[_axeItem.Name].Value);
    }

    private Item CreateEquipment(string name, EquipmentType type, CharacterStat stat, int statVal)
    {
        var config = ScriptableObject.CreateInstance<ItemConfig>();
        var component = new EquipmentComponent(type, stat, statVal);
        config.item = new Item(name, ItemFlags.EQUPPABLE, component);
        var item = config.item.Clone();

        return item;
    }

    private Dictionary<string, EquipmentComponent> SingleTestSetup(params Item[] items)
    {
        _inventory = new Inventory(items);

        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        Dictionary<string, EquipmentComponent> res = new();

        foreach (var item in items)
        {
            var component = item.GetComponent<EquipmentComponent>();
            res.Add(item.Name, component);
        }
        return res;
    }
}