# HeroBorn

> Unity 官方 RPG 教程《Hero Born》个人实现。从零搭建 2D RPG：角色控制、战斗系统、动画状态机、ShaderLab 着色器、TextMesh Pro UI。本项目为学习 Unity RPG 开发的完整记录，含制作过程的技术回顾。

## 项目简介

本项目是 Unity 官方 RPG 教程系列《Hero Born》的个人实现版本。教程从零开始教 RPG 基础：场景搭建 → 角色控制（移动 / 跳跃）→ 敌人 AI → 战斗（攻击 / 受伤 / 死亡）→ 生命值 + 经验值 + 升级 → Prefab → 动画状态机 → TextMesh Pro UI。

- **引擎**：Unity 2022.3 LTS
- **语言**：C# + ShaderLab（HLSL）
- **目标平台**：PC（Windows / macOS / Linux）+ WebGL
- **教程来源**：B 站 Unity 教程系列 + Unity 官方 RPG 教学包

## 核心特性

- **完整 RPG 循环**：Player + Enemy AI + 战斗 + 经验值 + 升级 + 生命值 UI
- **角色控制**：键盘 WASD 移动 + 空格跳跃 + 鼠标点击攻击
- **战斗系统**：近战攻击 + 碰撞检测 + 伤害计算 + 击退 + 死亡动画
- **动画状态机**：Animator Controller 管理 Idle / Run / Jump / Attack / Hurt / Die
- **Prefab 工作流**：Player / Enemy / Projectile 等可复用 Prefab
- **UI 系统**：TextMesh Pro 生命值条 + 经验值条 + 等级显示
- **ShaderLab 着色器**：角色溶解效果 + 地面 Tilemap Shader
- **摄像机跟随**：Vector3.Lerp 平滑插值

## 项目结构

```
HeroBorn/
├── Assets/
│   ├── Animations/         # Player / Enemy 动画（.anim）
│   ├── Materials/          # 材质（Standard / Custom Shader）
│   ├── Prefabs/            # Player / Enemy / Projectile / HealthPickup
│   ├── Scenes/             # Main.unity 主关卡
│   ├── Scripts/
│   │   ├── Player/         # PlayerController / Health / Combat / Animation
│   │   ├── Enemy/          # EnemyController / Health / Animation
│   │   ├── Combat/         # Damageable / Projectile / HealthPickup
│   │   ├── UI/             # HealthBarUI / ExpBarUI / LevelTextUI
│   │   ├── Camera/         # CameraFollow
│   │   └── Game/           # GameManager / ScoreManager
│   ├── Shaders/            # PlayerSprite.shader / GroundTile.shader
│   └── TextMesh Pro/       # 字体 + 预设
├── ProjectSettings/
└── Packages/
```

## 环境要求

| 工具 | 版本 |
|---|---|
| Unity | 2022.3 LTS |
| Visual Studio | 2019 / 2022（含 C# + Unity 工作负载） |
| .NET | 4.x 兼容 |

## 安装与运行

```bash
git clone https://github.com/fan1959/HeroBorn.git
```

1. Unity Hub → Add → 选择 `HeroBorn` 目录
2. 选择 Unity 2022.3 LTS 打开
3. 双击 `Assets/Scenes/Main.unity`
4. 点 **Play**：WASD 移动 / 空格跳跃 / 鼠标左键攻击

## 关键技术

### Animator Controller

| 状态 | 触发条件 | 下一状态 |
|---|---|---|
| Idle | 速度 = 0 | Run |
| Run | 速度 > 0 | Idle |
| Jump | 空格按下 | Idle（落地） |
| Attack | SetTrigger("Attack") | Idle（动画结束） |
| Hurt | SetTrigger("Hurt") | Idle |
| Die | SetTrigger("Die") | 终态 |

### 战斗流程

```
Player.Attack() → 动画播放 + OnTriggerEnter2D 碰撞检测
              → Enemy.TakeDamage(damage)
              → Enemy 受伤动画 + 击退 + 经验值增加
              → HP ≤ 0 → Enemy 死亡 + 销毁
```

### ShaderLab 溶解效果

`PlayerSprite.shader` 用 `clip(col.a - _Threshold)` 实现敌人死亡时从有到无的溶解过渡。

### TextMesh Pro UI

- `HealthBarUI.cs`：监听 `OnHealthChanged` 事件更新血条
- `ExpBarUI.cs`：监听 `OnExpChanged` 事件
- `LevelTextUI.cs`：监听 `OnLevelUp` 事件

### 摄像机跟随

```csharp
transform.position = Vector3.Lerp(
    transform.position,
    new Vector3(player.position.x, player.position.y, transform.position.z),
    smoothSpeed * Time.deltaTime
);
```

## 学习路径

按实现顺序：

1. **场景搭建** — Tilemap 2D 关卡设计
2. **角色控制** — Rigidbody2D 物理 + 键盘输入
3. **动画状态机** — Unity Animator Controller 核心
4. **战斗系统** — 碰撞检测 + 伤害计算
5. **敌人 AI** — 简单状态机（巡逻 / 追击 / 攻击）
6. **UI 系统** — TextMesh Pro + 事件驱动
7. **经验值 + 升级** — 数据驱动 UI
8. **ShaderLab** — 自定义 Sprite Shader

## 制作回顾

本项目是学习 Unity 2D RPG 开发的入门实践，整个过程覆盖了 Unity 的几个核心系统：

- **Animator** 是 RPG 的灵魂——角色的每个动作（待机 / 跑 / 跳 / 攻击 / 受伤 / 死亡）都需要动画状态机精确管理。设置好 transitions 的条件（trigger / bool / float）后，代码只需 `animator.SetTrigger("Attack")` 即可驱动复杂的状态切换。
- **Prefab 工作流** 大幅提升效率——Player、Enemy、Projectile 做成 Prefab 后，修改一处即全局生效。
- **ShaderLab** 虽然只用了一个简单的溶解效果，但理解了 `vertex → fragment` 的管线流程和 `clip()` 的用法，为后续深入学习 Shader 打好了基础。
- **TextMesh Pro** 比 UGUI Text 好用太多——字体清晰、支持 SDF、中文渲染无毛刺。

## 协议

本仓库仅用于学习目的。Unity 官方 RPG 教学包的资产版权归 Unity Technologies 所有。

## 仓库

- **GitHub**: https://github.com/fan1959/HeroBorn
- **引擎**: Unity 2022.3 LTS
- **类型**: 2D RPG 教程项目
