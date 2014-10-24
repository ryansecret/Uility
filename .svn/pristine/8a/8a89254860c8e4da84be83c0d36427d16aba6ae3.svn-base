using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility
{
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Windows;

    using Microsoft.CSharp;

    public  class DynamicCompile
    {
        private CSharpCodeProvider codeProvider = new CSharpCodeProvider();
        public bool Compile(string code, string outPath)
        {
            ICodeCompiler codeCompiler = this.codeProvider.CreateCompiler();
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.ReferencedAssemblies.Add("DynamicModelLibrary.dll");
            compilerParameters.ReferencedAssemblies.Add("SZTY.IRMS.DAL.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Data.OscarClient.dll");
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Data.dll");

            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = false;

            CompilerResults results = codeCompiler.CompileAssemblyFromSource(
                compilerParameters, this.GernerateCode(code));

            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder();
                foreach (CompilerError ce in results.Errors)
                {
                    if (errors.Length > 0)
                    {
                        errors.Append(Environment.NewLine + ce.ErrorText);
                    }
                    else
                    {
                        errors.Append(ce.ErrorText);
                    }
                }


                MessageBox.Show(errors.ToString());
            }
            else
            {
                File.Copy(results.PathToAssembly, outPath, true);
                return true;
            }
            return false;
        }

        public string startPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private string GernerateCode(string codeString)
        {
            var list = File.ReadAllLines(Path.Combine(startPath, "Template.txt")).ToList();
            int index = list.FindIndex(0, d => d.Trim().Equals("#region process")) + 1;

            //list.Insert(index, string.Format("ConnectionManager.ExecuteNonQuery(\"{0}\");",sql.ToUpper()));
            list.Insert(index, codeString);
            StringBuilder code = new StringBuilder();
            foreach (var item in list)
            {
                code.AppendLine(item.TrimEnd());
            }
            return code.ToString();
        }
    }
}
