# HeroBorn

> 一个 Unity RPG 教程项目《Hero Born》个人实现：含角色控制、战斗系统、动画状态机、Prefabs 预制体、ShaderLab 着色器、TextMesh Pro UI。学习 Unity RPG 开发的入门项目。

## 项目简介

本项目是 Unity 官方 RPG 教程系列《Hero Born》[HSM 001] 的个人实现版本。教程从零开始教 RPG 基础：场景搭建 + 角色控制（移动 / 跳跃）+ 敌人 AI + 战斗（攻击 / 受伤 / 死亡）+ 生命值 + 经验值 + 升级 + Prefab + 动画状态机（Animator Controller）+ TextMesh Pro UI。

- **引擎**：Unity 2022.3 LTS
- **语言**：C# + ShaderLab（HLSL）
- **目标平台**：PC（Windows / macOS / Linux）+ WebGL
- **教程来源**：B 站 Unity 教程系列 + Unity 官方 RPG 教学包

## 核心特性

- **完整 RPG 循环**：玩家角色（Player）+ 敌人 AI（Enemy）+ 战斗 + 经验值 + 升级 + 生命值 UI
- **角色控制**：键盘 WASD 移动 + 空格跳跃 + 鼠标点击攻击
- **战斗系统**：近战攻击 + 碰撞检测 + 伤害计算 + 击退 + 死亡动画
- **动画状态机**：Animator Controller 管理 Idle / Run / Jump / Attack / Hurt / Die 状态切换
- **Prefab 工作流**：Player / Enemy / Projectile 等可复用 Prefab
- **UI 系统**：TextMesh Pro 生命值条 + 经验值条 + 等级显示
- **ShaderLab 着色器**：角色 + 环境的自定义 Shader（HLSL）
- **场景系统**：Tilemap 2D 关卡 + 摄像机跟随

## 项目结构

```
HeroBorn/
├── .gitignore                              # Unity 标准排除
├── .vsconfig                               # Visual Studio 配置文件
├── README.md                               # 本文件
│
├── Assets/                                 # Unity 资源根目录
│   ├── Animations/                         # 动画文件（.anim）
│   │   ├── Player_Idle.anim
│   │   ├── Player_Run.anim
│   │   ├── Player_Jump.anim
│   │   ├── Player_Attack.anim
│   │   ├── Player_Hurt.anim
│   │   ├── Player_Die.anim
│   │   ├── Enemy_Idle.anim
│   │   ├── Enemy_Run.anim
│   │   ├── Enemy_Attack.anim
│   │   ├── Enemy_Hurt.anim
│   │   └── Enemy_Die.anim
│   │
│   ├── Materials/                          # 材质（Standard / Custom Shader）
│   │   ├── Player_Material.mat
│   │   ├── Enemy_Material.mat
│   │   ├── Ground_Material.mat
│   │   └── Projectile_Material.mat
│   │
│   ├── Prefabs/                            # 预制体
│   │   ├── Player.prefab                   # 玩家角色（带 Animator + Collider + Rigidbody2D）
│   │   ├── Enemy.prefab                     # 敌人 AI
│   │   ├── Projectile.prefab                # 投射物
│   │   └── HealthPickup.prefab              # 生命药水
│   │
│   ├── Scenes/                             # 场景文件
│   │   └── Main.unity                      # 主关卡
│   │
│   ├── Scripts/                            # C# 脚本
│   │   ├── Player/
│   │   │   ├── PlayerController.cs          # 角色控制（移动 / 跳跃 / 攻击）
│   │   │   ├── PlayerHealth.cs              # 生命值 + 受伤
│   │   │   ├── PlayerCombat.cs              # 战斗（攻击冷却 / 伤害输出）
│   │   │   └── PlayerAnimation.cs           # 动画状态机驱动
│   │   ├── Enemy/
│   │   │   ├── EnemyController.cs           # 敌人 AI（巡逻 / 追击 / 攻击）
│   │   │   ├── EnemyHealth.cs
│   │   │   └── EnemyAnimation.cs
│   │   ├── Combat/
│   │   │   ├── Damageable.cs               # 可受伤接口
│   │   │   ├── Projectile.cs               # 投射物
│   │   │   └── HealthPickup.cs             # 生命药水
│   │   ├── UI/
│   │   │   ├── HealthBarUI.cs               # 生命值条
│   │   │   ├── ExpBarUI.cs                 # 经验值条
│   │   │   └── LevelTextUI.cs              # 等级文字
│   │   ├── Camera/
│   │   │   └── CameraFollow.cs             # 摄像机跟随
│   │   └── Game/
│   │       ├── GameManager.cs              # 全局游戏状态
│   │       └── ScoreManager.cs             # 分数 + 经验值
│   │
│   ├── Shaders/                            # ShaderLab 着色器
│   │   ├── PlayerSprite.shader             # 角色 Sprite Shader（含溶解效果）
│   │   └── GroundTile.shader               # 地面 Tilemap Shader
│   │
│   └── TextMesh Pro/                       # TextMesh Pro 资源（字体 + 预设）
│       ├── LiberationSans SDF.asset
│       └── Default Style.asset
│
├── ProjectSettings/                        # Unity 工程配置
│   ├── ProjectVersion.txt
│   ├── QualitySettings.asset
│   ├── Physics2DSettings.asset
│   └── ...
│
└── Packages/                               # Package Manager 依赖
    ├── manifest.json
    └── packages-lock.json
```

## 环境要求

| 工具 | 版本 |
|---|---|
| Unity | 2022.3 LTS 或更新（推荐 LTS） |
| Visual Studio | 2019 / 2022（含 C# + Unity 工作负载） |
| .NET | 4.x 兼容 |
| Git LFS | 可选（动画 / Prefab 二进制资源） |

## 安装与运行

### 1. 克隆仓库

```bash
git clone https://github.com/fan1959/HeroBorn.git
cd HeroBorn
```

### 2. 用 Unity Hub 打开

1. 打开 Unity Hub
2. 点 **Add** → 选择 `HeroBorn` 目录
3. 选择 **Unity 2022.3 LTS**（或更新 LTS 版本）打开
4. 等待 Unity 导入资源（首次约 1-3 分钟）

### 3. 打开主场景

在 Unity Editor：
- Project 窗口：`Assets/Scenes/Main.unity`
- 双击打开主场景

### 4. 运行游戏

- 点 Unity 顶部 **▶ Play** 按钮
- 键盘 **WASD** 移动，**空格** 跳跃，**鼠标左键** 攻击
- 接近敌人触发战斗，击败敌人获得经验值
- 升级后生命值提升

### 5. 打包构建（可选）

```
File → Build Settings → Add Open Scenes → Build
```

可选平台：PC / macOS / Linux Standalone / WebGL。

## 关键技术

### Animator Controller（动画状态机）

Player 和 Enemy 都用 Animator Controller 管理状态：

| 状态 | 触发条件 | 下一状态 |
|---|---|---|
| Idle | 速度 = 0 | Run（按下移动键时） |
| Run | 速度 > 0 | Idle（停止时） |
| Jump | 速度 Y > 0（按下空格时） | Idle（落地时） |
| Attack | 触发器 `Attack` | Idle（攻击动画结束时） |
| Hurt | 触发器 `Hurt`（被攻击时） | Idle（受伤动画结束时） |
| Die | 触发器 `Die`（HP ≤ 0） | （终态，禁用输入） |

通过 `Animator.SetTrigger("Attack")` / `SetBool("IsRunning", true)` 等 API 驱动。

### 战斗系统

```
Player.Attack() → 触发 attack 触发器
              → 动画播放 + 攻击碰撞检测（OnTriggerEnter2D）
              → 对 Enemy 调用 EnemyHealth.TakeDamage(damage)
              → Enemy 受伤动画 + 击退 + 经验值增加
              → 如果 HP ≤ 0 → Enemy 死亡动画 + 销毁
```

### ShaderLab（角色 Sprite Shader）

`Assets/Shaders/PlayerSprite.shader`：实现溶解效果（Dissolve），用于敌人死亡时从有到无消失：

```hlsl
// 简化版：阈值控制 alpha
fixed4 frag(v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);
    clip(col.a - _Threshold);
    return col;
}
```

### TextMesh Pro UI

使用 TextMesh Pro 替代 UGUI Text，渲染清晰的中英文字体：

- `HealthBarUI.cs`：监听 `PlayerHealth.OnHealthChanged` 事件，实时更新血条
- `ExpBarUI.cs`：监听 `ScoreManager.OnExpChanged` 事件
- `LevelTextUI.cs`：监听 `ScoreManager.OnLevelUp` 事件

### 摄像机跟随

`CameraFollow.cs`：每帧用 `Vector3.Lerp` 平滑插值到玩家位置：

```csharp
transform.position = Vector3.Lerp(
    transform.position,
    new Vector3(player.position.x, player.position.y, transform.position.z),
    smoothSpeed * Time.deltaTime
);
```

## 学习要点

按实现顺序，推荐的学习路径：

1. **场景搭建**（`Scenes/Main.unity`）—— Tilemap 2D 关卡设计
2. **角色控制**（`Player/PlayerController.cs`）—— Rigidbody2D 物理 + 键盘输入
3. **动画状态机**（`Animations/` + Animator Controller）—— Unity 动画核心
4. **战斗系统**（`Combat/`）—— 碰撞检测 + 伤害计算
5. **敌人 AI**（`Enemy/EnemyController.cs`）—— 简单状态机（巡逻 / 追击 / 攻击）
6. **UI 系统**（`UI/`）—— TextMesh Pro + 事件驱动
7. **经验值 + 升级**（`Game/ScoreManager.cs`）—— 数据驱动 UI
8. **ShaderLab**（`Shaders/`）—— 自定义 Sprite Shader

## 协议

本仓库仅用于学习目的。Unity 官方 RPG 教学包的资产版权归 Unity Technologies 所有。

## 仓库

- **GitHub**: https://github.com/fan1959/HeroBorn
- **引擎**: Unity 2022.3 LTS
- **类型**: 2D RPG 教程项目
- **教程来源**: B 站 / Unity 官方
