using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Efrpg.Filtering;

namespace Efrpg.Util
{
    /// <summary>
    /// Purpose of this class is to serve the plugin implementation. When reading MultiContextSettings
    /// from an external DLL the returned object cannot be directly casted because type differs.
    /// It has to be copied to the local implementation of the MultiContextSettings class
    /// </summary>
    public static class MultiContextSettingsCopy
    {
        /// <summary>
        /// Copies properties with the same Name and Type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        private static void CopyPropertiesFrom(object source, object dest)
        {
            var fromProperties = source.GetType().GetProperties();
            var toProperties   = dest  .GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                var toProperty = toProperties.FirstOrDefault(x => fromProperty.Name == x.Name);
                if (toProperty != null)
                {
                    if (toProperty.Name == fromProperty.Name && toProperty.PropertyType.FullName == fromProperty.PropertyType.FullName)
                        toProperty.SetValue(dest, fromProperty.GetValue(source));
                }
            }
        }

        public static void Copy(object source, MultiContextSettings dest)
        {
            CopyPropertiesFrom(source, dest);

            var fromProperties = source.GetType().GetProperties();

            // Tables
            var fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextSettings.Tables));
            if (fromProperty != null)
            {
                var listValue = (IList) fromProperty.GetValue(source);
                if (listValue != null)
                {
                    dest.Tables = new List<MultiContextTableSettings>();

                    foreach (var item in listValue)
                    {
                        var tableSettings = new MultiContextTableSettings();
                        CopyMultiContextTableSettings(item, tableSettings);
                        dest.Tables.Add(tableSettings);
                    }
                }
            }

            // StoredProcedures
            fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextSettings.StoredProcedures));
            if (fromProperty != null)
            {
                var listValue = (IList) fromProperty.GetValue(source);
                if (listValue != null)
                {
                    dest.StoredProcedures = new List<MultiContextStoredProcedureSettings>();

                    foreach (var item in listValue)
                    {
                        var spSettings = new MultiContextStoredProcedureSettings();
                        CopyPropertiesFrom(item, spSettings);
                        dest.StoredProcedures.Add(spSettings);
                    }
                }
            }

            // Functions
            fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextSettings.Functions));
            if (fromProperty != null)
            {
                var listValue = (IList) fromProperty.GetValue(source);
                if (listValue != null)
                {
                    dest.Functions = new List<MultiContextFunctionSettings>();

                    foreach (var item in listValue)
                    {
                        var functionSettings = new MultiContextFunctionSettings();
                        CopyPropertiesFrom(item, functionSettings);
                        dest.Functions.Add(functionSettings);
                    }
                }
            }

            // Enumerations
            fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextSettings.Enumerations));
            if (fromProperty != null)
            {
                var listValue = (IList) fromProperty.GetValue(source);
                if (listValue != null)
                {
                    dest.Enumerations = new List<EnumerationSettings>();

                    foreach (var item in listValue)
                    {
                        var enumSettings = new EnumerationSettings();
                        CopyPropertiesFrom(item, enumSettings);
                        dest.Enumerations.Add(enumSettings);
                    }
                }
            }


            // ForeignKeys
            fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextSettings.ForeignKeys));
            if (fromProperty != null)
            {
                var listValue = (IList) fromProperty.GetValue(source);
                if (listValue != null)
                {
                    dest.ForeignKeys = new List<MultiContextForeignKeySettings>();

                    foreach (var item in listValue)
                    {
                        var fkSettings = new MultiContextForeignKeySettings();
                        CopyPropertiesFrom(item, fkSettings);
                        dest.ForeignKeys.Add(fkSettings);
                    }
                }
            }
        }

        private static void CopyMultiContextTableSettings(object source, MultiContextTableSettings dest)
        {
            CopyPropertiesFrom(source, dest);

            var fromProperties = source.GetType().GetProperties();

            // Columns
            var fromProperty = fromProperties.FirstOrDefault(x => x.Name == nameof(MultiContextTableSettings.Columns));
            if (fromProperty == null)
                return;

            var listValue = (IList) fromProperty.GetValue(source);
            if (listValue == null)
                return;

            dest.Columns = new List<MultiContextColumnSettings>();

            foreach (var item in listValue)
            {
                var columnsSettings = new MultiContextColumnSettings();
                CopyPropertiesFrom(item, columnsSettings);
                dest.Columns.Add(columnsSettings);
            }
        }
    }
}
