using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarViewController : MonoBehaviour
{
    public int ItemCount;
    [SerializeField] private HealthBarItemView _itemPrefab;

    public static List<HealthBarItemView> _items = new List<HealthBarItemView>();

    public List<HealthBarItemView> items = new List<HealthBarItemView>();
    private bool _isInitialized = false;

    public static HealthBarItemView CurrentItem;
    public static int CurrentItemIndex;

    public static Stats PlayerStats;

    public void InitializeItems()
    {
        ItemCount = (int) PlayerStats.Health;
        
        for (int i = 0; i < ItemCount; i++)
        {
            HealthBarItemView item = Instantiate(_itemPrefab, transform);
            _items.Add(item);
        }
        CurrentItemIndex = 1;
        SetCurrentItem();
        _isInitialized = true;
    }

    public static void SetCurrentItem()
    {
        if(CurrentItem != _items[0])
        {
            CurrentItem = _items[_items.Count - CurrentItemIndex];
        }        
    }

    public void ResetItems()
    {
        ItemCount = 0;
        _items = new List<HealthBarItemView>();
        Debug.Log(_items.Count);
        HealthBarItemView[] currentItems = transform.GetComponentsInChildren<HealthBarItemView>();
        foreach (HealthBarItemView item in currentItems)
        {
            Destroy(item.gameObject);
        }
    }
}
