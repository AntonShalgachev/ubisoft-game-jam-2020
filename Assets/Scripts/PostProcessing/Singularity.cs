using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(SingularityRenderer), PostProcessEvent.AfterStack, "Custom/Singularity")]
public sealed class Singularity : PostProcessEffectSettings
{
    [Range(0.8f, 20f)]
    public FloatParameter radius = new FloatParameter { value = 1.0f };
    public ColorParameter background = new ColorParameter { value = Color.black };
}

public sealed class SingularityRenderer : PostProcessEffectRenderer<Singularity>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Singularity"));
        sheet.properties.SetFloat("_Radius", settings.radius);
        sheet.properties.SetColor("_BackgroundColor", settings.background);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}