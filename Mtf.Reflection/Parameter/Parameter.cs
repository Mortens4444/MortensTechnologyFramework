namespace Mtf.Reflection.Parameter
{
    public class Parameter
    {
        /// <summary>
        /// Name of the parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value of the parameter.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Type of the parameter.
        /// </summary>
        public ParameterType Type { get; }

        /// <summary>
        /// Creates an output parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        public Parameter(string name) : this(name, null, ParameterType.Out)
        { }

        /// <summary>
        /// Creates an input parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public Parameter(string name, object value) : this(name, value, ParameterType.In)
        { }

        /// <summary>
        /// Creates a parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <param name="type">Type of the parameter.</param>
        public Parameter(string name, object value, ParameterType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}