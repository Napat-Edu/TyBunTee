using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Player : MonoBehaviour, Fall_Character
{
    public static Fall_Player main = null;

    [SerializeField] private Fall_MapManagement mapManagement;
    private Fall_Map currentMap = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
    private Vector3 target = Vector3.zero;
    private bool die = false;
    void Awake()
    {
        main = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = transform.position;
    }

    void Update()
    {
        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);

        if (up)
        {
            int y = currentMap.posY + 1;
            Jump(currentMap.posX, y);
        }
        else if (down)
        {
            int y = currentMap.posY - 1;
            Jump(currentMap.posX, y);
        }
        else if (left)
        {
            int x = currentMap.posX - 1;
            Jump(x, currentMap.posY);
        }
        else if (right)
        {
            int x = currentMap.posX + 1;
            Jump(x, currentMap.posY);
        }

        if (Vector3.Distance(transform.position, target) > 0.05f && !die)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 10);
        }


        if (die)
        {
            if (transform.position.y < -10)
            {
                Fall_GameManagement.isGameEnd = true;
                Destroy(gameObject);
            }
        }
    }

    public void Jump(Fall_Map map)
    {
        if (Fall_GameManagement.isPlaying == false || map.player != null)
            return;

        if (currentMap != null)
        {
            if (!map.CanMove(currentMap) || map == currentMap)
            {
                return;
            }
            currentMap.player = null;
            if (currentMap.posY == map.posY)
                spriteRenderer.flipX = currentMap.posX > map.posX;
        }

        StepOn(map);
    }

    public void Jump(int x, int y)
    {
        if (Fall_GameManagement.isPlaying == false)
            return;

        Fall_Map map = mapManagement.GetMap(x, y);
        if (map != null)
        {
            Jump(map);
        }
    }

    public void StepOn(Fall_Map map)
    {
        animator.Play("Jump");
        currentMap = map;
        map.StepOn(this);

        target = map.transform.position;
    }

    public void Fall(int layer)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layer;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        animator.Play("Fall");
        die = true;
    }

    public void Think()
    {
        animator.Play("Think");
    }
}

public interface Fall_Character
{
    void Fall(int layer);
}