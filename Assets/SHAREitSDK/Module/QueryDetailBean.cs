using System;
using LitJson;

[ System.Serializable]
public class QueryDetailBean
{
    private string merchantOrderNo;
    private string orderNo;
    private string productId;
    private int type;

    public string MerchantOrderNo { get => merchantOrderNo; set => merchantOrderNo = value; }
    public string OrderNo { get => orderNo; set => orderNo = value; }
    public string ProductId { get => productId; set => productId = value; }
    public int Type { get => type; set => type = value; }
}
