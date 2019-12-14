﻿using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Items.Repository
{

    public class ApiUpdateRepositoryGenerator : ApplicationCodeGenerateItem
    {
        public ApiUpdateRepositoryGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.ApiUpdateRepositoryClass;
        }

        public override void Execute()
        {
            StringBuilder interfaceMethod = new StringBuilder();

            interfaceMethod.AppendLine($"Task {this.Context.className}(command.Data);");
            interfaceMethod.AppendLine("");
            IFVsFile vsFile = this.GetVsFile();
            this.Context.fileSystem.FormatCode(interfaceMethod.ToString(), vsFile.FileExtension, $"I{vsFile.FileName}");


            CSMethod repositoryMethod = new CSMethod(this.Context.className, "void", "public");
            repositoryMethod.IsAsync = true;
            repositoryMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = this.Context.className + "Command" });


            repositoryMethod.Body += $"{this.Context.classType.Name} entity = new {this.Context.classType.Name}();" + Environment.NewLine;


            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                repositoryMethod.Body += $"entity.{classProperty.Name} = command.Data.{classProperty.Name};" + Environment.NewLine;
            }

            repositoryMethod.Body += $"this.Add(entity);" + Environment.NewLine;

            repositoryMethod.Body += $"await this.UnitOfWork.SaveChangesAsync();" + Environment.NewLine;
            repositoryMethod.Body += $"command.Data.Id = entity.Id;" + Environment.NewLine;

            var repositoryMethodCode = repositoryMethod.GenerateCode().Template;

            this.Context.fileSystem.FormatCode(repositoryMethodCode, vsFile.FileExtension, vsFile.FileName);

        }
    }
}
