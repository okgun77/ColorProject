using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public copymotion[] copy;

    private void Start()
    {
        //Invoke("TestInit",1f);
        TestInit();
    }

    private void TestInit()
    {
        copy[0].Init();
        copy[1].Init();
        copy[2].Init();
        copy[3].Init();
      
    }
}
