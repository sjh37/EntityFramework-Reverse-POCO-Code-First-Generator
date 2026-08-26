using System.Collections.Generic;

namespace Efrpg.Readers
{
    public class RawStoredProcedure
    {
        public readonly string Schema;
        public readonly string Name;
        public readonly bool IsTableValuedFunction;
        public readonly bool IsScalarValuedFunction;
        public readonly bool IsStoredProcedure;
        public readonly StoredProcedureParameter Parameter;
        public bool ReturnModelsRead { get; set; }
        public string ReturnModelError { get; set; }
        public List<List<RawStoredProcedureReturnColumn>> ReturnModels { get; set; }

        public RawStoredProcedure(
            string schema,
            string name,
            bool isTableValuedFunction,
            bool isScalarValuedFunction,
            bool isStoredProcedure,
            StoredProcedureParameter parameter)
        {
            Schema = schema;
            Name = name;
            IsTableValuedFunction = isTableValuedFunction;
            IsScalarValuedFunction = isScalarValuedFunction;
            IsStoredProcedure = isStoredProcedure;
            Parameter = parameter;
            ReturnModelsRead = false;
            ReturnModelError = string.Empty;
            ReturnModels = new List<List<RawStoredProcedureReturnColumn>>();
        }
    }
}
