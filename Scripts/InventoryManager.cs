using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory; 

    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);  

            if (inventory == null)
            {
                inventory = new Inventory();  
            }
        }
    }

    public bool CheckCompleteInventory()
    {
        foreach (var item in inventory.GetItemList())
        {
            if (item.amount < 1)
                return false;
        }
        return true; 
    }
}

