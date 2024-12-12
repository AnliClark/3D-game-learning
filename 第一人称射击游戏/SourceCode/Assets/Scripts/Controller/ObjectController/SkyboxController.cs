using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skyboxMaterial; // ���ɲ���
    public float skyboxTransSpeed = 0.005f; // �����ٶ�

    private int skyboxCount = 5;
    private float skyRange = 0f;

    void Start()
    {
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }
    }

    void Update()
    {
        skyRange += skyboxTransSpeed;
        skyboxMaterial.SetFloat("_SkyRange", skyRange);
        if (skyRange >= skyboxCount)
        {
            skyRange = 0f;
            skyboxMaterial.SetFloat("_SkyRange", skyRange);
        }
    }
}
