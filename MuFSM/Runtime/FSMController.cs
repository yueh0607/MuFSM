using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public sealed class FSMController : MonoBehaviour
{

    private class StateData
    {
        public IState State;
        public bool IsRunning = false;

        public StateData(IState state, bool isRunning)
        {
            State = state;
            IsRunning = isRunning;
        }
    }


    [SerializeField] string defaultState = String.Empty;

    private Dictionary<Type, StateData> states = new Dictionary<Type, StateData>();


    /// <summary>
    /// 获取正在运行的状态列表
    /// </summary>
    /// <param name="runningStatesList"></param>
    public void GetRunningStateTypes(List<Type> runningStatesList)
    {
        foreach (var state in states)
        {
            if (state.Value.IsRunning)
            {
                runningStatesList.Add(state.Key);
            }
        }
    }
    /// <summary>
    /// 判断状态是否在运行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool IsRunning<T>() => IsRunning(typeof(T));

    /// <summary>
    /// 进入状态，如果正在运行，则不生效
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Enter<T>() where T : IState
    {
        Enter(typeof(T));
    }
    /// <summary>
    /// 退出状态，如果不在运行，则不生效
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Exit<T>() where T : IState
    {
        Exit(typeof(T));
    }
    /// <summary>
    /// 强制进入状态，无论是否已经退出和是否正在运行，都将重新进入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ForceEnter<T>() where T : IState => ForceEnter(typeof(T));
    /// <summary>
    /// 指定秒后退出状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="time"></param>
    /// <returns></returns>
    public Coroutine ExitAfter<T>(float time) where T : IState
    {
        return StartCoroutine(ExitAfterEnumerator<T>(time));
    }
    /// <summary>
    /// 退出全部状态
    /// </summary>
    public void ExitAll()
    {
        foreach (var state in states)
        {
            if (state.Value.IsRunning)
                Exit(state.Key);
        }
    }



    bool IsRunning(Type type)
    {
        if (states.ContainsKey(type))
        {
            if (states[type].IsRunning == true) return true;
        }
        return false;
    }
    IEnumerator ExitAfterEnumerator<T>(float seconds) where T : IState
    {
        float current = 0;
        yield return null;
        while (true)
        {
            current += Time.deltaTime;
            if (current >= seconds) break;
            yield return null;

        }
        Exit<T>();
    }


    //不存在则添加并初始化状态
    void CheckAddState(Type type)
    {
        if (!states.ContainsKey(type))
        {
            var st = (IState)Activator.CreateInstance(type);
            st.Machine = this;
            st.OnInit();
            states.Add(type, new StateData(st, false));
        }
    }
    void Enter(Type type)
    {
        if (IsRunning(type)) return;
        ForceEnter(type);
    }
    void ForceEnter(Type type)
    {
        CheckAddState(type);
        if (IsRunning(type)) Exit(type);
        var state = states[type];
        state.IsRunning = true;
        state.State.OnEnter();
    }

    void Exit(Type type)
    {
        if (!states.ContainsKey(type)) return;
        var state = states[type];
        if (state.IsRunning == false) return;
        state.IsRunning = false;
        state.State.OnExit();
    }



    void Start()
    {
        if (defaultState != String.Empty)
        {
            Type defaultType = Type.GetType(defaultState);
            Enter(defaultType);
        }
    }

    void Update()
    {
        foreach (var state in states)
        {
            if (state.Value.IsRunning)
                state.Value.State.OnUpdate();
        }

    }
    private void FixedUpdate()
    {
        foreach (var state in states)
        {
            if (state.Value.IsRunning)
                state.Value.State.OnFixedUpdate();
        }
    }

    private void OnDestroy()
    {
        ExitAll();
        foreach (var state in states)
        {
            state.Value.State.OnDestroy();
        }
        states.Clear();
    }
}
