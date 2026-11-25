# Identity Rename

Change the name other players see in the lobby — no more being stuck with your
Steam name. Created by **ABUF**.

## What it does

PEAK shows `PhotonNetwork.NickName` to other players. This mod lets you set that
name from the config file:

- leave it empty → your Steam name is used
- fill it in → everyone sees that name instead

The name is applied automatically when you join a room.
Rich-text tags and line breaks are stripped, and the name is capped at 32 characters.

## Config

`BepInEx/config/abuf.identityrename.cfg`

```ini
[身份]
显示名 = 
```

| Key | Default | Meaning |
|---|---|---|
| `显示名` (Display name) | empty | Name shown to other players; empty = Steam name |

Change it, then rejoin the room (Photon syncs the nickname on join).

## Installation

1. Install **BepInEx 5** for PEAK.
2. Drop the `BepInEx` folder from the release zip into the game folder.
3. Launch the game once to generate the config.

## Building from source

```powershell
$env:PEAK_GAME_DIR     = "C:\path\to\PEAK"
$env:PEAK_BEPINEX_CORE = "C:\path\to\BepInEx\core"
./build.ps1
```

Output: `dist/IdentityRename-1.0.0.zip`

## License

MIT — see `LICENSE`.
