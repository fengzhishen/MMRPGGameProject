using UnityEngine;
using System.Collections;

public class NPCWorldMapData
{
    /// <summary>
    /// NPCId
    /// </summary>
   public int NPCId { get; set; }

    /// <summary>
    /// NPC位置
    /// </summary>
    public Vector3 NPCPostion { get; set; }

    /// <summary>
    /// NPC旋转
    /// </summary>
    public float EulerAngleY { get; set; }

    /// <summary>
    /// 开场白
    /// </summary>
    public string Prologue { get; set; }
}
