﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Csla8RestApi.Tests.Dal {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class JunctionText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal JunctionText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Csla8RestApi.Tests.Dal.JunctionText", typeof(JunctionText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user has been changed in the meantime..
        /// </summary>
        public static string User_Concurrency {
            get {
                return ResourceManager.GetString("User_Concurrency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting the user has failed..
        /// </summary>
        public static string User_DeleteFailed {
            get {
                return ResourceManager.GetString("User_DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating the new user has failed..
        /// </summary>
        public static string User_InsertFailed {
            get {
                return ResourceManager.GetString("User_InsertFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The requested user has not been found..
        /// </summary>
        public static string User_NotFound {
            get {
                return ResourceManager.GetString("User_NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Updating the user has failed..
        /// </summary>
        public static string User_UpdateFailed {
            get {
                return ResourceManager.GetString("User_UpdateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with code {0} already exists..
        /// </summary>
        public static string User_UserCodeExists {
            get {
                return ResourceManager.GetString("User_UserCodeExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting the role with name {0} has failed..
        /// </summary>
        public static string UserRole_DeleteFailed {
            get {
                return ResourceManager.GetString("UserRole_DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role with name {0} already exists..
        /// </summary>
        public static string UserRole_Exists {
            get {
                return ResourceManager.GetString("UserRole_Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating the new role with name {0} has failed..
        /// </summary>
        public static string UserRole_InsertFailed {
            get {
                return ResourceManager.GetString("UserRole_InsertFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The requested role with name {0} has not been found..
        /// </summary>
        public static string UserRole_NotFound {
            get {
                return ResourceManager.GetString("UserRole_NotFound", resourceCulture);
            }
        }
    }
}
