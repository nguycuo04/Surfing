using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinRotae : MonoBehaviour
{
   
    [SerializeField] private float rotateDuration= 1.0f; 
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
