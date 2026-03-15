using UnityEngine;

public class QuestStartCollider : MonoBehaviour
{
    [SerializeField] private Quests _questParent;


    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Cable"))
        {
            if(GameManager.Instance.Cable.CurrentCurrentState != _questParent.CurrentCableState)
            {
                _questParent.CableControl.SetCableState(_questParent.CurrentCableState);
            }
            
        }
    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Cable"))
        {
            if(GameManager.Instance.Cable.CurrentCurrentState != Cable.CableState.none)
            {
                _questParent.CableControl.SetCableState(Cable.CableState.none);
            }
            
        }
    }

}
