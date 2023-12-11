using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Camera : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 max;
    [SerializeField] private Vector2 min;
    void Start()
    {

    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, player.position.x, speed * Time.deltaTime);
        pos.y = Mathf.Lerp(pos.y, player.position.y+1, speed * Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }
}
