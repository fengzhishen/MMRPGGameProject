using UnityEngine;
using System.Collections;

public class SingletonMMO<T> : MonoBehaviour where T: MonoBehaviour
{
    #region µ¥ÊµÀý

    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (new GameObject(typeof(T).Name)).AddComponent<T>();

                DontDestroyOnLoad(m_instance.gameObject);
            }

            return m_instance;
        }
    }

    #endregion

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void OnDestory()
    {
        OnDestory();
    }
    void Update()
    {
        OnUpdate();
    }

    protected virtual void OnAwake() { }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void BeforeOnDestroy() { }

}
