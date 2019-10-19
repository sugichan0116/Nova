using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.UI;
using System.Linq;

public class MarketItem : MonoBehaviour
{
    public Product product;
    private float price, onePrice, dealNum;
    private int buyNum, sellNum;

    private Market market;

    [Header("UI")]
#if UNITY_EDITOR
    [SerializeField]
    private bool isHideUI;
#endif
    [HideIf("isHideUI")] [SerializeField] private bool IsEvenRow;
    private Color evenColor = new Color(0, 0, 0, 0.1f);
    private Color oddColor = new Color(0, 0, 0, 0.2f);
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI Tcategory;
    [HideIf("isHideUI")] [SerializeField] Image icon;
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI Tproduct;
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI Tprice;
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI TpriceSub;
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI TbuyNum;
    [HideIf("isHideUI")] [SerializeField] TextMeshProUGUI TsellNum;
    [HideIf("isHideUI")] [SerializeField] List<Image> cells;

    // Update is called once per frame
    void Update()
    {
        Reflect();
        Fetch();
    }

    public void Sell()
    {
        MarketField.Instance.Sell(product);
        Fetch();
    }

    public void Buy()
    {
        MarketField.Instance.Buy(product);
        Fetch();
    }

    public void Init(Market market, Product product, int index)
    {
        this.market = market;
        this.product = product;
        this.IsEvenRow = index % 2 == 0;

        Fetch();
    }

    private void Fetch()
    {
        var item = market.Items.Where(i => i.product == product).FirstOrDefault();

        if (item == null) return;

        buyNum = item.supply;
        onePrice = market.ImposeTax(item.Price);
        dealNum = MarketField.Instance.dealWith;
        price = onePrice * dealNum;
        sellNum = InventoryManager.Instance.GetStock(product);
    }

    [Button]
    private void Reflect()
    {
        //Debug.Log($"[Reflect]");

        if (product == null) return;

        Tcategory.text = $"{product.category}";
        icon.sprite = product.icon;
        Tproduct.text = product.productName;
        Tprice.text = $" {WithComma(price)}";
        TpriceSub.text = (dealNum > 1) ? $"{WithComma(onePrice)} (tax in) x {dealNum}" : "";
        TbuyNum.text = $"stock\n{WithComma(buyNum)}";
        TsellNum.text = $"you have\n{WithComma(sellNum)}";

        foreach (var cell in cells)
        {
            cell.color = (IsEvenRow) ? evenColor : oddColor;
        }
    }

    private string WithComma(float value)
    {
        return String.Format("{0:#,0}", value);
    }
}
