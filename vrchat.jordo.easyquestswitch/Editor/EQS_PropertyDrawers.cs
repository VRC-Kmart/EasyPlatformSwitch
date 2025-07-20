#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EasyQuestSwitch.Types;
using UnityEditorInternal;

namespace EasyQuestSwitch.Fields
{

    public class BaseDrawer : PropertyDrawer
    {
        protected float labelRatio = 0.25f;
        protected float optionRatio = 0.735f/3;
        protected float dividerRatio = 0.0075f;

        protected Rect labelRect;
        protected Rect optionARect;
        protected Rect dividerRect;
        protected Rect optionBRect;
        protected Rect dividerRect2;
        protected Rect optionCRect;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            labelRect = GetRatioedRect(position, labelRatio);
            optionARect = GetRatioedRect(position, optionRatio);
            dividerRect = GetRatioedRect(position, dividerRatio);
            optionARect.x += labelRect.width;
            dividerRect.x += labelRect.width + optionARect.width;
            optionBRect = optionARect;
            optionBRect.x += optionARect.width + dividerRect.width;
            dividerRect2 = dividerRect;
            dividerRect.x += dividerRect.width / 2;
            dividerRect.width = 1;
            dividerRect2.x += optionBRect.width + dividerRect2.width;
            dividerRect2.x += dividerRect2.width / 2;
            optionCRect = optionBRect;
            optionCRect.x += optionBRect.width + dividerRect2.width;
            dividerRect2.width = 1;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public Rect GetRatioedRect(Rect currentRect, float ratio)
        {
            Rect newRect = currentRect;
            newRect.width = currentRect.width * ratio;
            return newRect;
        }
    }

    public class PlatformLabel : BaseDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.LabelField(optionARect, "PC");
            EditorGUI.DrawRect(dividerRect, Color.grey);
            EditorGUI.LabelField(optionBRect, "Quest");
            EditorGUI.DrawRect(dividerRect2, Color.grey);
            EditorGUI.LabelField(optionCRect, "iOS");
        }
    }

    [CustomPropertyDrawer(typeof(SharedBool), false)
    , CustomPropertyDrawer(typeof(SharedInt), false)
    , CustomPropertyDrawer(typeof(SharedFloat), false)
    , CustomPropertyDrawer(typeof(SharedVector2), false)
    , CustomPropertyDrawer(typeof(SharedVector2Int), false)
    , CustomPropertyDrawer(typeof(SharedVector3), false)
    , CustomPropertyDrawer(typeof(SharedVector3Int), false)
    , CustomPropertyDrawer(typeof(SharedVector4), false)
    , CustomPropertyDrawer(typeof(SharedQuaternion), false)
    , CustomPropertyDrawer(typeof(SharedString), false)
    , CustomPropertyDrawer(typeof(SharedTransform), false)
    , CustomPropertyDrawer(typeof(SharedMesh), false)
    , CustomPropertyDrawer(typeof(SharedMaterial), false)
    , CustomPropertyDrawer(typeof(SharedShader), false)
    , CustomPropertyDrawer(typeof(SharedLayerMask), false)
    , CustomPropertyDrawer(typeof(SharedRenderer), true)
    // Graphic
    , CustomPropertyDrawer(typeof(SharedSprite), false)
    , CustomPropertyDrawer(typeof(SharedColor), false)
    , CustomPropertyDrawer(typeof(SharedRenderTexture), false)
    , CustomPropertyDrawer(typeof(SharedTexture), false)
    // Animator
    , CustomPropertyDrawer(typeof(SharedRuntimeAnimatorController), false)
    , CustomPropertyDrawer(typeof(SharedAnimatorCullingMode), false)
    // Renderer
    , CustomPropertyDrawer(typeof(SharedLightProbeUsage), false)
    , CustomPropertyDrawer(typeof(SharedReflectionProbeUsage), false)
    , CustomPropertyDrawer(typeof(SharedShadowCastingMode), false)
    // LODGroup
    , CustomPropertyDrawer(typeof(SharedFadeMode), false)
    // Reflection Probe
    , CustomPropertyDrawer(typeof(SharedReflectionProbeMode), false)
    , CustomPropertyDrawer(typeof(SharedReflectionProbeResolution), false)
    // AudioClip
    , CustomPropertyDrawer(typeof(SharedAudioClip), false)
    // RenderTexture
    , CustomPropertyDrawer(typeof(SharedDepthBuffer), false)
    , CustomPropertyDrawer(typeof(SharedAntiAliasing), false)
    , CustomPropertyDrawer(typeof(SharedFilterMode), false)
    // Light
    , CustomPropertyDrawer(typeof(SharedLightmapBakeType), false)
    , CustomPropertyDrawer(typeof(SharedLightShadows), false)
    , CustomPropertyDrawer(typeof(SharedLightType), false)
    , CustomPropertyDrawer(typeof(SharedLightRenderMode), false)]
    public class PropertiesDrawer : BaseDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty PC = property.FindPropertyRelative("PC");
            SerializedProperty Quest = property.FindPropertyRelative("Quest");
            SerializedProperty iOS = property.FindPropertyRelative("iOS");

            EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(optionARect, PC, GUIContent.none);
            EditorGUI.DrawRect(dividerRect, Color.grey);
            EditorGUI.PropertyField(optionBRect, Quest, GUIContent.none);
            EditorGUI.DrawRect(dividerRect2, Color.grey);
            EditorGUI.PropertyField(optionCRect, iOS, GUIContent.none);
            EditorGUI.EndProperty();
        }
    }

    // GameObject
    [CustomPropertyDrawer(typeof(SharedTag), false)]
    public class TagDrawer : BaseDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty PC = property.FindPropertyRelative("PC");
            SerializedProperty Quest = property.FindPropertyRelative("Quest");
            SerializedProperty iOS = property.FindPropertyRelative("iOS");

            EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            PC.stringValue = EditorGUI.TagField(optionARect, PC.stringValue);
            EditorGUI.DrawRect(dividerRect, Color.grey);
            Quest.stringValue = EditorGUI.TagField(optionBRect, Quest.stringValue);
            EditorGUI.DrawRect(dividerRect2, Color.grey);
            iOS.stringValue = EditorGUI.TagField(optionCRect, iOS.stringValue);
            EditorGUI.EndProperty();
        }
    }

    // GameObject
    [CustomPropertyDrawer(typeof(SharedLayer), false)]
    public class LayerDrawer : BaseDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty PC = property.FindPropertyRelative("PC");
            SerializedProperty Quest = property.FindPropertyRelative("Quest");
            SerializedProperty iOS = property.FindPropertyRelative("iOS");

            EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            PC.intValue = EditorGUI.LayerField(optionARect, PC.intValue);
            EditorGUI.DrawRect(dividerRect, Color.grey);
            Quest.intValue = EditorGUI.LayerField(optionBRect, Quest.intValue);
            EditorGUI.DrawRect(dividerRect2, Color.grey);
            iOS.intValue = EditorGUI.LayerField(optionCRect, iOS.intValue);
            EditorGUI.EndProperty();
        }
    }

    // GameObject
    [CustomPropertyDrawer(typeof(SharedStaticEditorFlags), false)]
    public class MaskFieldDrawer : BaseDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty PC = property.FindPropertyRelative("PC");
            SerializedProperty Quest = property.FindPropertyRelative("Quest");
            SerializedProperty iOS = property.FindPropertyRelative("iOS");

            EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            PC.intValue = EditorGUI.MaskField(optionARect, PC.intValue, PC.enumDisplayNames);
            EditorGUI.DrawRect(dividerRect, Color.grey);
            Quest.intValue = EditorGUI.MaskField(optionBRect, Quest.intValue, Quest.enumDisplayNames);
            EditorGUI.DrawRect(dividerRect2, Color.grey);
            iOS.intValue = EditorGUI.MaskField(optionCRect, iOS.intValue, iOS.enumDisplayNames);
            EditorGUI.EndProperty();
        }
    }



}


#endif