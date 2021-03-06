﻿using Microsoft.CodeAnalysis;

namespace Meziantou.Analyzer
{
    internal static class SymbolExtensions
    {
        public static bool IsVisibleOutsideOfAssembly(this ISymbol symbol)
        {
            if (symbol == null)
                return false;

            if (symbol.DeclaredAccessibility != Accessibility.Public &&
                symbol.DeclaredAccessibility != Accessibility.Protected &&
                symbol.DeclaredAccessibility != Accessibility.ProtectedOrInternal)
            {
                return false;
            }

            if (symbol.ContainingType == null)
                return true;

            return IsVisibleOutsideOfAssembly(symbol.ContainingType);
        }

        public static bool IsOperator(this ISymbol symbol)
        {
            if (symbol is IMethodSymbol methodSymbol)
            {
                return methodSymbol.MethodKind == MethodKind.UserDefinedOperator || methodSymbol.MethodKind == MethodKind.Conversion;
            }

            return false;
        }

        public static bool IsOverrideOrInterfaceImplementation(this ISymbol symbol)
        {
            if (symbol is IMethodSymbol methodSymbol)
                return methodSymbol.IsOverride || methodSymbol.IsInterfaceImplementation();

            if (symbol is IPropertySymbol propertySymbol)
                return propertySymbol.IsOverride || propertySymbol.IsInterfaceImplementation();

            if (symbol is IEventSymbol eventSymbol)
                return eventSymbol.IsOverride || eventSymbol.IsInterfaceImplementation();

            return false;
        }
    }
}
