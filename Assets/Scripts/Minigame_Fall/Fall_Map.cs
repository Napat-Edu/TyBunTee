using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Fall_Map : MonoBehaviour
{
    public Fall_Character player = null;
    public int posX = 0;
    public int posY = 0;
    private Color color;
    private Animator animator = null;

    [SerializeField] private SpriteRenderer marker;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        Fall_Player.main.Jump(this);
    }

    public void SetPos(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public void SetColor(Color color)
    {
        this.color = color;
        marker.color = color;
    }

    public bool CanMove(Fall_Map map)
    {
        int diffX = math.abs(posX - map.posX);
        int diffY = math.abs(posY - map.posY);

        return diffX + diffY == 1;
    }

    public bool isColor(Color color)
    {
        return this.color == color;
    }

    public void StepOn(Fall_Character player)
    {
        this.player = player;

        if (animator != null)
            animator.Play("StepOn");
    }

    public void Fall()
    {
        animator.Play("Fall");
    }

    public void Destroy()
    {
        if (player != null)
        {
            player.Fall(GetComponent<SortingGroup>().sortingOrder);
            player = null;
            Debug.Log("Destroy");
        }
        Destroy(gameObject);
    }
}
