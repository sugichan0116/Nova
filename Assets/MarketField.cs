using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketField : SingletonMonoBehaviour<MarketField>
{
    private const string PREFAB = "market item";
    public Market market;
    public int dealWith = 1;

    public void SetupMarket(Market market)
    {
        this.market = market;

        ShutdownMarket();

        int index = 0;
        foreach (var item in market.Items)
        {
            var entry = ResourcesFactory.Instantiate<MarketItem>(PREFAB, transform);
            entry.Init(market, item.product, index);
            index++;
        }
    }

    public void ShutdownMarket()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Sell(Product product)
    {
        market.Sell(product, dealWith);
    }

    public void Buy(Product product)
    {
        market.Buy(product, dealWith);
    }
}
