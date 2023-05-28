using System;
using UnityEngine.Assertions;

namespace ThorEditor
{
    public static class ReflectionUtility
    {
        /// <summary>
        /// Extracts a parent type of 'childType' which is created from a generic definition 'baseTypeGenericDefinition'.
        /// </summary>
        public static Type GetBaseTypeWithGenericDef(Type baseTypeGenericDefinition, Type childType)
        {
            while (childType != null)
            {
                if (childType.IsGenericType && childType.GetGenericTypeDefinition() == baseTypeGenericDefinition)
                {
                    return childType;
                }
                childType = childType.BaseType;
            }
            return null;
        }
        
        /// <summary>Asserts childType inherits from baseType.</summary>
        public static void AssertInheritance(Type baseType, Type childType, string methodName, string variableName)
        {
            Assert.IsTrue(baseType.IsAssignableFrom(childType),
                $"{methodName} expects {variableName} which inherits from {baseType}, but it is {childType}.");            
        }

        /// <summary>Asserts childType inherits from a baseType which is a generic type without defined parameters.</summary>
        public static void AssertGenericInheritance(Type baseTypeGenericDefinition, Type childType, string methodName, string variableName)
        {
            if (GetBaseTypeWithGenericDef(baseTypeGenericDefinition, childType) == null)
            {
                Assert.IsTrue(true,
                $"{methodName} expects {variableName} which inherits from {baseTypeGenericDefinition}, but it is {childType}.");  
            }
        }
    }
}