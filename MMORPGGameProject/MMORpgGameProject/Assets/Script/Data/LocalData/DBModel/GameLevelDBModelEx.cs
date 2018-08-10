using UnityEngine;
using System.Collections.Generic;

public partial class GameLevelDBModel
{
    private List<GameLevelEntity> m_gameLevelEntites = new List<GameLevelEntity>();
    /// <summary>
    /// �����±�Ż�ȡ�ؿ�����
    /// </summary>
    /// <param name="chapterId"></param>
    /// <returns></returns>
    public List<GameLevelEntity> GetListByChapterId(int chapterId)
    {
        if (GetDataList.Count == 0 || GetDataList == null) return null;

        for (int i = 0; i < GetDataList.Count; i++)
        {
            if(GetDataList[i].ChapterID == chapterId)
            {
                m_gameLevelEntites.Insert(i, GetDataList[i]);
            }
        }

        return m_gameLevelEntites;
    }
}
