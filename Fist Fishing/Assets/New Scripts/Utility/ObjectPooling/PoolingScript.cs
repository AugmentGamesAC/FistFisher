using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolingScript : MonoBehaviour
{
    protected GenericObjectPool m_pool; 
    protected virtual void MyDisable()
    {
        if (m_pool == default)
            return;
        m_pool.Deactivated(this);
    }
    protected virtual void MyEnable() { }
    
    public void PoolInit(GenericObjectPool myPool)
    {
        m_pool = myPool;
    }
    public void OnEnable() { MyEnable(); }
    public void OnDisable() { MyDisable(); }
}
