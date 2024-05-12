using Csla8RestApi.Dal;
using System.Reflection;

namespace Csla8ModelTemplates.Dal.Firebird
{
    /// <summary>
    /// Represents a list of Firebird data access implementations.
    /// </summary>
    public static class FirebirdDalIndex
    {
        /// <summary>
        /// Gets the list of data access implementations in the currwnt assembly.
        /// </summary>
        public static Dictionary<Type, Type> Items
        {
            get
            {
                var dalindex = new DalIndex(Assembly.GetExecutingAssembly());
                return dalindex.DalTypes;
            }
        }
    }
}
