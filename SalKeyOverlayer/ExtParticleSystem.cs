using Monocle;

namespace Celeste.Mod.SalKeyOverlayer;

public class ExtParticleSystem : ParticleSystem
{
    protected static ParticleType particleType;



    public ExtParticleSystem(int depth, int maxParticles) 
        : base(depth, maxParticles)
    {
    }

    public override void Update()
    {
        base.Update();
        this.Emit()
    }
}
