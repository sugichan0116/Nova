using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;


public class Market : MonoBehaviour
{
    public string marketName;
    public float tax = 5f;

    //[ReorderableList]
    [SerializeField]
    private List<FlowingProduct> items;

    public IEnumerable<FlowingProduct> Items { get => items; }

    public void SetMarket()
    {
        MarketField.Instance.SetupMarket(this);
    }

    public void Sell(Product product, int num)
    {
        Debug.Log($"[Market Sell] {product} x {num}");

        //var item = InventoryManager.Instance.Items
        //    .Where(i => i.product == product)
        //    .FirstOrDefault();
        var item = items.Where(i => i.product == product).FirstOrDefault();

        if (item == null) return;
        if(InventoryManager.Instance.GetStock(product) < num)
        {
            MessageLog.Print("[Error] No stock");
            return;
        }

        var price = item.Price * num;

        InventoryManager.Instance.EarnMoney(price);
        InventoryManager.Instance.SellProduct(product, num, price);
        item.demmand -= num;
        item.supply += num;
        item.UpdatePrice();
    }

    public void Buy(Product product, int num)
    {
        Debug.Log($"[Market Buy] {product} x {num}");

        var item = items.Where(i => i.product == product).FirstOrDefault();

        if (item == null) return;
        if (item.supply < num)
        {
            MessageLog.Print("[Error] No stock");
            return;
        }

        var price = ImposeTax(item.Price) * num;
        if(InventoryManager.Instance.TryToPay(price))
        {
            InventoryManager.Instance.BuyProduct(product, num, price);
            item.demmand += num;
            item.supply -= num;
            item.UpdatePrice(); //test
        }
    }

    public float ImposeTax(float price)
    {
        return price * (1f + tax / 100f);
    }
}
