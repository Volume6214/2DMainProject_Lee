using UnityEngine;

public class StdInventoryManager : MonoBehaviour
{
    public static StdInventoryManager Instance { get; private set; }

    [SerializeField] private int[] _inventorySlots = new int[10];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool CanPlaceItem(int startIndex, int size)
    {
        if (startIndex < 0 || startIndex + size > _inventorySlots.Length) return false;

        for (int i = startIndex; i < startIndex + size; i++)
        {
            if (_inventorySlots[i] != 0) return false;
        }
        return true;
    }

    public void PlaceItem(int startIndex, int size)
    {
        for (int i = startIndex; i < startIndex + size; i++)
        {
            if (i >= 0 && i < _inventorySlots.Length)
                _inventorySlots[i] = 1;
        }
    }

    public void RemoveItem(int startIndex, int size)
    {
        for (int i = startIndex; i < startIndex + size; i++)
        {
            if (i >= 0 && i < _inventorySlots.Length)
                _inventorySlots[i] = 0;
        }
    }
}