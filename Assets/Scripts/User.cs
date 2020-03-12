using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string userName;
    public User()
    {
        userName = FirebaseSaveData.countryAdded;
        Debug.Log(userName);
    }
}
