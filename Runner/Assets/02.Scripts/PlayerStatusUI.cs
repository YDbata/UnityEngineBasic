using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour 
    // MonoBehaviour는 객체의 특징으로 스크립트를 객체에 추가할 수 있다.
    // Script와 ScriptInstance는 다르다.
    // Script에서 객체에 넣으면 ScriptInstance가 된다.
{

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity!");
/*        GetComponent<RectTransform>().GetChild(0).GetComponent<Slider>();
        // 이 컴포넌트를 가지는 GameObject가 가지는 컴포넌트들 중에 해당 타입을 찾아서 반환
        GetComponents<RectTransform>();

        // 모든 자식들 가져오기
        GetComponentInChildren<Slider>();
        GetComponentsInChildren<Slider>();*/
        
        _hpBar.minValue = 0.0f;
        _hpBar.maxValue = _player.hpMax;
        _hpBar.value = _player.hp;
        // lambda식 사용전
        // _player.OnHpChanged += RefreshHPBar;

        // lambda식 사용 후
        // inline 함수 : 함수 오버헤드를 줄이기 위해(함수를 만들고 사용하는데 안에 내용에 비해 비용이 많이 들어감)
        // 때문에 lambda식으로 inline에 작성
        // C#의 inline함수는 lambda식으로 구현한다.
        // lambda : 컴파일러가 판단할 수 있는 코드를 모두 생략하고 이름을 생략한 함수식

        _player.OnHpChanged += value => _hpBar.value = value;
        

       
        
    }

    // lambda 전에 작성한 함수
    // 1. 인라인함수는 접근제한자가 의미 없으므로 private생략
    // 2. 구독하려는 대리자의 형식이 float파라미터 1개와 void반환이므로 void 및 float타입 생략
    // 3. 인라인이므로 이름으로 함수검색할 일이 없으므로 이름 생략
    // 4. 구현부 한주이면 그다음은 반드시 함수 리턴이 일어나야하므로 함수 범위 지정 필요 없으므로 중괄호 생략
    // 5. 람다식 명시를 위해 => 추가
   /* private void RefreshHPBar(float value)
    {
        _hpBar.value =  value;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
