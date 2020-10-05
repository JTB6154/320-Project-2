using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSingleton : MonoBehaviour
{
    #region Singleton stuff
    private static ShopSingleton _instance;
    public static ShopSingleton Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ShopSingleton>();
                if(_instance == null)
                {
                    _instance = new GameObject().AddComponent<ShopSingleton>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
    #endregion


    public Buyable[] currentShop;


    public void RefreshShop()
    {

    }
}
