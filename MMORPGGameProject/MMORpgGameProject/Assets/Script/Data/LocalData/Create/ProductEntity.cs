using UnityEngine;
using System.Collections;

/// <summary>
/// ��Ʒʵ����
/// </summary>
public partial class ProductEntity:AbstractEntity
{
    public ProductEntity()
    {

    }
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ��Ʒ�۸�
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// ��ƷͼƬ����
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public string Desc { get; set; }
}
