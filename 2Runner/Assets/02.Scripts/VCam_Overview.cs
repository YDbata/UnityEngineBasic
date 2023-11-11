using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam_Overview : MonoBehaviour
{
    private CinemachineVirtualCameraBase _vCam;

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCameraBase>();

    }

    private void OnEnable()
    {
        _vCam.LookAt = _vCam.Follow = PlayManager.instance.lead.transform;
        
    }
}
