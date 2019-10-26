using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using UnityEngine.Events;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    [SerializeField]
    private float money;
    [SerializeField]
    private List<HoldingProduct> items = new List<HoldingProduct>();

    public UnityEvent onPay;

    public IEnumerable<HoldingProduct> Items { get => items; }
    public float Money
    {
        get => money;
        set
        {
            Debug.Log($"[Money] set {value}");
            money = value;
        }
    }

    public void EarnMoney(float volume)
    {
        if (volume >= 1000f) MessageLog.Print($"[Gem] +{volume}");
        Money += volume;
    }

    public bool TryToPay(float volume)
    {
        if (Money >= volume)
        {
            onPay.Invoke();
            Money -= volume;
            return true;
        }
        else
        {
            MessageLog.Print("[Error] lack of Gem...");
            return false;
        }
    }

    public void BuyProduct(Product product, int num, float price)
    {
        Debug.Log($"[Inventory] Get {product} x {num}");
        MessageLog.Print($"[Item] Buy {product.productName} x{num}");

        var item = HoldingProduct(product);
        
        item.num += num;
        item.boughtPrice += price;
    }

    public void SellProduct(Product product, int num, float price)
    {
        Debug.Log($"[Inventory] Lost {product} x {num}");
        MessageLog.Print($"[Item] Sell {product.productName} x{num}");

        var item = HoldingProduct(product);

        var bought = item.boughtPrice / item.num * num;
        var gain = price - bought;
        MessageLog.Print($"[Gain] You earned Gem <color=green><b>{gain.ToString("+#;-#;")}</b></color>!");

        item.num -= num;
        item.boughtPrice -= bought;
    }

    //public void GetProduct(Product product, int num)
    //{
    //    Debug.Log($"[Inventory] Get {product} x {num}");
    //    MessageLog.Print($"[Item] Get {product.productName} +{num}");

    //    HoldingProduct(product).num += num;
    //}

    //public void LostProduct(Product product, int num)
    //{
    //    Debug.Log($"[Inventory] Lost {product} x {num}");
    //    MessageLog.Print($"[Item] Lost {product.productName} x{num}");

    //    var item = HoldingProduct(product);
    //    if (item.num < num) return;
    //    item.num -= num;
    //}

    public int GetStock(Product product)
    {
        return HoldingProduct(product).num;
    }

    private HoldingProduct HoldingProduct(Product product)
    {
        var item = items.Where(i => i.product == product).FirstOrDefault();

        if (item == null)
        {
            item = new HoldingProduct { product = product, num = 0 };
            items.Add(item);
        }

        return item;
    }
}

[System.Serializable]
public class HoldingProduct
{
    public Product product;
    public int num;
    public float boughtPrice;
}

[System.Serializable]
public class FlowingProduct
{
    public Product product;
    public int supply;
    public int demmand;

    [HideInInspector]
    private float price;

    public float Price { get { UpdatePrice(); return price; } }

    public void UpdatePrice()
    {
        var low = product.price.x;
        var high = product.price.y;
        var mid = (low + high) / 2f;

        price = Mathf.Clamp(mid * demmand / supply, low, high);
    }
}