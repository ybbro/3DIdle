using UnityEngine;

// 애니메이션 이벤트에 사용하기 위해서는 애니메이터와 같은 오브젝트에 메서드를 포함한 스크립트가 있어야 함..
public class EnemyDead : MonoBehaviour
{
    // 적 죽음 애니메이션 끝난 직후 호출하여 적 오브젝트 파괴
    public void Dead()
    {
        Destroy(transform.parent.gameObject);
    }
}
