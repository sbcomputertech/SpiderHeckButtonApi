using BepInEx;
using HarmonyLib;

namespace ButtonApiMod
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class ButtonApiModMain : BaseUnityPlugin
    {
        private const string ModName = "ButtonApi";
        private const string ModAuthor  = "reddust9";
        private const string ModGUID = "com.reddust9.buttonapi";
        private const string ModVersion = "1.0.0";
        internal void Awake()
        {
            var harmony = new Harmony(ModGUID);
            harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
            ButtonApi.RegisterButton("buttonapi test 1", () => { Logger.LogInfo("Test Button 1 click"); });
            ButtonApi.RegisterButton("buttonapi test 2", () => { Logger.LogInfo("Test Button 2 click"); });
        }
    }
}
