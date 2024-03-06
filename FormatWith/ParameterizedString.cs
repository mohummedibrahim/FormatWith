using FormatWith.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormatWith
{
    /// <summary>
    /// A parameterized string that parses the parameters and stores them locally for making subsequent formats on that string faster.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Create(string, FormatOptions)"/> method to create the instance, then use <see cref="Format(Func{string, string, ReplacementResult})"/> to format/resolve the parameters of the string. <br /><br />
    /// For handler (<see cref="Func{T1, T2, TResult}"/>) creation, one of the helper methods from <see cref="ReplacementDelegateHelper"/> class can be used.<br /><br />
    /// To retreive the parameters use <see cref="GetParameters"/>.
    /// </remarks>
    public class ParameterizedString
    {
        private readonly FormatToken[] _tokens;
        private readonly FormatOptions _options;
        private readonly int _stringLength;

        private ParameterizedString(FormatToken[] tokens, FormatOptions formatOptions, int stringLength)
        {
            _tokens = tokens;
            _options = formatOptions;
            _stringLength = stringLength;
        }

        /// <summary>
        /// Returns all the parameters found in the original string given in the constructor.
        /// </summary>
        public IEnumerable<string> GetParameters()
        {
            return from t in _tokens
                   where t.TokenType == TokenType.Parameter
                   select t.Value;
        }


        /// <summary>
        /// Formats a string with the values given by the properties on an input object.
        /// </summary>
        /// <param name="handler">The function used to perform the replacements on the format tokens</param>
        /// <returns>The formatted string.</returns>
        public string Format(
            Func<string, string, ReplacementResult> handler)
        {
            return FormatHelpers.ProcessTokens(_tokens, handler, _options.MissingKeyBehaviour, _options.FallbackReplacementValue, _stringLength * 2);
        }

        /// <summary>
        /// Parses the parameters from given string, constructs the instance only if the string contains one or more parameters.
        /// </summary>
        /// <param name="stringWithParameters"></param>
        /// <param name="formatOptions"></param>
        /// <returns>Either a valid instance if <paramref name="stringWithParameters"/> contains at least one parameter, or null if <paramref name="stringWithParameters"/> contains no parameter.</returns>
        public static ParameterizedString Create(string stringWithParameters, FormatOptions formatOptions = null)
        {
            if (string.IsNullOrEmpty(stringWithParameters))
                return null;

            if (formatOptions == null)
                formatOptions = FormatOptions.Default;

            var tokens = FormatHelpers.Tokenize(stringWithParameters, formatOptions.OpenBraceChar, formatOptions.CloseBraceChar).ToArray();

            if (!tokens.Any(t => t.TokenType == TokenType.Parameter))
                return null;

            return new ParameterizedString(tokens, formatOptions, stringWithParameters.Length);
        }
    }
}