namespace Efrpg.Generators
{
    public class ScalarValuedFunctionsTemplateData
    {
        public string ExecName                           { get; }
        public string ReturnType                         { get; }
        public string WriteStoredProcFunctionParamsFalse { get; }
        public string Name                               { get; }
        public string Schema                             { get; }

        public ScalarValuedFunctionsTemplateData(
            string execName,
            string returnType,
            string writeStoredProcFunctionParamsFalse,
            string name,
            string schema)
        {
            ExecName                           = execName;
            ReturnType                         = returnType;
            WriteStoredProcFunctionParamsFalse = writeStoredProcFunctionParamsFalse;
            Name                               = name;
            Schema                             = schema;
        }
    }
}