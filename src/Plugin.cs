using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ABUF.IdentityRename
{
    [BepInPlugin(Guid, PluginName, PluginVersion)]
    public sealed class Plugin : BaseUnityPlugin
    {
        public const string Guid = "abuf.identityrename";
        public const string PluginName = "Identity Rename";
        public const string PluginVersion = "1.0.0";

        public static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            Identity.Init(Config);
            try
            {
                Photon.Pun.PhotonNetwork.AddCallbackTarget(new IdentityJoinHook());
            }
            catch (System.Exception e)
            {
                Log.LogWarning("[Identity] 注册 Photon 回调失败：" + e.Message);
            }
            Identity.Apply();

            Log.LogInfo(PluginName + " " + PluginVersion + " loaded."
                + (Identity.Chosen.Length > 0 ? " name=" + Identity.Chosen : " name=Steam"));
        }
    }
}
