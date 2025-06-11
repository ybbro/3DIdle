using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] ItemPanel itemInfoPanel;

    [SerializeField] int itemCount;

    [SerializeField] TextMeshProUGUI countText;

    [field:SerializeField] public ItemSO itemData {  get; private set; }

    public List<Action<float>> ItemEffects = new List<Action<float>>();

    private void Start()
    {
        // 아이템 수량이 없다면 아이템, 수량 표시 비활성화
        if (itemCount <= 0)
        {
            gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }

        // 아이템 아이콘 교체
        if (TryGetComponent(out Image icon))
            icon.sprite = itemData.icon;
        else
            Debug.Log("이미지 컴포넌트가 필요합니다.");

        // 아이템이 영향을 줄 스텟에 대한 접근 메서드들을 등록
        for (int i = 0; i < itemData.effect.Length; i++)
        {
            switch (itemData.effect[i].targetStatus)
            {
                case StatusType.HP:
                    ItemEffects.Add(GameManager.instance.player.Heal);
                    break;
                case StatusType.Atk:
                    ItemEffects.Add(GameManager.instance.player.AtkChange);
                    break;
                    // 아이템이 영향을 줄 스테이터스가 많아지면 여기에 추가
                default:
                    break;
            }
        }
    }

    public void Effect()
    {
        // 소모하고 텍스트로 표시
        itemCount--;
        countText.text = itemCount.ToString();

        // 효과 발생
        for (int i = 0; i < itemData.effect.Length; i++)
        {
            float effectTime = itemData.effect[i].effectTime;
            if (effectTime == 0)
                ItemEffects[i].Invoke(itemData.effect[i].changeAmount);
            // HP라면 지속회복 (MP도 만든다면 여기에 추가)
            else if(itemData.effect[i].targetStatus == StatusType.HP)
                StartCoroutine(LastingHeal(ItemEffects[i], itemData.effect[i]));
            // 나머지 스테이터스는 일정 시간 동안 버프/디버프를 줬다가 원상태로
            else
                StartCoroutine(Buff(ItemEffects[i], itemData.effect[i]));
        }

        // 아이템을 다 썼다면 슬롯에서 아이템 항목이 비활성화 되게끔
        if (itemCount <= 0)
        {
            gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }
    }


    // 지속 힐(HP, MP 등 지속적으로 차는 효과)
    IEnumerator LastingHeal(Action<float> itemEffectMethod, StatusWhichItemEffects itemEffectInfo)
    {
        float lastTime = itemEffectInfo.effectTime;
        float changeTime = 0.1f;

        while (lastTime > 0)
        {
            itemEffectMethod.Invoke(itemEffectInfo.changeAmount);
            yield return new WaitForSeconds(changeTime);
            lastTime -= changeTime;
        }
    }

    // 이걸로 버프/디버프를 한번에 해결할 수 있지 않을까?
    IEnumerator Buff(Action<float> itemEffectMethod, StatusWhichItemEffects itemEffectInfo)
    {
        // 아이템의 속성만큼 변화했다가
        itemEffectMethod(itemEffectInfo.changeAmount);
        // 버프 지속 시간만큼 대기
        yield return new WaitForSeconds(itemEffectInfo.effectTime);
        // 변화했던 값을 원상 복구
        itemEffectMethod(-itemEffectInfo.changeAmount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 아이템 정보 변경
        itemInfoPanel.nameText.text = itemData.itemName;
        itemInfoPanel.descriptionText.text = itemData.description;

        // 패널을 아이템 슬롯 옆으로 이동 및 활성화
        itemInfoPanel.transform.position = transform.position;
        itemInfoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 패널 비활성화
        itemInfoPanel.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭을 했다면 아이템 사용
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Effect();
        }
    }
}
