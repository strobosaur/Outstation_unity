using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grass01Prefab : MonoBehaviour
{
    public Animator PropAnim;
    public float animationTimer;
    // Start is called before the first frame update

    void Start()
    {
        animationTimer = (Random.Range(0, 6) + 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer > 0) {
            animationTimer -= Time.deltaTime;
        } else {
            UpdateAnimation();
        }
    }

    public void UpdateAnimation()
    {        
        //animator.clip = SpriteAnim;
        //PropAnim.Play();
        animationTimer = (Random.Range(0, 6) + 3);
    }
}
