using Doozy.Engine.UI;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ButtonApiMod;

[HarmonyPatch(typeof(HudController), nameof(HudController.Update))]
public class UpdatePatch
{
    public static void Postfix(HudController __instance)
    {
        if (__instance.graphController.Graph.ActiveNode !=
            __instance.graphController.Graph.GetNodeByName("Paused")) return;
        for(int i = 0; i < ButtonApi.buttons.Count; i++)
        {
            var bi = ButtonApi.buttons[i];
            if (!bi.added)
            {
                var newButton = Object.Instantiate(__instance.quitButton, __instance.PauseLayout.transform, true);
                newButton.name = "NewButton";
                bi.buttonObj = newButton;
                bi.added = true;
            }
            
            var label = bi.buttonObj.transform.Find("Label");
            if (label != null)
            {
                var tmp = label.GetComponent<TextMeshProUGUI>();
                tmp.SetText(bi.text);
            }
            bi.buttonObj.gameObject.SetActive(true);
            var uibutton = bi.buttonObj.gameObject.GetComponent<UIButton>();
            var s_NormalLoopAnimation = uibutton.NormalLoopAnimation;
            var s_SelectedLoopAnimation = uibutton.SelectedLoopAnimation;
            var s_OnSelected = uibutton.OnSelected;
            var s_OnClick_PunchAnimation = uibutton.OnClick.PunchAnimation;
            uibutton.Reset();
            uibutton.NormalLoopAnimation = s_NormalLoopAnimation;
            uibutton.SelectedLoopAnimation = s_SelectedLoopAnimation;
            uibutton.OnSelected = s_OnSelected;
            uibutton.OnClick.PunchAnimation = s_OnClick_PunchAnimation;
            
            uibutton.OnClick.OnTrigger.Event.AddListener(bi.onClick);
            ButtonApi.buttons[i] = bi;
        }
    }
}