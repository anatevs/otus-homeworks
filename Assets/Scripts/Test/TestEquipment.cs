using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sample;
using Equipment;
using System.Drawing.Text;

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
        _inventory = new Inventory(_swordItem);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var swordType = _swordItem.GetComponent<EquipmentComponent>().Type;

        Assert.IsTrue(_equipment.HasItemOfType(swordType));
        Assert.IsTrue(_equipment.HasItem(_swordItem));
    }

    [Test]
    public void WhenEquip_CharacterStatShouldBeChangedByEquipVal()
    {
        _inventory = new Inventory(_swordItem);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        var component = _swordItem.GetComponent<EquipmentComponent>();
        int prevStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));
        _equipmentSetter.UseEquipment(_swordItem.Name);
        int currStatVal = _character.GetStat(_statNames.GetName(component.CharacterStat));

        Assert.AreEqual(currStatVal - prevStatVal, component.Value);
    }

    [Test]
    public void WhenEquipWithType_AndCharacterHasThisType_CharacterEquipmentShuoldChanged()
    {
        _inventory = new Inventory(_swordItem, _staffItem);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        var component = _swordItem.GetComponent<EquipmentComponent>();
        var type = component.Type;

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var prevEquipment = _equipment.GetItem(type);

        _equipmentSetter.UseEquipment(_staffItem.Name);
        var currEquipment = _equipment.GetItem(type);


        Assert.AreNotEqual(prevEquipment, currEquipment);
    }

    [Test]
    public void WhenEquipWithTypeAndStat_AndCharacterHasThisTypeAndStat_StatShouldDecreaseByPrevAndIncreaseByNew()
    {
        _inventory = new Inventory(_swordItem, _axeItem);
        _equipmentSetter =
            new EquipmentSetter(_character, _inventory, _equipment, _statNames);

        var componentSword = _swordItem.GetComponent<EquipmentComponent>();
        var componentAxe = _axeItem.GetComponent<EquipmentComponent>();

        string stat = _statNames.GetName(componentSword.CharacterStat);

        _equipmentSetter.UseEquipment(_swordItem.Name);
        var prevStatVal = _character.GetStat(stat);

        _equipmentSetter.UseEquipment(_axeItem.Name);
        var currStatVal = _character.GetStat(stat);

        Assert.AreEqual(currStatVal, prevStatVal - componentSword.Value + componentAxe.Value);
    }

    //TODO: removing tests


    private Item CreateEquipment(string name, EquipmentType type, CharacterStat stat, int statVal)
    {
        var config = ScriptableObject.CreateInstance<ItemConfig>();
        var component = new EquipmentComponent(type, stat, statVal);
        config.item = new Item(name, ItemFlags.EQUPPABLE, component);
        var item = config.item.Clone();

        return item;
    }
}