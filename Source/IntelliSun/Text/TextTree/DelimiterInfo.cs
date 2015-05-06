using System;

namespace IntelliSun.Text
{
    [Obsolete]
    public struct DelimiterInfo
    {
        private readonly string delimiter;
        private readonly string parameters;
        private readonly CollectDirection direction;

        public DelimiterInfo(string delimiter)
            : this(delimiter, null, default(CollectDirection))
        {

        }

        public DelimiterInfo(string delimiter, string parameters)
            : this(delimiter, parameters, default(CollectDirection))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DelimiterInfo(string delimiter, string parameters, CollectDirection direction)
        {
            this.delimiter = delimiter;
            this.parameters = parameters;
            this.direction = direction;
        }

        public string Delimiter
        {
            get { return this.delimiter; }
        }

        public string Parameters
        {
            get { return this.parameters; }
        }

        public CollectDirection Direction
        {
            get { return this.direction; }
        }
    }
}