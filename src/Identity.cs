using System.Text.RegularExpressions;
using BepInEx.Configuration;
using Photon.Pun;
using Photon.Realtime;

namespace ABUF.IdentityRename
{
    /// <summary>
    /// 游戏内显示名：写进 PhotonNetwork.NickName，其它玩家看到的就是这个名字。
    /// 留空则使用 Steam 名。
    /// </summary>
    public static class Identity
    {
        public const int MaxLength = 32;

        private static readonly Regex RichText = new Regex("<[^>]*>", RegexOptions.Compiled);

        public static ConfigEntry<string> DisplayName;

        public static void Init(ConfigFile config)
        {
            DisplayName = config.Bind("身份", "显示名", "",
                "游戏内显示名（留空 = 使用 Steam 名）。改完重进房间生效。");
        }

        public static string Clean(string raw)
        {
            if (string.IsNullOrEmpty(raw))
            {
                return string.Empty;
            }

            string text = RichText.Replace(raw, string.Empty);
            text = text.Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Trim();
            if (text.Length > MaxLength)
            {
                text = text.Substring(0, MaxLength).Trim();
            }
            return text;
        }

        public static string Chosen
        {
            get { return Clean(DisplayName == null ? string.Empty : DisplayName.Value); }
        }

        public static string SteamName
        {
            get
            {
                try
                {
                    return Clean(Peak.Network.NetworkingUtilities.GetUsername());
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public static string Current
        {
            get
            {
                try
                {
                    return Clean(PhotonNetwork.NickName);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>把自定义名字写进 Photon 昵称（进房间前后都可以调）。</summary>
        public static void Apply()
        {
            try
            {
                string c = Chosen;
                if (c.Length > 0 && PhotonNetwork.NickName != c)
                {
                    PhotonNetwork.NickName = c;
                    Plugin.Log.LogInfo("[Identity] 显示名已设为 " + c);
                }
            }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning("[Identity] 设置显示名失败：" + e.Message);
            }
        }
    }

    /// <summary>进房间时把自定义名字应用一次（Photon 只在加入房间时同步昵称）。</summary>
    internal sealed class IdentityJoinHook : IMatchmakingCallbacks
    {
        public void OnJoinedRoom()
        {
            Identity.Apply();
        }

        public void OnFriendListUpdate(System.Collections.Generic.List<FriendInfo> friendList)
        {
        }

        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
        }
    }
}
