using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using IF.Tools.CodeGenerator.VsAutomation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator
{
    public abstract class CSApplicationCodeGeneratorEngineBase
    {
        
        public readonly ApplicationCodeGeneratorContext Context;

        public abstract void UpdateContext();

        public abstract void SetItemActive(VSFileType type);
        
        public List<ApplicationCodeGenerateItem> Items { get; set; }

        

        public CSApplicationCodeGeneratorEngineBase(ApplicationCodeGeneratorContext context)
        {
            this.Context = context;            
            this.Items = new List<ApplicationCodeGenerateItem>();            
        }      

        public void Generate()
        {
            //this.UpdateContext();

            foreach (var item in Items)
            {
                item.Execute();
            }
        }

     

       
    }
}
