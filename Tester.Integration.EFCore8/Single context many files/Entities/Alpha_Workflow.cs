// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // workflow
    public class Alpha_Workflow
    {
        public int Id { get; set; } // Id (Primary key)
        public string Description { get; set; } // Description (length: 10)

        // Reverse navigation

        /// <summary>
        /// Child Beta_ToAlphas where [ToAlpha].[AlphaId] point to this entity (BetaToAlpha_AlphaWorkflow)
        /// </summary>
        public virtual ICollection<Beta_ToAlpha> Beta_ToAlphas { get; set; } // ToAlpha.BetaToAlpha_AlphaWorkflow

        public Alpha_Workflow()
        {
            Beta_ToAlphas = new List<Beta_ToAlpha>();
        }
    }

}
// </auto-generated>
