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
        // ������ ������ ���ٸ� ������, ���� ǥ�� ��Ȱ��ȭ
        if (itemCount <= 0)
        {
            gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }

        // ������ ������ ��ü
        if (TryGetComponent(out Image icon))
            icon.sprite = itemData.icon;
        else
            Debug.Log("�̹��� ������Ʈ�� �ʿ��մϴ�.");

        // �������� ������ �� ���ݿ� ���� ���� �޼������ ���
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
                    // �������� ������ �� �������ͽ��� �������� ���⿡ �߰�
                default:
                    break;
            }
        }
    }

    public void Effect()
    {
        // �Ҹ��ϰ� �ؽ�Ʈ�� ǥ��
        itemCount--;
        countText.text = itemCount.ToString();

        // ȿ�� �߻�
        for (int i = 0; i < itemData.effect.Length; i++)
        {
            float effectTime = itemData.effect[i].effectTime;
            if (effectTime == 0)
                ItemEffects[i].Invoke(itemData.effect[i].changeAmount);
            // HP��� ����ȸ�� (MP�� ����ٸ� ���⿡ �߰�)
            else if(itemData.effect[i].targetStatus == StatusType.HP)
                StartCoroutine(LastingHeal(ItemEffects[i], itemData.effect[i]));
            // ������ �������ͽ��� ���� �ð� ���� ����/������� ��ٰ� �����·�
            else
                StartCoroutine(Buff(ItemEffects[i], itemData.effect[i]));
        }

        // �������� �� ��ٸ� ���Կ��� ������ �׸��� ��Ȱ��ȭ �ǰԲ�
        if (itemCount <= 0)
        {
            gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }
    }


    // ���� ��(HP, MP �� ���������� ���� ȿ��)
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

    // �̰ɷ� ����/������� �ѹ��� �ذ��� �� ���� ������?
    IEnumerator Buff(Action<float> itemEffectMethod, StatusWhichItemEffects itemEffectInfo)
    {
        // �������� �Ӽ���ŭ ��ȭ�ߴٰ�
        itemEffectMethod(itemEffectInfo.changeAmount);
        // ���� ���� �ð���ŭ ���
        yield return new WaitForSeconds(itemEffectInfo.effectTime);
        // ��ȭ�ߴ� ���� ���� ����
        itemEffectMethod(-itemEffectInfo.changeAmount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� ����
        itemInfoPanel.nameText.text = itemData.itemName;
        itemInfoPanel.descriptionText.text = itemData.description;

        // �г��� ������ ���� ������ �̵� �� Ȱ��ȭ
        itemInfoPanel.transform.position = transform.position;
        itemInfoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �г� ��Ȱ��ȭ
        itemInfoPanel.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ��Ŭ���� �ߴٸ� ������ ���
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Effect();
        }
    }
}
