using System.Collections.Generic;

namespace Efrpg.Generators
{
    public class StoredProcTemplateData
    {
        public bool HasReturnModels { get; } // sp.ReturnModels.Count > 0
        public bool SingleReturnModel { get; } // sp.ReturnModels.Count == 1
        public bool MultipleReturnModels { get; } // sp.ReturnModels.Count > 1
        public string ReturnType { get; } // sp.WriteStoredProcReturnType()
        public string ReturnModelName { get; } // sp.WriteStoredProcReturnModelName()
        public string FunctionName { get; } // sp.WriteStoredProcFunctionName()
        public string WriteStoredProcFunctionParamsFalseFalse { get; } // WriteStoredProcFunctionParams(false, false)
        public string WriteStoredProcFunctionParamsFalseTrue { get; } // WriteStoredProcFunctionParams(false, true)
        public string WriteStoredProcFunctionParamsTrueFalse { get; } // WriteStoredProcFunctionParams(true, false)
        public string WriteStoredProcFunctionParamsTrueTrue { get; } // WriteStoredProcFunctionParams(true, true)
        public bool AsyncFunctionCannotBeCreated { get; } // sp.StoredProcHasOutParams() || sp.ReturnModels.Count == 0
        public string WriteStoredProcFunctionOverloadCall { get; } // sp.WriteStoredProcFunctionOverloadCall()
        public string WriteStoredProcFunctionSetSqlParametersFalse { get; } // WriteStoredProcFunctionSetSqlParameters(false)
        public string WriteStoredProcFunctionSetSqlParametersTrue { get; } // WriteStoredProcFunctionSetSqlParameters(true)
        public string Exec { get; }
        public string AsyncExec { get; }
        public string WriteStoredProcReturnModelName { get; } // sp.WriteStoredProcReturnModelName()
        public string WriteStoredProcFunctionSqlParameterAnonymousArrayTrue { get; } // sp.WriteStoredProcFunctionSqlParameterAnonymousArray(true)
        public string WriteStoredProcFunctionSqlParameterAnonymousArrayFalse { get; } // sp.WriteStoredProcFunctionSqlParameterAnonymousArray(false)
        public string WriteStoredProcFunctionDeclareSqlParameterTrue { get; } // sp.WriteStoredProcFunctionDeclareSqlParameter(true)
        public string WriteStoredProcFunctionDeclareSqlParameterFalse { get; } // sp.WriteStoredProcFunctionDeclareSqlParameter(false)
        public List<string> Parameters { get; }
        public int ReturnModelsCount { get; }
        public string ExecWithNoReturnModel { get; }
        public List<ResultSetResultReaderCommand> ReturnModelResultSetReaderCommand { get; }
        public bool CreateDbSetForReturnModel { get; set; }

        public StoredProcTemplateData(
            bool hasReturnModels,
            bool singleReturnModel,
            bool multipleReturnModels,
            string returnType,
            string returnModelName,
            string functionName,
            string writeStoredProcFunctionParamsFalseFalse,
            string writeStoredProcFunctionParamsFalseTrue,
            string writeStoredProcFunctionParamsTrueFalse,
            string writeStoredProcFunctionParamsTrueTrue,
            bool asyncFunctionCannotBeCreated,
            string writeStoredProcFunctionOverloadCall,
            string writeStoredProcFunctionSetSqlParametersFalse,
            string writeStoredProcFunctionSetSqlParametersTrue,
            string exec,
            string asyncExec,
            string writeStoredProcReturnModelName,
            string writeStoredProcFunctionSqlParameterAnonymousArrayTrue,
            string writeStoredProcFunctionSqlParameterAnonymousArrayFalse,
            string writeStoredProcFunctionDeclareSqlParameterTrue,
            string writeStoredProcFunctionDeclareSqlParameterFalse,
            List<string> parameters,
            int returnModelsCount,
            string execWithNoReturnModel)
        {
            HasReturnModels                                        = hasReturnModels;
            SingleReturnModel                                      = singleReturnModel;
            MultipleReturnModels                                   = multipleReturnModels;
            ReturnType                                             = returnType;
            ReturnModelName                                        = returnModelName;
            FunctionName                                           = functionName;
            WriteStoredProcFunctionParamsFalseFalse                = writeStoredProcFunctionParamsFalseFalse;
            WriteStoredProcFunctionParamsFalseTrue                 = writeStoredProcFunctionParamsFalseTrue;
            WriteStoredProcFunctionParamsTrueFalse                 = writeStoredProcFunctionParamsTrueFalse;
            WriteStoredProcFunctionParamsTrueTrue                  = writeStoredProcFunctionParamsTrueTrue;
            AsyncFunctionCannotBeCreated                           = asyncFunctionCannotBeCreated;
            WriteStoredProcFunctionOverloadCall                    = writeStoredProcFunctionOverloadCall;
            WriteStoredProcFunctionSetSqlParametersFalse           = writeStoredProcFunctionSetSqlParametersFalse;
            WriteStoredProcFunctionSetSqlParametersTrue            = writeStoredProcFunctionSetSqlParametersTrue;
            Exec                                                   = exec;
            AsyncExec                                              = asyncExec;
            WriteStoredProcReturnModelName                         = writeStoredProcReturnModelName;
            WriteStoredProcFunctionSqlParameterAnonymousArrayTrue  = writeStoredProcFunctionSqlParameterAnonymousArrayTrue;
            WriteStoredProcFunctionSqlParameterAnonymousArrayFalse = writeStoredProcFunctionSqlParameterAnonymousArrayFalse;
            WriteStoredProcFunctionDeclareSqlParameterTrue         = writeStoredProcFunctionDeclareSqlParameterTrue;
            WriteStoredProcFunctionDeclareSqlParameterFalse        = writeStoredProcFunctionDeclareSqlParameterFalse;
            Parameters                                             = parameters;
            ReturnModelsCount                                      = returnModelsCount;
            ExecWithNoReturnModel                                  = execWithNoReturnModel;

            CreateDbSetForReturnModel = true;

            ReturnModelResultSetReaderCommand = new List<ResultSetResultReaderCommand>(returnModelsCount);
            for (var n = 1; n <= returnModelsCount; ++n)
            {
                var lastRecord = n == returnModelsCount;
                ReturnModelResultSetReaderCommand.Add(new ResultSetResultReaderCommand(n, lastRecord ? "Close" : "NextResult", !lastRecord, writeStoredProcReturnModelName));
            }
        }
    }
}