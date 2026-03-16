
using UnityEngine;

public class QuestEndCollider : MonoBehaviour
{
    [SerializeField] private Quests _questParent;
    [SerializeField] private ParticleSystem _particleSystem;


    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Cable"))
        {
            if(GameManager.Instance.CableInstance.CurrentCurrentState == _questParent.CurrentCableState)
            {
                var _psEmission = _particleSystem.emission;
                _psEmission.rateOverTime = GameManager.Instance.CableInstance.ParticleAmount;
                _questParent.CompleteQuest();
            }
            else
            {
                var _psEmission = _particleSystem.emission;
                _psEmission.rateOverTime = 0;
            }
            
        }
    }
    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Cable"))
        {
            if(GameManager.Instance.CableInstance.CurrentCurrentState == _questParent.CurrentCableState)
            {
                var _psEmission = _particleSystem.emission;
                _psEmission.rateOverTime = 0;
            }
            
        }
    }
}
