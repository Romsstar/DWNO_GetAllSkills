using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;


namespace BackToTheTitleScreen;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Romsstar.DWNO.GetAllSkills";
    internal const string PluginName = "GetAllSkills";
    internal const string PluginVersion = "1.0";

    public override void Load()
    {
        Awake();
    }

    public void Awake()
    {
        Harmony harmony = new Harmony("Patches");
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(PlayerData))]
    [HarmonyPatch("ReadSaveData")]
    public static class MainTitlePatch
    {
        private static bool flagsSet = false;

        [HarmonyPostfix]
        public static void ReadSaveData_Postfix(MainTitle __instance)
        {
            if (!flagsSet && StorageData.m_playerData != null && StorageData.m_playerData.m_AttacSkillkFlag != null)
            {
                for (uint i = 0; i < StorageData.m_playerData.m_AttacSkillkFlag.GetNumFlags(); i++)
                {
                    StorageData.m_playerData.m_AttacSkillkFlag[i] = true;
                }
            }
        }
    }
}