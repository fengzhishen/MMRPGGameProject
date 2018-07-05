using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WorldMapEntity 
{
    /// <summary>
    /// 得到角色的出生地
    /// </summary>
    public Vector3 RoleBirthPosition
    {
        get
        {
            string[] arr = RoleBirthPos.Split('_');

            if(arr.Length < 3)
            {
                return Vector3.zero;
            }

            float x , y ,z;

            x = y = z = 0.0f;

            x = float.Parse(arr[0]);

            y = float.Parse(arr[1]);

            z = float.Parse(arr[2]);

            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// 得到角色Y轴旋转角度
    /// </summary>
    public float RoleBirthY
    {
        get
        {
            string[] arr = RoleBirthPos.Split('_');
            
            if(arr.Length < 4)
            {
                return 0.0F;
            }

            return float.Parse(arr[3]);
        }
    }

    private List<NPCWorldMapData> m_NpcWorldMapList = new List<NPCWorldMapData>();

    /// <summary>
    /// 得到NPC世界地图数据
    /// </summary>
    public IList<NPCWorldMapData> NPCWorldMapList
    {
        get
        {
            if (m_NpcWorldMapList.Count > 0) return m_NpcWorldMapList;
            if (this.NPCList == null) return null;

            //开始从字符串提取NPC数据到实体类中
            string[] arr1 = this.NPCList.Split('|');

            foreach (string item in arr1)
            {
                string[] arr2 = item.Split('_');

                if(arr2.Length < 6)
                {
                    return null;
                }

                int npcId = 0;

                int.TryParse(arr2[0], out npcId);

                float x = 0, y = 0, z = 0,angleY = 0;

                x = float.Parse(arr2[1]);

                y = float.Parse(arr2[2]);

                z = float.Parse(arr2[3]);

                angleY = float.Parse(arr2[4]);

                string s = arr2[5];

                NPCWorldMapData nPCWorldMapData = new NPCWorldMapData()
                {
                    NPCId = npcId,
                    NPCPostion = new Vector3(x, y, z),
                    EulerAngleY = angleY,
                    Prologue = s
                };

               m_NpcWorldMapList.Add(nPCWorldMapData);
            }

            return m_NpcWorldMapList;
        }
    }
	
}
