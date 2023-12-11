using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Bot : MonoBehaviour, Fall_Character
{
    public Fall_Map currentMap = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
    private Vector3 target;
    private bool die = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = transform.position;
    }
    void Start()
    {

    }
    void Update()
    {
        if (Vector3.Distance(transform.position, target) > 0.05f && !die)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 10);
        }

        if (die)
        {
            if (transform.position.y < -20)
            {
                Destroy(gameObject);
                Fall_GameManagement.main.StartGame();
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

    public void StepOn(Fall_Map map)
    {
        target = map.transform.position;
        animator.Play("BotJump");
        currentMap = map;
        map.StepOn(this);
    }

    public void Fall(int layer)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layer;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        animator.Play("BotFall");
        die = true;
    }
}
