using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    /// <summary>
    /// 저장시 데이터를 파싱해서 반환한다.
    /// </summary>
    /// <returns></returns>
    object CaptureState();

    /// <summary>
    /// 로드할때 저장된 데이터를 가져와서 파싱하단.
    /// </summary>
    /// <param name="state">저장된 상태 데이터</param>
    void RestoreState(object state);
}
