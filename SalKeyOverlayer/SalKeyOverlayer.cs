using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mod.SalKeyOverlayer;

public class SalKeyOverlayer : EverestModule
{
    protected static bool hookedFromHudRenderer = false;
    public static SalKeyOverlayer Instance { get; set; }
    public static SalKeyOverlayerSettings Settings => Instance._Settings as SalKeyOverlayerSettings;

    public override Type SettingsType => typeof(SalKeyOverlayerSettings);

    public SalKeyOverlayer()
    {
        Instance = this;
    }

    public override void Load()
    {
        On.Celeste.Level.Begin += this.Level_Begin;
        On.Celeste.HiresRenderer.BeginRender += this.HiresRenderer_BeginRender;
        On.Celeste.HudRenderer.RenderContent += this.HudRenderer_RenderContent;
    }

    private void HudRenderer_RenderContent(On.Celeste.HudRenderer.orig_RenderContent orig, HudRenderer self, Monocle.Scene scene)
    {
        if (scene is Level)
            hookedFromHudRenderer = true;
        orig(self, scene);
        hookedFromHudRenderer = false;
    }

    private void HiresRenderer_BeginRender(On.Celeste.HiresRenderer.orig_BeginRender orig, BlendState blend, SamplerState sampler)
    {
        if (!hookedFromHudRenderer)
            orig(blend, sampler);
        else
            orig(BlendState.NonPremultiplied, sampler);
    }

    private void Level_Begin(On.Celeste.Level.orig_Begin orig, Level self)
    {
        orig(self);
        self.Add(new OverLayerEntity());
    }

    public override void Unload()
    {
        On.Celeste.Level.Begin -= this.Level_Begin;
        On.Celeste.HiresRenderer.BeginRender -= this.HiresRenderer_BeginRender;
        On.Celeste.HudRenderer.RenderContent -= this.HudRenderer_RenderContent;
    }
}
