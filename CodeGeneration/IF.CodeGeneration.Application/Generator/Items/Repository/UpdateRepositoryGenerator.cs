using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Items.Repository
{

    public class UpdateRepositoryGenerator : ApplicationCodeGenerateItem
    {
        public UpdateRepositoryGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.UpdateRepositoryClass;
        }

        public override void Execute()
        {
            StringBuilder interfaceMethod = new StringBuilder();

            interfaceMethod.AppendLine($"Task {this.Context.className}({this.Context.className}Dto data);");
            interfaceMethod.AppendLine("");
            IFVsFile vsFile = this.GetVsFile();
            this.Context.fileSystem.FormatCode(interfaceMethod.ToString(), vsFile.FileExtension, $"I{vsFile.FileName}", "");


            CSMethod repositoryMethod = new CSMethod(this.Context.className, "void", "public");
            repositoryMethod.IsAsync = true;
            repositoryMethod.Parameters.Add(new CsMethodParameter() { Name = "dto", Type = this.Context.className + "Dto" });


            repositoryMethod.Body += $"var entity = await this.GetQuery<{this.Context.classType.Name}>().SingleOrDefaultAsync(k => k.Id == dto.Id);" + Environment.NewLine + Environment.NewLine;
            repositoryMethod.Body += $"if (entity == null){{ throw new BusinessException(\"{this.Context.className} : No such entity exists\");}}" + Environment.NewLine + Environment.NewLine;


            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                repositoryMethod.Body += $"entity.{classProperty.Name} = dto.{classProperty.Name};" + Environment.NewLine;
            }

            repositoryMethod.Body += $"this.Update(entity);" + Environment.NewLine;

            repositoryMethod.Body += $"await this.UnitOfWork.SaveChangesAsync();" + Environment.NewLine;
//            repositoryMethod.Body += $"command.Data.Id = entity.Id;" + Environment.NewLine;

            var repositoryMethodCode = repositoryMethod.GenerateCode().Template;

            this.Context.fileSystem.FormatCode(repositoryMethodCode, vsFile.FileExtension, vsFile.FileName, "");

        }
    }
}
