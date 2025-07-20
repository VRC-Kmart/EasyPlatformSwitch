#if UNITY_EDITOR && VRC_SDK_VRCSDK2
using System;
using UnityEditor;
using UnityEngine;
using VRCSDK2;
using EasyQuestSwitch.Fields;
using Object = UnityEngine.Object;

namespace EasyQuestSwitch.Types
{
    [AddComponentMenu("")]
    public class Type_VRC_SpatialAudioSource : Type_Base
    {
        [System.NonSerialized]
        private VRC_SpatialAudioSource type;

        public SharedFloat Gain = new SharedFloat();
        public SharedFloat Far = new SharedFloat();
        public SharedFloat Near = new SharedFloat();
        public SharedFloat VolumetricRadius = new SharedFloat();
        public SharedBool EnableSpatialization = new SharedBool();

        public override void Setup(Object type)
        {
            VRC_SpatialAudioSource component = (VRC_SpatialAudioSource)type;
            Gain.Setup(component.Gain);
            Far.Setup(component.Far);
            Near.Setup(component.Near);
            VolumetricRadius.Setup(component.VolumetricRadius);
            EnableSpatialization.Setup(component.EnableSpatialization);
        }

        public override void Upgrade(Object type, int currentVersion)
        {
            if (currentVersion < EQS_Data.UpdateAddIOS)
            {
                Gain.iOS = Gain.Quest;
                Far.iOS = Far.Quest;
                Near.iOS = Near.Quest;
                VolumetricRadius.iOS = VolumetricRadius.Quest;
                EnableSpatialization.iOS = EnableSpatialization.Quest;
            }
        }

        public override void Process(Object type, BuildTarget buildTarget)
        {
            VRC_SpatialAudioSource component = (VRC_SpatialAudioSource)type;
            component.Gain = Gain.Get(buildTarget);
            component.Far = Far.Get(buildTarget);
            component.Near = Near.Get(buildTarget);
            component.VolumetricRadius = VolumetricRadius.Get(buildTarget);
            component.EnableSpatialization = EnableSpatialization.Get(buildTarget);
        }
    }
}
#endif
