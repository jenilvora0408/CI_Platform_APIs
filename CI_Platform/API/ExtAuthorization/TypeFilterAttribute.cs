using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ExtAuthorization
{
    /// <summary>
    /// Represents a filter attribute that creates an instance of the specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TypeFilterAttribute : Attribute, IFilterFactory, IOrderedFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFilterAttribute"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type to be instantiated as a filter.</param>
        public TypeFilterAttribute(Type type)
        {
            ImplementationType = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the type that will be instantiated as a filter.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the filter is reusable.
        /// </summary>
        public bool IsReusable => true; 

        /// <summary>
        /// Gets or sets the order of execution for the filter.
        /// </summary>
        public int Order { get; set; } = 0; 

        /// <summary>
        /// Creates an instance of the filter using the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve the filter instance.</param>
        /// <returns>The created filter instance.</returns>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // Create an instance of the filter using the implementation type
            IFilterMetadata filter = (IFilterMetadata)serviceProvider.GetService(ImplementationType)!;
            if (filter == null)
            {
                throw new InvalidOperationException($"Could not resolve filter of type '{ImplementationType}' from the service provider.");
            }

            return filter;
        }
    }
}
