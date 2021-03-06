using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(SingularityRenderer), PostProcessEvent.AfterStack, "Custom/Singularity")]
public sealed class Singularity : PostProcessEffectSettings
{
    public FloatParameter minPower = new FloatParameter { value = 1.0f };
    public FloatParameter maxPower = new FloatParameter { value = 1.0f };
    [Range(0.0f, 1.0f)]
    public FloatParameter singularityProgress = new FloatParameter { value = 1.0f };
    [Range(0.0f, 1.0f)]
    public FloatParameter scale = new FloatParameter { value = 1.0f };
    public ColorParameter background = new ColorParameter { value = Color.black };
}

public sealed class SingularityRenderer : PostProcessEffectRenderer<Singularity>
{
    public override void Render(PostProcessRenderContext context)
    {

        var flowManager = GameComponentsLocator.Get<UnityPrototype.GameFlowManager>();

        float singularityProgress = settings.singularityProgress;
        float scale = settings.scale;

        if (Application.isPlaying)
        {
            singularityProgress = flowManager.singularityProgress;
            scale = flowManager.scale;
        }

        var radius = Mathf.Lerp(settings.minPower, settings.maxPower, singularityProgress);

        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Singularity"));
        sheet.properties.SetFloat("_Radius", radius);
        sheet.properties.SetFloat("_SingularityScale", scale);
        sheet.properties.SetColor("_BackgroundColor", settings.background);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}