using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using GLOBALS;

public class fxPuffControl : MonoBehaviour
{
    public float ymod;
    public float spdMod;
    public float ground;
    public int clip;
    private Animator _animator;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        ymod = (Random.Range(1,4) / Globals.G_CELLSIZE) / 24f;
        spdMod = Random.Range(0.075f, 0.25f);
        ground = transform.position.y + (2 / Globals.G_CELLSIZE);
        _renderer.flipX = Convert.ToBoolean(Random.Range(0,2));
        clip = Random.Range(0,3);        
        _animator.SetInteger("clip", clip);
        _animator.speed = (0.35f + spdMod);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + ymod, transform.position.z);
    }
}
