#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EasyQuestSwitch.Fields;

namespace EasyQuestSwitch.Types
{
    [AddComponentMenu("")]
    public class Type_MeshCollider : Type_Collider
    {
        [System.NonSerialized]
        private MeshCollider type;

        public SharedBool Convex = new SharedBool();
        public SharedMesh SharedMesh = new SharedMesh();

        public override void Setup(Object type)
        {
            base.Setup(type);
            MeshCollider component = (MeshCollider)type;
            Convex.Setup(component.convex);
            SharedMesh.Setup(component.sharedMesh);
        }

        public override void Upgrade(Object type, int currentVersion)
        {
            base.Upgrade(type, currentVersion);
            if (currentVersion < EQS_Data.UpdateAddIOS)
            {
                Convex.iOS = Convex.Quest;
                SharedMesh.iOS = SharedMesh.Quest;
            }
        }

        public override void Process(Object type, BuildTarget buildTarget)
        {
            MeshCollider component = (MeshCollider)type;
            component.enabled = Enabled.Get(buildTarget);
            component.isTrigger = IsTrigger.Get(buildTarget) && Convex.Get(buildTarget) ? true : false;
            component.convex = Convex.Get(buildTarget);
            component.sharedMesh = SharedMesh.Get(buildTarget);
        }
    }
}
#endif

