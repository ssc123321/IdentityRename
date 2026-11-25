# Identity Rename

## 中文

现在你可以每天换一个新的名字，以新的身份出现在大家的眼里。祝你每天都有一个好的心情。

PEAK 里别人看到的名字，其实就是 `PhotonNetwork.NickName`，默认取你登录 Steam 的名字，游戏里并没有改名入口。

这个模组做了件很小的事：让你从配置文件里填一个名字。

- 留空 → 还是用 Steam 名，跟原版一样
- 填了 → 房间里的所有人都看到这个名字

改完配置重进一次房间就生效（Photon 只在加入房间时同步昵称）。名字里的富文本标签和换行会被自动去掉，长度最多 32 个字符，不会把大厅界面弄花。

配置文件在 `BepInEx/config/abuf.identityrename.cfg`，要改的是 `身份.显示名`。

纯客户端模组，自己装就生效，队友不需要装。昵称本来就是 Photon 会同步给房间里其他人的字段，这里只是换了个来源。

---

## English

Now you can pick a new name every day and show up as someone else.
Hope every day treats you well.

The name other players see in PEAK is simply `PhotonNetwork.NickName`, which defaults
to your Steam name — and the game has no rename option.

This mod does one small thing: it lets you set that name from the config file.

- Empty → your Steam name is used, exactly like vanilla
- Filled in → everyone in the room sees that name

Edit the config and rejoin a room for it to take effect (Photon syncs the nickname on
join). Rich-text tags and line breaks are stripped, and the name is capped at 32
characters, so the lobby UI can't be broken.

The file is `BepInEx/config/abuf.identityrename.cfg`, and the entry to change is
`身份.显示名`.

Client-side only: install it and you're set, teammates don't need it. The nickname is
already something Photon syncs to the room — this just changes where it comes from.
