using Sample;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public sealed class InventoryContext : MonoBehaviour
{
    [SerializeReference]
    public ItemConfig ItemConfig;

    [ShowInInspector]
    private Inventory _inventory;

    [Inject]
    public void Construct(Inventory inventory)
    {
        _inventory = inventory;
    }

    [Button]
    public void AddItem()
    {
        Item item = ItemConfig.item.Clone();
        _inventory.AddItem(item);
    }
}