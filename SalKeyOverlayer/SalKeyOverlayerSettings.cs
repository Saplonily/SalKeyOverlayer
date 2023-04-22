using System.Collections.Generic;
using Monocle;

namespace Celeste.Mod.SalKeyOverlayer;

[SettingName("modoptions_salkeyoverlayer_title")]
public class SalKeyOverlayerSettings : EverestModuleSettings
{
    public List<ButtonInfo> ButtonInfos { get; set; }

    public void CreateButtonInfosEntry(TextMenu menu, bool inGame)
    {
        TextMenu.Item item;
        menu.Add(
            item = new TextMenu.Button("modoptions_salkeyoverlayer_reload".DialogClean())
            .Pressed(() =>
            {
                SalKeyOverlayerModule.Instance.LoadSettings();
                if(Engine.Scene is Level l && l.Tracker.GetEntity<OverLayerEntity>() is OverLayerEntity entity)
                {
                    entity.RemoveSelf();
                    l.Add(new OverLayerEntity());
                }
            })
            );
        item.AddDescription(menu, "modoptions_salkeyoverlayer_reloadhint".DialogClean());
    }
}