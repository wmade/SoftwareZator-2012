﻿// *****************************************************************************
// 
//  © Veler Software 2012. All rights reserved.
//  The current code and the associated software are the proprietary 
//  information of Etienne Baudoux from Veler Software and are
//  supplied subject to licence terms.
// 
//  www.velersoftware.com
// *****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using VelerSoftware.SZC.Obfuscator.Confuser.Core.Engines;

namespace VelerSoftware.SZC.Obfuscator.Confuser.Core.Confusions
{
    public class NameConfusion : IConfusion
    {
        class Phase1 : StructurePhase
        {
            public Phase1(NameConfusion nc) { this.nc = nc; }
            NameConfusion nc;
            public override IConfusion Confusion
            {
                get { return nc; }
            }

            public override int PhaseID
            {
                get { return 1; }
            }

            public override Priority Priority
            {
                get { return Priority.MetadataLevel; }
            }

            public override bool WholeRun
            {
                get { return false; }
            }

            ModuleDefinition mod;
            public override void Initialize(ModuleDefinition mod)
            {
                this.mod = mod;
            }

            public override void DeInitialize()
            {
                foreach (Resource res in mod.Resources)
                {
                    if (!res.Name.EndsWith(".resources")) continue;
                    string cult = mod.Assembly.Name.Culture;
                    Identifier id = new Identifier()
                    {
                        scope = string.IsNullOrEmpty(cult) ? res.Name.Substring(0, res.Name.LastIndexOf('.')) : res.Name.Substring(0, res.Name.LastIndexOf('.', res.Name.LastIndexOf('.') - 1)),
                        name = string.IsNullOrEmpty(cult) ? res.Name.Substring(res.Name.LastIndexOf('.') + 1) : res.Name.Substring(res.Name.LastIndexOf('.', res.Name.LastIndexOf('.') - 1) + 1)
                    };
                    foreach (IReference refer in (res as IAnnotationProvider).Annotations["RenRef"] as List<IReference>)
                    {
                        refer.UpdateReference(id, id);
                    }
                }
            }

            public override void Process(ConfusionParameter parameter)
            {
                IMemberDefinition mem = parameter.Target as IMemberDefinition;
                if (mem is TypeDefinition)
                {
                    TypeDefinition type = mem as TypeDefinition;
                    if ((bool)(type as IAnnotationProvider).Annotations["RenOk"])
                    {
                        type.Name = ObfuscationHelper.GetNewName(type.FullName);
                        type.Namespace = "";
                        Identifier id = (Identifier)(type as IAnnotationProvider).Annotations["RenId"];
                        Identifier n = id;
                        n.name = type.Name;
                        n.scope = type.Namespace;
                        foreach (IReference refer in (type as IAnnotationProvider).Annotations["RenRef"] as List<IReference>)
                        {
                            refer.UpdateReference(id, n);
                        }
                    }
                }
                else if (mem is MethodDefinition)
                {
                    MethodDefinition mtd = mem as MethodDefinition;
                    PerformMethod(mtd);
                }
                else if ((bool)(mem as IAnnotationProvider).Annotations["RenOk"])
                {
                    mem.Name = ObfuscationHelper.GetNewName(mem.Name);
                    Identifier id = (Identifier)(mem as IAnnotationProvider).Annotations["RenId"];
                    Identifier n = id;
                    n.scope = mem.DeclaringType.FullName;
                    n.name = mem.Name;
                    foreach (IReference refer in (mem as IAnnotationProvider).Annotations["RenRef"] as List<IReference>)
                    {
                        refer.UpdateReference(id, n);
                    }
                }
            }
                
            void PerformMethod(MethodDefinition mtd)
            {
                if ((bool)(mtd as IAnnotationProvider).Annotations["RenOk"])
                {
                    mtd.Name = ObfuscationHelper.GetNewName(mtd.Name);
                    Identifier id = (Identifier)(mtd as IAnnotationProvider).Annotations["RenId"];
                    Identifier n = id;
                    n.scope = mtd.DeclaringType.FullName;
                    n.name = mtd.Name;
                    foreach (IReference refer in (mtd as IAnnotationProvider).Annotations["RenRef"] as List<IReference>)
                    {
                        refer.UpdateReference(id, n);
                    }
                }

                foreach (ParameterDefinition para in mtd.Parameters)
                {
                    para.Name = ObfuscationHelper.GetNewName(para.Name);
                }

                if (mtd.HasBody)
                {
                    foreach (VariableDefinition var in mtd.Body.Variables)
                    {
                        var.Name = ObfuscationHelper.GetNewName(var.Name);
                    }
                }
            }

            RenameEngine eng;
            public override IEngine GetEngine()
            {
                if (eng == null) eng = new RenameEngine();
                return eng;
            }
        }


        public string ID
        {
            get { return "rename"; }
        }
        public string Name
        {
            get { return "Name Confusion"; }
        }
        public string Description
        {
            get { return "This confusion rename the members to unprintable name thus the decompiled source code can neither be compiled nor read."; }
        }
        public Target Target
        {
            get { return Target.Types | Target.Fields | Target.Methods | Target.Properties | Target.Events; }
        }
        public Preset Preset
        {
            get { return Preset.Minimum; }
        }
        public bool StandardCompatible
        {
            get { return true; }
        }
        public bool SupportLateAddition
        {
            get { return true; }
        }
        public Behaviour Behaviour
        {
            get { return Behaviour.AlterStructure; }
        }

        Phase[] ps;
        public Phase[] Phases
        {
            get
            {
                if (ps == null)
                    ps = new Phase[] { new Phase1(this) };
                return ps;
            }
        }

        string GetSig(MethodReference mtd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mtd.ReturnType.FullName);
            sb.Append(" ");
            sb.Append(mtd.Name);
            sb.Append("(");
            if (mtd.HasParameters)
            {
                for (int i = 0; i < mtd.Parameters.Count; i++)
                {
                    ParameterDefinition param = mtd.Parameters[i];
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    if (param.ParameterType.IsSentinel)
                    {
                        sb.Append("...,");
                    }
                    sb.Append(param.ParameterType.FullName);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
        string GetSig(FieldReference fld)
        {
            return fld.FieldType.FullName + " " + fld.Name;
        }
    }
}
