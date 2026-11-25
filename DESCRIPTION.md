# Identity Rename

## 中文

受够了自己的 Steam 名字出现在大厅里？
PEAK 没有提供改名入口，但别人看到的名字其实就是 `PhotonNetwork.NickName`。

**——想叫什么就叫什么。**

### 原版是怎么显示的？

PEAK 把 `PhotonNetwork.NickName`（默认就是你登录 Steam 的名字）直接显示给同一房间的其他人，游戏内没有任何改名入口。

### 这个模组做了什么？

从配置文件读一个名字，在加入房间时写进 `PhotonNetwork.NickName`：

| 配置 | 效果 |
|---|---|
| 留空 | 使用 Steam 名（原版行为） |
| 填了内容 | 房间里的所有人看到这个名字 |

- 加入房间时自动应用（Photon 只在加入房间时同步昵称），改完配置重进房间即可
- 会自动去掉富文本标签和换行，长度上限 32 字符，避免把大厅界面弄乱

### 配置

`BepInEx/config/abuf.identityrename.cfg`

| 配置项 | 默认 | 说明 |
|---|---|---|
| `身份.显示名` | 空 | 想让别人看到的名字；留空 = 使用 Steam 名 |

### 说明

- 纯客户端模组：你自己装就生效，队友不需要安装
- 昵称本来就会随 Photon 同步给房间里的其他人，本模组只是换了个来源
- 多人游戏安全

---

## English

Tired of your Steam name showing up in the lobby?
PEAK gives you no rename option, but the name others see is simply
`PhotonNetwork.NickName`.

**Pick whatever name you like.**

### How vanilla shows your name

PEAK displays `PhotonNetwork.NickName` (your Steam name by default) to everyone
in the room, and the game offers no way to change it.

### What this mod does

It reads a name from the config file and writes it into
`PhotonNetwork.NickName` when you join a room:

| Config | Result |
|---|---|
| Empty | Your Steam name is used (vanilla behaviour) |
| Filled in | Everyone in the room sees that name |

- Applied automatically when joining a room (Photon syncs the nickname on join),
  so edit the config and rejoin
- Rich-text tags and line breaks are stripped, and the name is capped at 32
  characters, so the lobby UI cannot be broken

### Config

`BepInEx/config/abuf.identityrename.cfg`

| Key | Default | Meaning |
|---|---|---|
| `身份.显示名` (Display name) | empty | Name shown to other players; empty = Steam name |

### Notes

- Client-side mod: only you need to install it
- The nickname is already synced to the room by Photon; this mod only changes where it comes from
- Multiplayer-safe
