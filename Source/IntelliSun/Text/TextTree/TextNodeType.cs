using System;

namespace IntelliSun.Text
{
    public struct TextNodeType
    {
        private readonly int id;
        private readonly string name;

        public TextNodeType(string name)
            : this(-1, name)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public TextNodeType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}