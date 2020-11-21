using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductItem : MonoBehaviour
{
    public delegate void OnClickEvent(ProductDetailBean detailBean);
    public Text productNameText;
    public Text priceText;
    public ProductDetailBean productDetailBean;
    public OnClickEvent onClick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setProductDetail(ProductDetailBean detailBean)
    {
        productDetailBean = detailBean;
        productNameText.text = detailBean.productName;
        priceText.text = detailBean.price + " " + detailBean.currency;
    }

    public void OnProductClick()
    {
        if (onClick != null)
            onClick(productDetailBean);
    }

}