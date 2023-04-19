using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste.Mod.SalKeyOverlayer;

public class OverLayerEntity : Entity
{
    protected PixelFont font;
    public List<ButtonInfo> Buttons { get; protected set; }

    public OverLayerEntity()
    {
        Tag = Tags.HUD | Tags.Global | Tags.PauseUpdate | Tags.TransitionUpdate;
        Depth = -50;
        font = Dialog.Languages["schinese"].Font;
        Color defaultNormal = Color.White with { A = 125 };
        Color defaultPressed = Color.LimeGreen with { A = 125 };
        Buttons = new()
        {
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-150, Engine.Height-225), 75 * Vector2.One,
                KeyToButton(Keys.Up), "上", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-150, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.Down), "下", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-225, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.Left), "左", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-75, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.Right), "右", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-225-75*3, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.Z), "Z", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-225-75*2, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.X), "X", 32
                ),
            new ButtonInfo(
                defaultNormal, defaultPressed, new Vector2(Engine.Width-100-225-75*1, Engine.Height-150), 75 * Vector2.One,
                KeyToButton(Keys.C), "C", 32
                )
        };
        //Buttons = SalKeyOverlayer.Settings.Buttons ?? Buttons;
        //SalKeyOverlayer.Settings.Buttons = Buttons;

        foreach (var b in Buttons)
        {
            ActiveButtonBinding(b.Button);
        }
    }

    public override void Render()
    {
        base.Render();
        foreach (var b in Buttons)
        {
            Color c = b.Button.Button.Check ? b.ColorPressed : b.Color;
            Draw.Rect(b.Position, b.Size.X, b.Size.Y, c);
            font.Draw(b.BaseSize,
                b.Text,
                b.Position + b.Size / 2,
                Vector2.One / 2,
                Vector2.One,
                Color.Black
                );
        }
    }

    protected static void ActiveButtonBinding(ButtonBinding buttonBinding)
    {
        buttonBinding.Button = new(buttonBinding.Binding, 0, 0f, 0f);
    }

    public ButtonBinding KeyToButton(Keys key)
    {
        return new ButtonBinding(0, key);
    }
}

//no c#12 available, bad (qwq
public class ButtonInfo
{
    public Color Color { get; set; }
    public Color ColorPressed { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public ButtonBinding Button { get; set; }
    public string Text { get; set; }
    public float BaseSize { get; set; }

    public ButtonInfo(Color color, Color colorPressed, Vector2 position, Vector2 size, ButtonBinding button, string text, float baseSize)
    {
        Color = color;
        ColorPressed = colorPressed;
        Position = position;
        Size = size;
        Button = button;
        Text = text;
        BaseSize = baseSize;
    }

    public ButtonInfo()
    {

    }
}