//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-06 07:43:41
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 世界地图场景控制器
/// </summary>
public class WorldMapSceneCtrl : MonoBehaviour 
{
    /// <summary>
    /// 主角出生点  临时使用 真正的时候是策划在Excel表中配置的
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;

    private UISceneMainCityView UISceneMainCityView;

    private WorldMapEntity worldMapEntity;

    void Awake()
    {
        UISceneCtr.Instance.CurrentUIScene = UISceneMainCityView;

        UISceneMainCityView = UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.MainCity,OnLoadComplete).GetComponent<UISceneMainCityView>();

        UISceneCtr.Instance.CurrentUIScene = UISceneMainCityView;

        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnZoom += OnZoom;
            FingerEvent.Instance.OnPlayerClick += OnPlayerClick;
        }
    }

    /// <summary>
    /// 当资源加载完毕后
    /// </summary>
    private void OnLoadComplete()
    {
        PlayerCtr.Instance.SetMainCityRoleInfo();
    }

    void Start()
    {
        
        if(DelegateDefine.Instance.OnSceneLoadOk != null)
        {
            DelegateDefine.Instance.OnSceneLoadOk();
        }

        if (GlobalInit.Instance == null) return;

        RoleMgr.Instance.InitMainPlayer(m_PlayerBornPos);

        if(GlobalInit.Instance.CurrPlayer != null)
        {
             worldMapEntity = WorldMapDBModel.GetInstance.GetEntityById(SceneMgr.Instance.CurrWorldMapId);

            if(worldMapEntity != null)
            {
                GlobalInit.Instance.CurrPlayer.gameObject.transform.position = worldMapEntity.RoleBirthPosition;

                GlobalInit.Instance.CurrPlayer.gameObject.transform.eulerAngles = new Vector3(0,worldMapEntity.RoleBirthY,0);

            }
            else
            {
                //没有得到表格中的坐标就用临时的 
                GlobalInit.Instance.CurrPlayer.gameObject.transform.position = m_PlayerBornPos.position;
            }

        }

        StartCoroutine(InitNPC());
    }

    private IEnumerator InitNPC()
    {
        yield return null;

        if (worldMapEntity == null) yield break;

        for (int i = 0; i < worldMapEntity.NPCWorldMapList.Count; i++)
        {
            NPCWorldMapData data = worldMapEntity.NPCWorldMapList[i];

            NPCEntity nPCEntity = NPCDBModel.GetInstance.GetEntityById(data.NPCId);

            GameObject @object = RoleMgr.Instance.LoadNPC(nPCEntity.PrefabName);

            @object.transform.position = data.NPCPostion;

            @object.transform.eulerAngles = new Vector3(0,data.EulerAngleY,0);

            @object.GetComponent<NPCCtr>().Init(data, @object);
        }
    }

    #region OnZoom 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="obj"></param>
    private void OnZoom(FingerEvent.ZoomType obj)
    {
        switch (obj)
        {
            case FingerEvent.ZoomType.In:
                CameraCtrl.Instance.SetCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraCtrl.Instance.SetCameraZoom(1);
                break;
        }
    }
    #endregion

    #region OnPlayerClickGround 玩家点击
    /// <summary>
    /// 玩家点击
    /// </summary>
    private void OnPlayerClick()
    {
        //防止UI穿透
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        RaycastHit[] hitArr = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
        if (hitArr.Length > 0)
        {
            RoleCtrl hitRole = hitArr[0].collider.gameObject.GetComponent<RoleCtrl>();
            if (hitRole.CurrRoleType == RoleType.Monster)
            {
                GlobalInit.Instance.CurrPlayer.LockEnemy = hitRole;
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag.Equals("Road", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    if (GlobalInit.Instance.CurrPlayer != null)
                    {
                        GlobalInit.Instance.CurrPlayer.LockEnemy = null;
                        GlobalInit.Instance.CurrPlayer.MoveTo(hitInfo.point);
                    }
                }
            }
        }
    }
    #endregion

    #region OnFingerDrag 手指滑动
    /// <summary>
    /// 手指滑动
    /// </summary>
    /// <param name="obj"></param>
    private void OnFingerDrag(FingerEvent.FingerDir obj)
    {
        switch (obj)
        {
            case FingerEvent.FingerDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
            case FingerEvent.FingerDir.Up:
                CameraCtrl.Instance.SetCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraCtrl.Instance.SetCameraUpAndDown(0);
                break;
        }
    }
    #endregion

    #region OnDestroy 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
            FingerEvent.Instance.OnZoom -= OnZoom;
            FingerEvent.Instance.OnPlayerClick -= OnPlayerClick;
        }
    }
    #endregion
}