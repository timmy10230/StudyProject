using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject doorPre; 
    private int X, Y;
    private List<Vector2> nullPointList = new List<Vector2>();
    private List<Vector2> superWallPointList = new List<Vector2>();
    private GameObject door;

    private Dictionary<ObjectType, List<GameObject>> poolObjectDic =
        new Dictionary<ObjectType, List<GameObject>>();

    public bool IsSuperWall(Vector2 pos)
    {
        if (superWallPointList.Contains(pos))
            return true;
        else
            return false;
    }

    public Vector2 GetPlayerPos()
    {
        return new Vector2(-(X + 1), Y - 1);
    }

    private void Recovery()
    {
        nullPointList.Clear();
        superWallPointList.Clear();
        foreach (var item in poolObjectDic)
        {
            foreach (var obj in item.Value)
            {
                ObjectPool.Instance.Add(item.Key, obj);
            }
        }
        poolObjectDic.Clear();
    }

    public void InitMap(int x, int y, int wallCount, int enemyCount)
    {
        Recovery();

        X = x;
        Y = y;
        CreateSuperWall();
        FindNullPoints();
        CreateWall(wallCount);
        CreatDoor();
        CreateProp();
        CreateEnemy(enemyCount);
    }

    private void CreateSuperWall()
    {
        for (int x = -X; x < X; x += 2)
        {
            for (int y = -Y; y < Y; y += 2)
            {
                SpawnSuperWall(new Vector2(x, y));
            }
        }

        for (int x = -(X + 2); x <= X; x++)
        {
            SpawnSuperWall(new Vector2(x, Y));
            SpawnSuperWall(new Vector2(x, -Y - 2));
        }

        for (int y = -(Y + 1); y < Y; y++)
        {
            SpawnSuperWall(new Vector2(-(X + 2), y));
            SpawnSuperWall(new Vector2(X, y));
        }
    }

    private void SpawnSuperWall(Vector2 pos)
    {
        superWallPointList.Add(pos);
        GameObject superWall = ObjectPool.Instance.Get(ObjectType.SuperWall,pos);
        if (poolObjectDic.ContainsKey(ObjectType.SuperWall) == false)
            poolObjectDic.Add(ObjectType.SuperWall, new List<GameObject>());
        poolObjectDic[ObjectType.SuperWall].Add(superWall);
    }

    private void FindNullPoints()
    {
        for (int x = -(X + 1); x <= X - 1; x++)
        {
            if (-(X + 1) % 2 == x % 2)
            {
                for (int y = -(Y + 1); y <= Y - 1; y++)
                {
                    nullPointList.Add(new Vector2(x, y));
                }
            }
            else
            {
                for (int y = -(Y + 1); y <= Y - 1; y += 2)
                {
                    nullPointList.Add(new Vector2(x, y));
                }
            }
        }
        nullPointList.Remove(new Vector2(-(X + 1), Y - 1));
        nullPointList.Remove(new Vector2(-(X + 1), Y - 2));
        nullPointList.Remove(new Vector2(-X, Y - 1));
    }

    private void CreateWall(int wallCount)
    {
        if (wallCount >= nullPointList.Count)
        {
            wallCount = (int)(nullPointList.Count * 0.7f);
        }
        for (int i = 0; i < wallCount; i++)
        {
            int index = Random.Range(0, nullPointList.Count);
            GameObject wall = ObjectPool.Instance.Get(ObjectType.Wall, nullPointList[index]);
            nullPointList.RemoveAt(index);

            if (poolObjectDic.ContainsKey(ObjectType.Wall) == false)
                poolObjectDic.Add(ObjectType.Wall, new List<GameObject>());
            poolObjectDic[ObjectType.Wall].Add(wall);
        }
    }

    private void CreatDoor()
    {
        if(door == null)
            door = Instantiate(doorPre, transform);
        door.GetComponent<Door>().ResetDoor();
        int index = Random.Range(0, nullPointList.Count);
        door.transform.position = nullPointList[index];
        nullPointList.RemoveAt(index);
    }

    private void CreateProp()
    {
        int count = Random.Range(0, 2 + (int)(nullPointList.Count * 0.05f));
        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, nullPointList.Count);
            GameObject prop = ObjectPool.Instance.Get(ObjectType.Prop, nullPointList[index]);
            nullPointList.RemoveAt(index);

            if (poolObjectDic.ContainsKey(ObjectType.Prop) == false)
                poolObjectDic.Add(ObjectType.Prop, new List<GameObject>());
            poolObjectDic[ObjectType.Prop].Add(prop);
        }
    }

    private void CreateEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, nullPointList.Count);
            GameObject enemy = ObjectPool.Instance.Get(ObjectType.Enemy, nullPointList[index]);
            enemy.GetComponent<EnemyAI>().Init();
            nullPointList.RemoveAt(index);

            if (poolObjectDic.ContainsKey(ObjectType.Enemy) == false)
                poolObjectDic.Add(ObjectType.Enemy, new List<GameObject>());
            poolObjectDic[ObjectType.Enemy].Add(enemy);
        }
    }
}
