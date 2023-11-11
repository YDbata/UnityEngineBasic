using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam_TraceLead : MonoBehaviour
{
    private CinemachineVirtualCameraBase _vCam;

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCameraBase>();

    }

    private void LateUpdate()
    {
        _vCam.LookAt = _vCam.Follow = PlayManager.instance.lead.transform;

    }
}
