﻿// ----------------------------------------------------------------------
// File: 		VTextEditorAdditionalMonobehaviours
// Organisation: 	Virtence GmbH
// Department:   	Simulation Development
// Copyright:    	© 2014 Virtence GmbH. All rights reserved
// Author:       	Silvio Lange (silvio.lange@virtence.com)
// ----------------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

/// <summary>
/// 
/// </summary>
public class VTextEditorAdditionalComponents : AbstractVTextEditorComponent
{	
	#region EVENTS

	#endregion // EVENTS


	#region CONSTANTS
    /// <summary>
    /// the width of the labels in front of controls
    /// </summary>
    private const float LABEL_WIDTH = 105;

    /// <summary>
    /// the width of images in the help sections
    /// </summary>
    private const float HELP_IMAGE_WIDTH = 50.0f;

    /// <summary>
    /// the height of the scroll view which shows the help text (in the help regions)
    /// </summary>
    private const float HELP_SCROLLVIEW_HEIGHT = 60.0f;
	#endregion // CONSTANTS


	#region FIELDS
    SerializedProperty _additionalComponentsSection;                                        // the section of the vtext object which holds the additional components
    SerializedProperty _referenceObjects;                                                   // the objects which holds the additional components

    #region INFOFIELDS
    private AnimBool _showAdditionalComponentsInfo = new AnimBool();                        // show or hide the rigidbody info

    private Texture _additionalComponentsInfoHelpImage;                                     // the image which is shown in the rigidbody help box

    private Vector2 _additionalComponentsInfoHelpTextScrollPosition = Vector2.zero;         // the scrollview position for the rigidbody help text
    #endregion // INFOFIELDS
	#endregion // FIELDS


	#region PROPERTIES

	#endregion // PROPERTIES


	#region CONSTRUCTORS

    public VTextEditorAdditionalComponents(SerializedObject obj, Editor currentEditor) 
	{
        _additionalComponentsSection = obj.FindProperty("AdditionalComponents");
        _referenceObjects = _additionalComponentsSection.FindPropertyRelative("_additionalComponentsObject");

        // the images in the help screens
        _additionalComponentsInfoHelpImage = Resources.Load("Images/Icons/Help/scripts_icon") as Texture;

        // add repaints if the animated values are changed
        _showAdditionalComponentsInfo.valueChanged.AddListener(currentEditor.Repaint);
	}

	#endregion // CONSTRUCTORS


	#region METHODS
    /// <summary>
    /// draw the ui for this component
    /// 
    /// returns true if this aspect of the VText should be updated (mesh, layout, physics, etc)
    /// </summary>
    public override bool DrawUI()
    {
        EditorGUIUtility.labelWidth = LABEL_WIDTH;

        bool updateAdditionalComponents = false;
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("E X P E R I M E N T A L !!!!!");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        Object newObject = EditorGUILayout.ObjectField("Reference objects:", _referenceObjects.objectReferenceValue, typeof(GameObject), true);
        if (newObject != _referenceObjects.objectReferenceValue) {
            _referenceObjects.objectReferenceValue = newObject;
            updateAdditionalComponents = true;
        }

        VTextEditorGUIHelper.DrawHelpButton(ref _showAdditionalComponentsInfo);
        GUILayout.EndHorizontal();

        if (EditorGUILayout.BeginFadeGroup(_showAdditionalComponentsInfo.faded)) {
            string txt = VTextEditorGUIHelper.ConvertStringToHelpWindowHeader("Additional components:") + "\n\n" +               
                VTextEditorGUIHelper.ConvertStringToHelpWindowWarning("Be careful! This is experimental and can lead to Unity-Editor crashes!") + "\n\n" +
                "This allows you to add arbitrary components (like scripts) to each letter of your text. To do so you have to create a " +
                "<b>reference object</b> in your scene and add all components you want for your letters to this <b>reference</b> object. " +
                "Then you can fill out each exposed variable on this object to fit your needs. <b>It is absolutely fine to disable the reference object and all" +
                "of its components in the scene.</b> \n" +
                "Now you can drag and drop the <b>reference object</b> into the 'Reference objects' field. When your text is rebuilded it will copy " +
                "all components from your <b>reference object</b> and add them to each letter. Even the specified values of the components are copied. \n\n" +
                "Because some components are generated by VText or by Unity itself it is not possible to copy or replace all available components. The following " +
                "component types (or sub-types of them) will <b>NOT</b> be deleted, overwritten or added: \n" +
                VTextEditorGUIHelper.ConvertStringToHelpWindowListItem("- Transform") + "\n" +
                VTextEditorGUIHelper.ConvertStringToHelpWindowListItem("- Renderer") + "\n" +
                VTextEditorGUIHelper.ConvertStringToHelpWindowListItem("- Meshfilter") + "\n" +
                VTextEditorGUIHelper.ConvertStringToHelpWindowListItem("- RigidBody") + "\n" +
                VTextEditorGUIHelper.ConvertStringToHelpWindowListItem("- Collider") + "\n\n" +
                "We actually know that it will " + VTextEditorGUIHelper.ConvertStringToHelpWindowWarning("crash") + " if you add a VTextInterface or " +
                "a script which references a VTextInterface. Furthermor some of the Unity-internal components (like <b>AudioSource</b>) will throw errors " +
                "when we copy all available variables of that component because some of them are deprecated or completely out of order ... but still available " +
                "except the issue that Unity will throw errors if you use them. \n" +
                "We are working on these issues and maybe we can find solutions :).";


            DrawHelpWindow(_additionalComponentsInfoHelpImage, txt, ref _additionalComponentsInfoHelpTextScrollPosition, ref _showAdditionalComponentsInfo);
        }
        EditorGUILayout.EndFadeGroup();

        return updateAdditionalComponents;
    }
   
    #region HELP WINDOWS
    /// <summary>
    /// Draws the help window with the specified parameters
    /// </summary>
    private void DrawHelpWindow(Texture helpImage, string helpText, ref Vector2 helpTextScrollbarPosition, ref AnimBool showHelpWindowVariable) {
        int currentIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();

        // the image
        VTextEditorGUIHelper.DrawBorderedImage(helpImage, HELP_IMAGE_WIDTH);
        float imgHeight = (float) helpImage.height / helpImage.width * HELP_IMAGE_WIDTH;
        float borderOffset = 6.0f;      // there is a 3-pixel space to each side when put the image into a border (like we do)

        // the help text
        helpTextScrollbarPosition = GUILayout.BeginScrollView(helpTextScrollbarPosition, "box", GUILayout.Height(imgHeight + borderOffset));
        EditorGUILayout.LabelField(helpText, VTextEditorGUIHelper.HelpTextStyle);
        GUILayout.EndScrollView();

        // close button
        if (GUILayout.Button(new GUIContent("x", "Close help"), GUILayout.ExpandWidth(false))) {
            showHelpWindowVariable.target = false;
        }
        GUILayout.Space(5);     // space 5 pixel
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        EditorGUI.indentLevel = currentIndent;
    }
    #endregion // HELP WINDOWS
	#endregion // METHODS


	#region EVENT HANDLERS

	#endregion // EVENT HANDLERS
}
