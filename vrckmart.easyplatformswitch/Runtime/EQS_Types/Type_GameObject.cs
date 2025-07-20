#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EasyQuestSwitch.Fields;

namespace EasyQuestSwitch.Types
{
    [AddComponentMenu("")]
    public class Type_GameObject : Type_Base
    {
        [System.NonSerialized]
        private GameObject type;

        public SharedBool Active = new SharedBool();
        public SharedTag Tag = new SharedTag();
        public SharedLayer Layer = new SharedLayer();
        public SharedStaticEditorFlags StaticEditorFlags = new SharedStaticEditorFlags();

        public override void Setup(Object type)
        {
            GameObject component = (GameObject)type;
            Active.Setup(component.activeInHierarchy);
            Tag.Setup(component.tag);
            Layer.Setup(component.layer);
            StaticEditorFlags.Setup(GameObjectUtility.GetStaticEditorFlags(component));
        }

        public override void Upgrade(Object type, int currentVersion)
        {
            if (currentVersion < EQS_Data.UpdateAddIOS)
            {
                Active.iOS = Active.Quest;
                Tag.iOS = Tag.Quest;
                Layer.iOS = Layer.Quest;
                StaticEditorFlags.iOS = StaticEditorFlags.Quest;
            }
        }

        public override void Process(Object type, BuildTarget buildTarget)
        {
            GameObject component = (GameObject)type;
            component.SetActive(Active.Get(buildTarget));
            component.tag = Tag.Get(buildTarget);
            component.layer = Layer.Get(buildTarget);
            GameObjectUtility.SetStaticEditorFlags(component, StaticEditorFlags.Get(buildTarget));
        }
    }
}
#endif

