using UnityEngine;
using System.Collections;

/// <summary>
/// 产品实体类
/// </summary>
public partial class ProductEntity:AbstractEntity
{
    public ProductEntity()
    {

    }
    /// <summary>
    /// 商品名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 商品价格
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// 商品图片名字
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// 商品描述
    /// </summary>
    public string Desc { get; set; }
}
