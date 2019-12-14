using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Items.Repository
{
    public class AddRepositoryGenerator : ApplicationCodeGenerateItem
    {
        public AddRepositoryGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.AddRepositoryClass;
        }

        public override void Execute()
        {
            StringBuilder interfaceMethod = new StringBuilder();

            interfaceMethod.AppendLine($"Task {this.Context.className}();");
            interfaceMethod.AppendLine("");
            IFVsFile vsFile = this.GetVsFile();
            this.Context.fileSystem.FormatCode(interfaceMethod.ToString(), vsFile.FileExtension,$"I{vsFile.FileName}");


            CSMethod repositoryMethod = new CSMethod("this.Context.className", "void", "public");
            repositoryMethod.IsAsync = true;
            repositoryMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = this.Context.className + "Command" });

            repositoryMethod.Body += $"var entity = await this.repository.GetQuery<{this.Context.classType.Name}>().SingleOrDefaultAsync(k => k.Id == command.Data.Id);" + Environment.NewLine + Environment.NewLine;
            repositoryMethod.Body += $"if (entity == null){{ throw new BusinessException(\"{this.Context.className} : No such entity exists\");}}" + Environment.NewLine + Environment.NewLine;


            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                repositoryMethod.Body += $"entity.{classProperty.Name} = command.Data.{classProperty.Name};" + Environment.NewLine;
            }


            repositoryMethod.Body += $"this.repository.Update(entity);" + Environment.NewLine;
            repositoryMethod.Body += $"await this.UnitOfWork.SaveChangesAsync();" + Environment.NewLine;

            var repositoryMethodCode = repositoryMethod.GenerateCode().Template;

            this.Context.fileSystem.FormatCode(repositoryMethodCode, vsFile.FileExtension, vsFile.FileName);

        }
    }
}
