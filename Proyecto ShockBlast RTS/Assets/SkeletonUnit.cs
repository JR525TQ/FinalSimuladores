using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkeletonUnit : MonoBehaviour
{

    public VisualEffect healEffect;

    // Start is called before the first frame update
    void Start()
    {
        healEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
