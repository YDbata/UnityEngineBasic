using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour 
    // MonoBehaviour�� ��ü�� Ư¡���� ��ũ��Ʈ�� ��ü�� �߰��� �� �ִ�.
    // Script�� ScriptInstance�� �ٸ���.
    // Script���� ��ü�� ������ ScriptInstance�� �ȴ�.
{

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity!");
/*        GetComponent<RectTransform>().GetChild(0).GetComponent<Slider>();
        // �� ������Ʈ�� ������ GameObject�� ������ ������Ʈ�� �߿� �ش� Ÿ���� ã�Ƽ� ��ȯ
        GetComponents<RectTransform>();

        // ��� �ڽĵ� ��������
        GetComponentInChildren<Slider>();
        GetComponentsInChildren<Slider>();*/
        
        _hpBar.minValue = 0.0f;
        _hpBar.maxValue = _player.hpMax;
        _hpBar.value = _player.hp;
        // lambda�� �����
        // _player.OnHpChanged += RefreshHPBar;

        // lambda�� ��� ��
        // inline �Լ� : �Լ� ������带 ���̱� ����(�Լ��� ����� ����ϴµ� �ȿ� ���뿡 ���� ����� ���� ��)
        // ������ lambda������ inline�� �ۼ�
        // C#�� inline�Լ��� lambda������ �����Ѵ�.
        // lambda : �����Ϸ��� �Ǵ��� �� �ִ� �ڵ带 ��� �����ϰ� �̸��� ������ �Լ���

        _player.OnHpChanged += value => _hpBar.value = value;
        

       
        
    }

    // lambda ���� �ۼ��� �Լ�
    // 1. �ζ����Լ��� ���������ڰ� �ǹ� �����Ƿ� private����
    // 2. �����Ϸ��� �븮���� ������ float�Ķ���� 1���� void��ȯ�̹Ƿ� void �� floatŸ�� ����
    // 3. �ζ����̹Ƿ� �̸����� �Լ��˻��� ���� �����Ƿ� �̸� ����
    // 4. ������ �����̸� �״����� �ݵ�� �Լ� ������ �Ͼ���ϹǷ� �Լ� ���� ���� �ʿ� �����Ƿ� �߰�ȣ ����
    // 5. ���ٽ� ��ø� ���� => �߰�
   /* private void RefreshHPBar(float value)
    {
        _hpBar.value =  value;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
