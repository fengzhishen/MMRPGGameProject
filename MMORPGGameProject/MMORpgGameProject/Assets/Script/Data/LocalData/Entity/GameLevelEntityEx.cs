using UnityEngine;
using System.Collections;

public partial class GameLevelEntity
{
    Vector3 vector3 = Vector3.zero;
    public Vector3 GetGameLevelPosition
    {
        get
        {
            if(vector3 == Vector3.zero)
            {
                string[] strPos = this.PosInMap.Split('_');
                vector3 = new Vector3(float.Parse(strPos[0]), float.Parse(strPos[1]));
            }
            return vector3;
        }
    }
}
