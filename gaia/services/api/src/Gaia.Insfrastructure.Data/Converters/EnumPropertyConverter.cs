// <copyright file="EnumPropertyConverter.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Gaia.Insfrastructure.Data.Converters
{
    /// <summary>Class EnumPropertyConverter.</summary>
    /// <typeparam name="T">The enumeration Type.</typeparam>
    /// <seealso cref="IPropertyConverter" />
    /// <remarks>None.</remarks>
    public class EnumPropertyConverter<T> : IPropertyConverter
        where T : struct, IConvertible
    {
        /// <summary>Convert DynamoDBEntry to the specified object.</summary>
        /// <param name="entry">DynamoDBEntry to be serialized.</param>
        /// <returns>Serialized object.</returns>
        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (entry is Primitive primitive)
            {
                return GetEnum(primitive);
            }

            if (entry is PrimitiveList primitiveList)
            {
               return primitiveList.Entries.Select(GetEnum).ToList();
            }

            throw new NotSupportedException("The requested conversion is not supported");
        }

        /// <summary>Convert object to DynamoDBEntry.</summary>
        /// <param name="value">Object to be deserialized.</param>
        /// <returns>Object deserialized as DynamoDBEntry.</returns>
        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.GetType() == typeof(List<T>))
            {
               return GetPrimitiveList(value);
            }

            return GetPrimitive(value);
        }

        private static T GetEnum(Primitive primitive)
        {
            var enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);
            var candidate = primitive.AsString()
                             .Replace("_", string.Empty);

            var name = names.Single(x => string.Compare(x, candidate, StringComparison.OrdinalIgnoreCase) == 0);

            return (T)Enum.Parse(enumType, name);
        }

        private static Primitive GetPrimitive(object value)
        {
            if (!(value is Enum))
            {
                throw new NotSupportedException("The requested conversion is not supported");
            }

            var name = Enum.GetName(typeof(T), value);

            return new Primitive(name);
        }

        private static PrimitiveList GetPrimitiveList(object value)
        {
            var list = (List<T>)value;
            var primitiveList = new PrimitiveList(DynamoDBEntryType.String);
            foreach (var item in list)
            {
                primitiveList.Add(GetPrimitive(item));
            }

            return primitiveList;
        }
    }
}