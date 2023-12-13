using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_BotManagement : MonoBehaviour
{
    [SerializeField] private Fall_Bot botClone = null;
    [SerializeField] private Fall_MapManagement mapManagement;
    private List<Fall_Bot> bots = new List<Fall_Bot>();

    void Start()
    {

    }

    void Update()
    {
        if (Count == 0 && !Fall_GameManagement.isGameEnd)
        {
            Fall_GameManagement.isGameEnd = true;
        }
    }

    // Update is called once per frame
    public void CreateBot(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Fall_Bot bot = Instantiate(botClone, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Fall_Bot>();
            bot.StepOn(mapManagement.GetMapNoPlayer());
            bot.index = i + 1;
            bots.Add(bot);
        }
    }

    public void BotMove()
    {
        List<Fall_Bot> remove = new List<Fall_Bot>();

        foreach (Fall_Bot bot in bots)
        {
            if (bot == null || bot.currentMap == null)
            {
                remove.Add(bot);
                continue;
            }

            int x, y;
            int dir = Random.Range(0, 5);
            switch (dir)
            {
                case 0:
                    x = bot.currentMap.posX;
                    y = bot.currentMap.posY + 1;
                    break;
                case 1:
                    x = bot.currentMap.posX;
                    y = bot.currentMap.posY - 1;
                    break;
                case 2:
                    x = bot.currentMap.posX + 1;
                    y = bot.currentMap.posY;
                    break;
                case 3:
                    x = bot.currentMap.posX - 1;
                    y = bot.currentMap.posY;
                    break;
                default:
                    x = bot.currentMap.posX;
                    y = bot.currentMap.posY;
                    break;
            }

            Fall_Map map = mapManagement.GetMap(x, y);
            if (map != null && map.player == null)
            {
                bot.Jump(map);
            }
        }

        foreach (Fall_Bot bot in remove)
        {
            bots.Remove(bot);
        }
    }

    public int Count
    {
        get
        {
            return bots.Count;
        }
    }
}
