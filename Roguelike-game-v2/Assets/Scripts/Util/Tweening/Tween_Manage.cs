using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// <para>
/// 트윈 애니메이션 실행 및 관리
/// </para>
/// Transform을 단위로 트윈을 등록, 해제, 제어 기능 제공
/// </summary>
public static class Tween_Manage
{
    private static Dictionary<Component, Sequence> _schedule = new();
    private static Dictionary<Component, TweenStatus> _status = new();

    private static readonly Type _transform = typeof(Transform);

    // 컴포넌트의 Transform으로 트윈을 생성 _schedule에 등록, TweenOperation에 따라서 Tween 실행 순서가 결정
    public static Component Execute(Component comp, TweenOperation op, TweenType type, NumericValue numeric, float duration, EaseType ease, float delay = 0)
    {
        if(GetTransform(comp, out Transform trans))
        {
            TweenData data = new();

            bool isContain = _schedule.TryGetValue(trans, out Sequence schedule);

            switch(op)
            {
                case TweenOperation.Append:
                    data.Set(type, trans, EaseProvider.Get(ease), numeric, duration);

                    if(!isContain)
                    {
                        data.Set(CoroutineHelper.Start(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay), CoroutineType.Tween));

                        op = TweenOperation.Join;
                    }
                    break;
                case TweenOperation.Insert:
                    data.Set(CoroutineHelper.Start(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay), CoroutineType.Tween), type, trans, EaseProvider.Get(ease), numeric, duration);
                    break;
                case TweenOperation.Join:
                    if(isContain)
                    {
                        if(schedule.Count() > 1)
                        {
                            data.Set(type, trans, EaseProvider.Get(ease), numeric, duration);
                            break;
                        }
                    }

                    data.Set(CoroutineHelper.Start(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay), CoroutineType.Tween), type, trans, EaseProvider.Get(ease), numeric, duration);
                    break;
            }

            if(isContain && op != TweenOperation.Append)
            {
                schedule.PeekLast().Add(data);
            }
            else
            {
                List<TweenData> sched = new() { data };

                if(op != TweenOperation.Append)
                {
                    schedule = new();

                    _schedule.Add(trans, schedule);
                    _status.Add(trans, new(true));
                }

                schedule.Enqueue(sched);
            }

            return comp;
        }
        else
        {
            return null;
        }
    }
    // 트윈 애니메이션 실행 활성화 상태 설정
    public static Component SetStatus(Component comp, bool status)
    {
        _status[comp].flag = status;

        return comp;
    }
    // 트윈 중단 및 제거
    public static Component Kill(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                foreach(TweenData data in sequence.Values()[0])
                {
                    if(data.coroutine != null)
                    {
                        CoroutineHelper.Stop(data.coroutine);
                    }
                }

                Clear(trans);
            }
        }

        return comp;
    }
    // 다음 순서의 트윈을 실행하며, 이전 단계의 트윈은 해제
    public static Component PlayNext(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                List<TweenData> dataList = sequence.Peek();
                int count = dataList.Count;

                for (int i = 0; i < count; i++)
                {
                    Release(trans, dataList[0]);
                }
            }
        }

        return comp;
    }
    // 현재 진행 중인 트윈을 완료 시점으로 전환
    public static Component SkipToEnd(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                List<TweenData> dataList = sequence.Peek();

                TweenData data = dataList[0];

                CoroutineHelper.Stop(data.coroutine, CoroutineType.Tween);
                Tweening.ToEnd(data);
            }
        }

        return comp;
    }
    // 모든 트윈을 완료 시점으로 전환
    public static void AllSkipToEnd()
    {
        List<Component> keys = _schedule.Keys.ToList();

        for(int i = 0; i < keys.Count; i++)
        {
            SkipToEnd(keys[i]);
        }
    }
    // 등록된 트윈 여부 확인
    public static bool IsTweenActive()
    {
        if(_schedule.Count != 0)
        {
            return true;
        }

        return false;
    }
    // 모든 트윈 종료, _schecule과 _status 초기화
    public static void Reset()
    {
        foreach(Sequence schedule in _schedule.Values)
        {
            foreach(List<TweenData> datas in schedule.Values())
            {
                foreach(TweenData data in datas)
                {
                    CoroutineHelper.Stop(data.coroutine);
                }
            }
        }

        _schedule = new();
        _status = new();
    }
    // 트윈 정보를 통해서 등록된 트윈 제거
    public static void Release(Transform transform, TweenData data)
    {
        _schedule[transform].Dequeue(transform, data);
    }
    // 트윈 제거
    public static void Clear(Transform transform)
    {
        _schedule.Remove(transform);
        _status.Remove(transform);
    }
    // 트윈 활성화 상태 반환
    public static TweenStatus GetStatus(Component comp)
    {
        if(_status.TryGetValue(comp, out TweenStatus value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    // 트윈 등록 과정에서 입력받은 컴포넌트에서 Transform 반환
    private static bool GetTransform(Component comp, out Transform transform)
    {
        if(comp.Equals(_transform))
        {
            transform = comp as Transform;

            return true;
        }
        else
        {
            transform = comp.GetComponent<Transform>();

            return transform != null ? true : false;
        }
    }
}