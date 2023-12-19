//-----------------------------------------------------------------------
// <copyright file="JsonEnumStringValueAttribute.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class JsonEnumStringValueAttribute : Attribute
    {
        public JsonEnumStringValueAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
