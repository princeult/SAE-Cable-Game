
using UnityEngine;

public class QuestEndCollider : MonoBehaviour
{
    [SerializeField] private Quests _questParent;
    [SerializeField] private ParticleSystem _particleSystem;


    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Cable")) // keep checking if cable is in correct state to complete quest (state set by QuestStartCollider)
        {
            if(GameManager.Instance.CableInstance.CurrentCurrentState == _questParent.CurrentCableState)
            {
               _questParent.CableControl.SetCableState(_questParent.CurrentCableState);
                _questParent.CompleteQuest();
            }
            else
            {
                var _psEmission = _particleSystem.emission;
                _psEmission.rateOverTime = 0;
            }
            
        }
    }
    private void OnTriggerExit(Collider _collision) // if left set to basic state(just a extra check incase QuestStartCollider failed)
    {
        if (_collision.gameObject.CompareTag("Cable"))
        {
            if(GameManager.Instance.CableInstance.CurrentCurrentState == _questParent.CurrentCableState)
            {
                _questParent.CableControl.SetCableState(Cable.CableState.none);
            }
            
        }
    }
}
