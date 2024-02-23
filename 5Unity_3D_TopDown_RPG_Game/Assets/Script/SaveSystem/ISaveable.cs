using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    /// <summary>
    /// ����� �����͸� �Ľ��ؼ� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    object CaptureState();

    /// <summary>
    /// �ε��Ҷ� ����� �����͸� �����ͼ� �Ľ��ϴ�.
    /// </summary>
    /// <param name="state">����� ���� ������</param>
    void RestoreState(object state);
}
