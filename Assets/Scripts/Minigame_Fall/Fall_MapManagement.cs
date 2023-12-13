using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Fall_MapManagement : MonoBehaviour
{
    private Fall_Map[,] maps = null;
    [SerializeField] private Fall_Map mapClone = null;
    [SerializeField] private Fall_Color colorManagement;

    public Fall_Map GetMap(int x, int y)
    {
        try
        {
            return maps[x, y];
        }
        catch
        {
            return null;
        }
    }

    public Fall_Map GetMapNoPlayer()
    {
        int infinity = 0;

        while (true)
        {
            int x = Random.Range(0, maps.GetLength(0));
            int y = Random.Range(0, maps.GetLength(1));

            if (maps[x, y].player == null)
                return maps[x, y];

            infinity++;
            if (infinity > 1000)
                break;
        }

        return null;
    }

    public void DeleteMap(int x, int y)
    {
        Destroy(maps[x, y].gameObject);
        maps[x, y] = null;
    }

    public void CreateEasyMap()
    {
        float[] map = new float[] { -1.1f, 0, 1.1f };
        CreateMap(map, 3);
    }

    public void CreateNormalMap()
    {
        float[] map = new float[] { -1.65f, -0.55f, 0.55f, 1.65f };
        CreateMap(map, 4);
    }


    public void CreateHardMap()
    {
        float[] map = new float[] { -2.2f, -1.1f, 0, 1.1f, 2.2f };
        CreateMap(map, 5);
    }

    void CreateMap(float[] mapPos, int size)
    {
        maps = new Fall_Map[size, size];
        colorManagement.Init(size * size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject map = Instantiate(mapClone, transform).gameObject;
                map.transform.localPosition = new Vector3(mapPos[i], mapPos[j], 0);
                map.GetComponent<SortingGroup>().sortingOrder = -j - 1;

                maps[i, j] = map.GetComponent<Fall_Map>();
                maps[i, j].SetPos(i, j);
                maps[i, j].SetColor(colorManagement.GetMapColor());
            }
        }
    }

    public void DestroyMap()
    {
        Color color = colorManagement.GetCurrent();
        foreach (Fall_Map map in maps)
        {
            if (map != null)
            {
                if (map.isColor(color))
                {
                    map.Fall();
                }
            }
        }

        colorManagement.Next();
    }
}
