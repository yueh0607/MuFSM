# MuFSM
基于Unity的超轻量级FSM工具，运行时只有三个半类。支持运行时状态实时显示和多个状态同时进入。

# Runtime类型介绍
- IState  状态接口，所有状态必须实现，能无法继承AbstractState时可以考虑实现IState
- AbstractState 状态基类，所有状态可以直接继承
- FSMController 状态机，继承了MonoBehaviour,请直接添加到物体上，可以填写默认状态的类名，不填就是没有。

# 状态生命周期
- OnInit  仅在状态第一次被初始化是调用，一个FSMController会初始化一次
- OnDestroy 状态机被Destroy时，会退出全部状态并调用该方法
- OnEnter 进入状态时调用
- OnExit 退出状态时调用
- Update 等同于MonoBehaviour的生命周期，仅在进入状态后被调用
- FixedUpdate 等同于MonoBehaviour的生命周期，仅在进入状态后被调用

生命周期顺序 ：  OnInit-> OnEnter -> Update/FixedUpdate -> OnExit -> OnDestroy

# API
![image](https://github.com/yueh0607/MuFSM/assets/102401735/bea4eb7d-7a01-4808-a8ca-edd5800e16b7)
