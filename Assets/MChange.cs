using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MChange : MonoBehaviour
{
    private SpriteRenderer SpritesRenderer;
    public List<Sprite> Sprites = new List<Sprite>();

    private int Timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        SpritesRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer--;
        if (Timer <= 0)
        {
            SpritesRenderer.sprite = Sprites[1];
        }
        //if (LIfe == 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
