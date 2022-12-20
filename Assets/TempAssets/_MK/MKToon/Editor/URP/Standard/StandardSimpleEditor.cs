﻿//////////////////////////////////////////////////////
// MK Toon URP Standard Simple Editor        		//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UnityEditor.Utils;
using UnityEditorInternal;
using EditorHelper = MK.Toon.Editor.EditorHelper;

namespace MK.Toon.Editor.URP
{
    internal class StandardSimpleEditor : MK.Toon.Editor.SimpleEditorBase 
    {
        protected override void DrawEmissionFlags(MaterialEditor materialEditor)
        {

        }

        protected override void EmissionRealtimeSetup(Material material)
        {
            //URP: realtime emissive not supported
            material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
            if(Properties.emissionColor.GetValue(material).maxColorComponent <= 0)
                material.globalIlluminationFlags |= MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        }
        
        protected override void DrawPipeline(MaterialEditor materialEditor)
        {
            DrawPipelineHeader();

            materialEditor.EnableInstancingField();
            DrawRenderPriority(materialEditor);
        }
    }
}
#endif