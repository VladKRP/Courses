using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Tasks
{
    public static class CommonTasks
    {
        /// <summary>
        /// Returns the lists of public and obsolete classes for specified assembly.
        /// Please take attention: classes (not interfaces, not structs)
        /// </summary>
        /// <param name="assemblyName">name of assembly</param>
        /// <returns>List of public but obsolete classes</returns>
        public static IEnumerable<string> GetPublicObsoleteClasses(string assemblyName) {
            var obsoleteClasses = from entity in Assembly.Load(assemblyName).GetTypes()
                                  where entity.IsClass && entity.IsPublic && Attribute.IsDefined(entity, typeof(ObsoleteAttribute))
                                  select entity.Name;
            return obsoleteClasses;  
        }

        /// <summary>
        /// Returns the value for required property path
        /// </summary>
        /// <example>
        ///  1) 
        ///  string value = instance.GetPropertyValue("Property1")
        ///  The result should be equal to invoking statically
        ///  string value = instance.Property1;
        ///  2) 
        ///  string name = instance.GetPropertyValue("Property1.Property2.FirstName")
        ///  The result should be equal to invoking statically
        ///  string name = instance.Property1.Property2.FirstName;
        /// </example>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="obj">source object to get property from</param>
        /// <param name="propertyPath">dot-separated property path</param>
        /// <returns>property value of obj for required propertyPath</returns>
        public static T GetPropertyValue<T>(this object obj, string propertyPath) {
            Type objType = obj.GetType();
            if (!propertyPath.Contains('.'))
            {
                return (T)(objType.GetProperty(propertyPath).GetValue(obj, null));
            }
            else
            {
                string[] propertyLevel = propertyPath.Split('.');
                object propertyValue = null;
                for(int i = 0; i < propertyLevel.Length - 1; i++)
                {
                    PropertyInfo propertyInfo = objType.GetProperty(propertyLevel[i]);
                    propertyValue = propertyInfo.GetValue(obj, null);
                    objType = propertyValue.GetType();
                }
                return (T)objType.GetProperty(propertyLevel.Last()).GetValue(propertyValue, null);
            }
        }


        /// <summary>
        /// Assign the value to the required property path
        /// </summary>
        /// <example>
        ///  1)
        ///  instance.SetPropertyValue("Property1", value);
        ///  The result should be equal to invoking statically
        ///  instance.Property1 = value;
        ///  2)
        ///  instance.SetPropertyValue("Property1.Property2.FirstName", value);
        ///  The result should be equal to invoking statically
        ///  instance.Property1.Property2.FirstName = value;
        /// </example>
        /// <param name="obj">source object to set property to</param>
        /// <param name="propertyPath">dot-separated property path</param>
        /// <param name="value">assigned value</param>
        public static void SetPropertyValue(this object obj, string propertyPath, object value) {

            Type objType = obj.GetType();
            if (!propertyPath.Contains('.'))
            {
                objType.GetProperty(propertyPath).SetValue(obj, value, null);
            }
            else
            {
                string[] propertyLevel = propertyPath.Split('.');
                object propertyValue = null;
                for (int i = 0; i < propertyLevel.Length - 1; i++)
                {
                    PropertyInfo propertyInfo = objType.GetProperty(propertyLevel[i]);
                    propertyValue = propertyInfo.GetValue(obj, null);
                    objType = propertyValue.GetType();
                }
                objType.GetProperty(propertyLevel.Last()).SetValue(propertyValue, value, null);
            }  
        }

    }
}
