using UnityEngine;
using System.Collections;

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
	
}
