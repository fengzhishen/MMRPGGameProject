using UnityEngine;
using System.Collections;

public partial class GameLevelGradeDBModel
{

	public GameLevelGradeEntity GetEntityByGameLevelIdAndGrade(int gameLevelId,GameLevelGrade grade)
    {
        for (int i = 0; i < GetDataList.Count; i++)
        {
            if(GetDataList[i].Grade == (int)grade && GetDataList[i].GameLevelId == gameLevelId)
            {
                return GetDataList[i];
            }
        }
        return null;
    }
}
