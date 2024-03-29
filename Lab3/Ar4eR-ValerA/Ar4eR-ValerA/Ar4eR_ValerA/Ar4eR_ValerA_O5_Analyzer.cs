﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ar4eR_ValerA
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Ar4eR_ValerA_O5_Analyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Ar4eR_ValerA";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.O5_AnalyzerTitle),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat =
            new LocalizableResourceString(nameof(Resources.O5_AnalyzerMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description =
            new LocalizableResourceString(nameof(Resources.O5_AnalyzerDescription), Resources.ResourceManager,
                typeof(Resources));

        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat,
            Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(Rule); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration);
        }

        private static void AnalyzeMethodDeclaration(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            if (methodDeclaration.Identifier.ToString().ToLower().Contains("try") &&
                methodDeclaration.ReturnType.ToString() != "bool")
            {
                var diagnostic = Diagnostic.Create(
                    Rule,
                    context.Node.GetLocation(),
                    methodDeclaration.Identifier.ToString());

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}