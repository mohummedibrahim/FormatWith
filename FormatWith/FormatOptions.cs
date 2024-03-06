namespace FormatWith
{
    /// <summary>
    /// Format options.
    /// </summary>
    public class FormatOptions
    {
        /// <summary>
        /// The behaviour to use when the format string contains a parameter that is not present in the lookup object or dictionary.
        /// </summary>
        public MissingKeyBehaviour MissingKeyBehaviour { get; set; }
            = MissingKeyBehaviour.ThrowException;

        /// <summary>
        /// When the <see cref="MissingKeyBehaviour.ReplaceWithFallback"/> is specified, this string is used as a fallback replacement value when the parameter is present in the lookup object or dictionary.
        /// </summary>
        public object FallbackReplacementValue { get; set; } = null;

        /// <summary>
        /// The character used to begin parameters.
        /// </summary>
        public char OpenBraceChar { get; set; } = '{';

        /// <summary>
        /// The character used to end parameters.
        /// </summary>
        public char CloseBraceChar { get; set; } = '}';

        /// <summary>
        /// The default FormatOptions instance.
        /// </summary>
        public static FormatOptions Default { get; } 
            = new FormatOptions();

    }
}