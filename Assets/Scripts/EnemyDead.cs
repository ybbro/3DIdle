using UnityEngine;

// �ִϸ��̼� �̺�Ʈ�� ����ϱ� ���ؼ��� �ִϸ����Ϳ� ���� ������Ʈ�� �޼��带 ������ ��ũ��Ʈ�� �־�� ��..
public class EnemyDead : MonoBehaviour
{
    // �� ���� �ִϸ��̼� ���� ���� ȣ���Ͽ� �� ������Ʈ �ı�
    public void Dead()
    {
        Destroy(transform.parent.gameObject);
    }
}
