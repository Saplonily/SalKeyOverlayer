using Monocle;

namespace Celeste.Mod.SalKeyOverlayer;

public class BoredEntity : Entity
{
    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        scene.Add(new ExtParticleSystem(Depth, 200));
    }
}
