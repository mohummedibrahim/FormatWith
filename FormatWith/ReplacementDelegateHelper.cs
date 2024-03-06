using FormatWith.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace FormatWith
{
    /// <summary>
    /// Provides some helper methods for creating delegate/replacement handler. <see cref="Func{String, String, ReplacementResult}"/>
    /// </summary>
    public static class ReplacementDelegateHelper
    {
        /// <summary>
        /// Creates handler from IDictinary.
        /// </summary>
        /// <param name="replacements">An <see cref="IDictionary"/> with keys and values to inject into the string.</param>
        public static Func<string, string, ReplacementResult> ToHandler(
            this IDictionary<string, object> replacements)
        {
            return (key, format) => new ReplacementResult(replacements.TryGetValue(key, out object value), value);
        }

        /// <summary>
        /// Creates handler from IDictinary.
        /// </summary>
        /// <param name="replacements">An <see cref="IDictionary"/> with keys and values to inject into the string.</param>
        public static Func<string, string, ReplacementResult> ToHandler(
            this IDictionary<string, string> replacements)
        {
            return (key, format) => new ReplacementResult(replacements.TryGetValue(key, out string value), value);
        }

        /// <summary>
        /// Creates handler from POCO object.
        /// </summary>
        /// <param name="replacementObject">The object whose properties should be injected in the string.</param>
        public static Func<string, string, ReplacementResult> ToHandler(
            object replacementObject)
        {
            return (key, format) => FormatWithMethods.FromReplacementObject(key, replacementObject);
        }

        /// <summary>
        /// Creates handler from IDictinary.
        /// </summary>
        /// <param name="dataRow">A <see cref="DataRow"/> with column name as parameter names and cells value as parameter replacement value.</param>
        public static Func<string, string, ReplacementResult> ToHandler(
            this DataRow dataRow)
        {
            return (key, format) =>
            {
                var success = dataRow.Table.Columns.Contains(key);
                var value = success ? dataRow[key] : null;
                return new ReplacementResult(success, value);
            };
        }
    }
}