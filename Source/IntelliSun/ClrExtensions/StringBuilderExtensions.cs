using System;
using System.Text;

namespace IntelliSun
{
    /// <summary>
    ///     Provides extension methods for <see cref="System.Text.StringBuilder" />.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        ///     Appends the string returned by processing a composite format string followed by the default line terminator
        /// </summary>
        /// <param name="builder">A <see cref="System.Text.StringBuilder" /> instance to append to.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">An object to format.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="builder" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        ///     <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero),
        ///     or greater than or equal to 1.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        /// </exception>
        public static void AppendFormatLine(this StringBuilder builder, string format, object arg0)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (format == null)
                throw new ArgumentNullException("format");

            builder.AppendFormat(format, arg0);
            builder.AppendLine();
        }

        /// <summary>
        ///     Appends the string returned by processing a composite format string followed by the default line terminator
        /// </summary>
        /// <param name="builder">A <see cref="System.Text.StringBuilder" /> instance to append to.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="builder" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        ///     <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero),
        ///     or greater than or equal to 2.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        /// </exception>
        public static void AppendFormatLine(this StringBuilder builder, string format, object arg0, object arg1)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (format == null)
                throw new ArgumentNullException("format");

            builder.AppendFormat(format, arg0, arg1);
            builder.AppendLine();
        }

        /// <summary>
        ///     Appends the string returned by processing a composite format string followed by the default line terminator
        /// </summary>
        /// <param name="builder">A <see cref="System.Text.StringBuilder" /> instance to append to.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="builder" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        ///     <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero),
        ///     or greater than or equal to 3.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        /// </exception>
        public static void AppendFormatLine(this StringBuilder builder, string format, object arg0, object arg1,
            object arg2)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (format == null)
                throw new ArgumentNullException("format");

            builder.AppendFormat(format, arg0, arg1, arg2);
            builder.AppendLine();
        }

        /// <summary>
        ///     Appends the string returned by processing a composite format string followed by the default line terminator
        /// </summary>
        /// <param name="builder">A <see cref="System.Text.StringBuilder" /> instance to append to.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="args">An array of objects to format.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="builder" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        ///     <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero),
        ///     or greater than or equal to the length of the args array.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        /// </exception>
        public static void AppendFormatLine(this StringBuilder builder, string format, params object[] args)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (format == null)
                throw new ArgumentNullException("format");

            builder.AppendFormat(format, args);
            builder.AppendLine();
        }

        /// <summary>
        ///     Appends the string returned by processing a composite format string followed by the default line terminator
        /// </summary>
        /// <param name="builder">A <see cref="System.Text.StringBuilder" /> instance to append to.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="args">An array of objects to format.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="builder" /> or <paramref name="format" /> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        ///     <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero),
        ///     or greater than or equal to the length of the args array.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        /// </exception>
        public static void AppendFormatLine(this StringBuilder builder, IFormatProvider provider, string format,
            params object[] args)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (format == null)
                throw new ArgumentNullException("format");

            builder.AppendFormat(provider, format, args);
            builder.AppendLine();
        }
    }
}