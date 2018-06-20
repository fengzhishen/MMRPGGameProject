using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 商品实体管理器
/// </summary>
public partial class ProductDBModel:AbstractDBModel<ProductDBModel,ProductEntity>,IDisposable
{
    sealed protected override string DataFileName()
    {
        return base.DataFileName() + "Product.data";
    }
    protected override ProductEntity MakeEntity(GameDataTableParser dataParser)
    {
        ProductEntity product = new ProductEntity
        {
            Id = dataParser.GetFieldValue("Id").ToInt(),
            Name = dataParser.GetFieldValue("Name"),
            PicName = dataParser.GetFieldValue("PicName"),
            Price = dataParser.GetFieldValue("Price").ToFloat(),
            Desc = dataParser.GetFieldValue("Desc")
        };
        return product;
    }
    public override void Dispose()
    {
        base.Dispose();
    }  
}
