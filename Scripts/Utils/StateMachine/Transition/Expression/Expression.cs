using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Expression
{
    private Stack<IExpressionUnit> m_LeftResultCaches = new Stack<IExpressionUnit>();
    private IExpressionUnit m_RightResultCache;
    private IExpressionUnit m_Result;
    private Stack<OperatorBase> m_CacheOperatorList = new Stack<OperatorBase>();

    private IStateMachineOwner Owner;      //!true and (true or !true)

    private bool _IsInitialized = false;

    public void PushUnit(IExpressionUnit unit)
    {
        if(m_CacheOperatorList.Count == 0)
        {
            if (!_IsInitialized)
            {
                m_Result = unit;
                _IsInitialized = true;
            }

            m_LeftResultCaches.Push(unit);
        }
        else
        {
            m_RightResultCache = unit;
            int count = m_CacheOperatorList.Count;
            for(int i = 0;i<count;i++)
            {
                OperatorBase op = m_CacheOperatorList.Pop();
                if (op is SeparatorStartOperator)
                {
                    m_CacheOperatorList.Push(op);
                    m_LeftResultCaches.Push(m_RightResultCache);
                    break;
                }
                else
                {
                    op.SetRightUnit(m_RightResultCache);
                    m_RightResultCache = op.GetOperatorResult();
                    //op.DebugText(Owner);
                }

                if(i == count - 1)
                {
                    if (!_IsInitialized)
                    {
                        _IsInitialized = true;
                    }
                    m_Result = m_RightResultCache;

                    m_LeftResultCaches.Push(m_RightResultCache);
                }
            }
        }
    }

    public void PushOperator(OperatorBase op)
    {
        if(op.NeedLeftUnit())
        {
            if (op is SeparatorEndOperator)
            {
                if (m_RightResultCache == null)
                    throw new System.Exception("Need Right unit");
                op.SetLeftUnit(m_RightResultCache);
                //op.DebugText(Owner);
                OperatorBase preOp = m_CacheOperatorList.Pop();
                if(preOp is SeparatorStartOperator)
                {
                    PushUnit(op.GetOperatorResult());
                }
                else
                {
                    throw new System.Exception("Need Separator Start Operator");
                }
            }
            else
            {
                IExpressionUnit left = m_LeftResultCaches.Pop();
                op.SetLeftUnit(left);
            }
        }
        
        if(op.NeedRightUnit())
        {
            m_CacheOperatorList.Push(op);
        }
    }

    public bool GetExpressionResult(IStateMachineOwner owner)
    {
        if (m_CacheOperatorList.Count > 0)
            throw new System.Exception("Express Unit is not enough");

        return m_Result.GetValue(owner);
    }

    public void Reset(IStateMachineOwner owner)
    {
        if (m_CacheOperatorList.Count > 0)
            throw new System.Exception("Express Unit is not enough");

        m_Result.Reset(owner);
    }

    //Debug Function
    public void SetOwner(IStateMachineOwner owner)
    {
        Owner = owner;
    }
}
