//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 21:45:22
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public static class GameObjectUtil 
{
    /// <summary>
    /// 获取或创建组建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetOrCreatComponent<T>(this GameObject obj) where T:MonoBehaviour
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }

    /// <summary>
    /// Text扩展方法
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="text"></param>
    public static void SetTextValue(this Text textObj,string text)
    {
        if(textObj != null)
        {
            //textObj.text = text;
            textObj.DOText(text, 1.5f,true, ScrambleMode.None);
        }
    }

    /// <summary>
    /// slider扩展方法
    /// </summary>
    /// <param name="sliderObj"></param>
    /// <param name="value"></param>
    public static void SetSliderValue(this Slider sliderObj,float value)
    {
        if(sliderObj != null)
        {
            sliderObj.value = value;
        }
    }

    public static Texture LoadGameLevelMapPic(string mapPicName)
    {
        string mapBgPicPath = @"UI\GameLevel\GameLevelMap\" + mapPicName;
        return Resources.Load<Texture>(mapBgPicPath);
    }

    /// <summary>
    /// 把游戏物体属性归一化 GameObject扩展方法
    /// </summary>
    public static void SetGameObjNormalTransfor(this GameObject @object,float localScaleRate = 1)
    {
        @object.transform.localPosition = Vector3.zero;
        @object.transform.localScale = Vector3.one * localScaleRate;
        @object.transform.localRotation = Quaternion.identity;
    }
}