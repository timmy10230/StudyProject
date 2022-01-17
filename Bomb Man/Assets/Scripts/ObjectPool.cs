using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    SuperWall,
    Wall,
    Prop,
    Bomb,
    Enemy,
    BombEffect
}

[System.Serializable]
public class Type_Prefab
{
    public ObjectType type;
    public GameObject prefab;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public List<Type_Prefab> type_Prefabs = new List<Type_Prefab>();

    private GameObject GetPreByType(ObjectType type)
    {
        foreach (var item in type_Prefabs)
        {
            if (item.type == type)
                return item.prefab;
        }
        return null;
    }

    private Dictionary<ObjectType, List<GameObject>> dic = 
        new Dictionary<ObjectType, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Get(ObjectType type,Vector2 pos)
    {
        GameObject temp = null;
        if (dic.ContainsKey(type) == false)
            dic.Add(type, new List<GameObject>());
        if(dic[type].Count > 0)
        {
            int index = dic[type].Count - 1;
            temp = dic[type][index];
            dic[type].RemoveAt(index);
        }
        else
        {
            GameObject pre = GetPreByType(type);
            if(pre != null)
            {
                temp = Instantiate(pre, transform);
            }

        }
        temp.SetActive(true);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        return temp;
    }

    public void Add(ObjectType type, GameObject go)
    {
        if(dic.ContainsKey(type)&&dic[type].Contains(go) == false)
        {
            dic[type].Add(go);
        }
        go.SetActive(false);
    }
}
