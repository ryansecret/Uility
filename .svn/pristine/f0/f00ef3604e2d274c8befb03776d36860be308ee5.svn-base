// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleCodeDom.cs" company="">
//   
// </copyright>
// <summary>
//   This code example creates a graph using a CodeCompileUnit and
//   generates source code for the graph using the CSharpCodeProvider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.Example
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// This code example creates a graph using a CodeCompileUnit and  
    ///   generates source code for the graph using the CSharpCodeProvider.
    /// </summary>
    internal class Sample
    {
        #region Constants and Fields

        /// <summary>
        ///   The name of the file to contain the source code.
        /// </summary>
        private const string outputFileName = "SampleCode.cs";

        /// <summary>
        ///   The only class in the compile unit. This class contains 2 fields,
        ///   3 properties, a constructor, an entry point, and 1 simple method.
        /// </summary>
        private CodeTypeDeclaration targetClass;

        /// <summary>
        ///   Define the compile unit to use for code generation.
        /// </summary>
        private CodeCompileUnit targetUnit;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Sample"/> class. 
        ///   Define the class.
        /// </summary>
        public Sample()
        {
            this.targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("CodeDOMSample");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            this.targetClass = new CodeTypeDeclaration("CodeDOMCreatedClass");
            this.targetClass.IsClass = true;
            this.targetClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            samples.Types.Add(this.targetClass);
            this.targetUnit.Namespaces.Add(samples);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a constructor to the class.
        /// </summary>
        public void AddConstructor()
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            // Add parameters.
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "width"));
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Double), "height"));

            // Add field initialization logic
            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue");
            constructor.Statements.Add(
                new CodeAssignStatement(widthReference, new CodeArgumentReferenceExpression("width")));
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "heightValue");
            constructor.Statements.Add(
                new CodeAssignStatement(heightReference, new CodeArgumentReferenceExpression("height")));
            this.targetClass.Members.Add(constructor);
        }

        /// <summary>
        /// Add an entry point to the class.
        /// </summary>
        public void AddEntryPoint()
        {
            CodeEntryPointMethod start = new CodeEntryPointMethod();
            CodeObjectCreateExpression objectCreate =
                new CodeObjectCreateExpression(
                    new CodeTypeReference("CodeDOMCreatedClass"), 
                    new CodePrimitiveExpression(5.3), 
                    new CodePrimitiveExpression(6.9));

            // Add the statement:
            // "CodeDOMCreatedClass testClass = 
            // new CodeDOMCreatedClass(5.3, 6.9);"
            start.Statements.Add(
                new CodeVariableDeclarationStatement(
                    new CodeTypeReference("CodeDOMCreatedClass"), "testClass", objectCreate));

            // Creat the expression:
            // "testClass.ToString()"
            CodeMethodInvokeExpression toStringInvoke =
                new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("testClass"), "ToString");

            // Add a System.Console.WriteLine statement with the previous 
            // expression as a parameter.
            start.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression("System.Console"), "WriteLine", toStringInvoke));
            this.targetClass.Members.Add(start);
        }

        /// <summary>
        /// Adds two fields to the class.
        /// </summary>
        public void AddFields()
        {
            // Declare the widthValue field.
            CodeMemberField widthValueField = new CodeMemberField();
            widthValueField.Attributes = MemberAttributes.Private;
            widthValueField.Name = "widthValue";
            widthValueField.Type = new CodeTypeReference(typeof(System.Double));
            widthValueField.Comments.Add(new CodeCommentStatement("The width of the object."));
            this.targetClass.Members.Add(widthValueField);

            // Declare the heightValue field
            CodeMemberField heightValueField = new CodeMemberField();
            heightValueField.Attributes = MemberAttributes.Private;
            heightValueField.Name = "heightValue";
            heightValueField.Type = new CodeTypeReference(typeof(System.Double));
            heightValueField.Comments.Add(new CodeCommentStatement("The height of the object."));
            this.targetClass.Members.Add(heightValueField);
        }

        /// <summary>
        /// Adds a method to the class. This method multiplies values stored 
        ///   in both fields.
        /// </summary>
        public void AddMethod()
        {
            // Declaring a ToString method
            CodeMemberMethod toStringMethod = new CodeMemberMethod();
            toStringMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            toStringMethod.Name = "ToString";
            toStringMethod.ReturnType = new CodeTypeReference(typeof(System.String));

            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Width");
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Height");
            CodeFieldReferenceExpression areaReference =
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Area");

            // Declaring a return statement for method ToString.
            CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement();

            // This statement returns a string representation of the width,
            // height, and area.
            string formattedOutput = "The object:" + Environment.NewLine + " width = {0}," + Environment.NewLine
                                     + " height = {1}," + Environment.NewLine + " area = {2}";
            returnStatement.Expression = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.String"), 
                "Format", 
                new CodePrimitiveExpression(formattedOutput), 
                widthReference, 
                heightReference, 
                areaReference);
            toStringMethod.Statements.Add(returnStatement);
            this.targetClass.Members.Add(toStringMethod);
        }

        /// <summary>
        /// Add three properties to the class.
        /// </summary>
        public void AddProperties()
        {
            // Declare the read-only Width property.
            CodeMemberProperty widthProperty = new CodeMemberProperty();
            widthProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            widthProperty.Name = "Width";
            widthProperty.HasGet = true;
            widthProperty.Type = new CodeTypeReference(typeof(System.Double));
            widthProperty.Comments.Add(new CodeCommentStatement("The Width property for the object."));
            widthProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue")));
            this.targetClass.Members.Add(widthProperty);

            // Declare the read-only Height property.
            CodeMemberProperty heightProperty = new CodeMemberProperty();
            heightProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            heightProperty.Name = "Height";
            heightProperty.HasGet = true;
            heightProperty.Type = new CodeTypeReference(typeof(System.Double));
            heightProperty.Comments.Add(new CodeCommentStatement("The Height property for the object."));
            heightProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "heightValue")));
            this.targetClass.Members.Add(heightProperty);

            // Declare the read only Area property.
            CodeMemberProperty areaProperty = new CodeMemberProperty();
            areaProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            areaProperty.Name = "Area";
            areaProperty.HasGet = true;
            areaProperty.Type = new CodeTypeReference(typeof(System.Double));
            areaProperty.Comments.Add(new CodeCommentStatement("The Area property for the object."));

            // Create an expression to calculate the area for the get accessor 
            // of the Area property.
            CodeBinaryOperatorExpression areaExpression =
                new CodeBinaryOperatorExpression(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "widthValue"), 
                    CodeBinaryOperatorType.Multiply, 
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "heightValue"));
            areaProperty.GetStatements.Add(new CodeMethodReturnStatement(areaExpression));
            this.targetClass.Members.Add(areaProperty);
        }

        /// <summary>
        /// Generate CSharp source code from the compile unit.
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(this.targetUnit, sourceWriter, options);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the CodeDOM graph and generate the code.
        /// </summary>
        private static void Main()
        {
            Sample sample = new Sample();
            sample.AddFields();
            sample.AddProperties();
            sample.AddMethod();
            sample.AddConstructor();
            sample.AddEntryPoint();
            sample.GenerateCSharpCode(outputFileName);
        }

        #endregion
    }
}