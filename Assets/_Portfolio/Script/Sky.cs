using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public Material[] SkyV;

    static Material SelectMaterial;

    [SerializeField]
    Skybox Camara;

    // Start is called before the first frame update
    void Start()
    {
        if(SelectMaterial != null)
        {
            Camara.material = SelectMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Midday()
    {
        SelectMaterial = SkyV[0];
    }

    public void Sunset()
    {
        SelectMaterial = SkyV[1];
    }

    public void Daybreak()
    {
        SelectMaterial = SkyV[2];
    }
}
