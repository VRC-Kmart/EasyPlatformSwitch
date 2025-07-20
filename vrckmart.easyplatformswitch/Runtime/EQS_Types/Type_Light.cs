#if UNITY_EDITOR
using UnityEngine;
using EasyQuestSwitch.Fields;
using UnityEditor;

namespace EasyQuestSwitch.Types
{
    [AddComponentMenu("")]
    public class Type_Light : Type_Behaviour
    {
        [System.NonSerialized]
        private Light type;

        public SharedLightType LightType = new SharedLightType();
        public SharedFloat Range = new SharedFloat();
        public SharedColor Color = new SharedColor();
        public SharedLightmapBakeType Mode = new SharedLightmapBakeType();
        public SharedFloat Intensity = new SharedFloat();
        public SharedFloat IndirectMultiplier = new SharedFloat();
        public SharedLightShadows ShadowType = new SharedLightShadows();
        public SharedLightRenderMode RenderMode = new SharedLightRenderMode();

        public override void Setup(Object type)
        {
            base.Setup(type);
            Light component = (Light)type;
            LightType.Setup(component.type);
            Range.Setup(component.range);
            Color.Setup(component.color);
            Mode.Setup(component.lightmapBakeType);
            Intensity.Setup(component.intensity);
            IndirectMultiplier.Setup(component.bounceIntensity);
            ShadowType.Setup(component.shadows);
            RenderMode.Setup(component.renderMode);
        }

        public override void Upgrade(Object type, int currentVersion)
        {
            base.Upgrade(type, currentVersion);
            Light component = (Light)type;
            // Bug: This upgrade code was never called originally.
            // This is due to a typo in a conditional that snuck in.
            // It's probably too late to fix it this way.
            if (currentVersion < EQS_Data.UpdateFixMatsAndLights)
            {
                LightType.Setup(component.type);
                Range.Setup(component.range);
                RenderMode.Setup(component.renderMode);
            }

            if (currentVersion < EQS_Data.UpdateAddIOS)
            {
                LightType.iOS = LightType.Quest;
                Range.iOS = Range.Quest;
                Color.iOS = Color.Quest;
                Mode.iOS = Mode.Quest;
                Intensity.iOS = Intensity.Quest;
                IndirectMultiplier.iOS = IndirectMultiplier.Quest;
                ShadowType.iOS = ShadowType.Quest;
                RenderMode.iOS = RenderMode.Quest;
            }
        }

        public override void Process(Object type, BuildTarget buildTarget)
        {
            base.Process(type, buildTarget);
            Light component = (Light)type;
            component.type = LightType.Get(buildTarget);
            component.range = Range.Get(buildTarget);
            component.color = Color.Get(buildTarget);
            component.lightmapBakeType = Mode.Get(buildTarget);
            component.intensity = Intensity.Get(buildTarget);
            component.bounceIntensity = IndirectMultiplier.Get(buildTarget);
            component.shadows = ShadowType.Get(buildTarget);
            component.renderMode = RenderMode.Get(buildTarget);
        }
    }
}
#endif