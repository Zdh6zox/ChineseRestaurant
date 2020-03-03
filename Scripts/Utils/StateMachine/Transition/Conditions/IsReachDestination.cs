using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReachDestination : ConditionBase
{
    private float _distanceThreshold;

    public void SetDistanceThreshold(float distance) { _distanceThreshold = distance; }

    protected override void UpdateIsVerfied(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Blackboard bb = customer.GetComponent<Blackboard>();
        if (bb == null)
            throw new System.Exception(string.Format("Cannot Find Blackboard in {0}", customer.name));

        Vector3 des;
        if(bb.GetBBValue<Vector3>("NavTargetPos",  out des))
        {
            Vector3 curPos = customer.transform.position;

            float distance = (des - curPos).magnitude;
            if(distance <= _distanceThreshold)
            {
                _isVerfied = true;
            }
            else
            {
                _isVerfied = false;
            }
        }
        else
        {
            Debug.Log(string.Format("{0} haven't set destination yet", customer.Data.CharacterName));
            _isVerfied = false;
        }
    }
}
