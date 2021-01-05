using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;

namespace Webadmin.Models
{
    public class SessionContextInterceptor : DbCommandInterceptor
    {
        private int adminID;

        public void SetAdminId(int adminIDIn)
        {
            this.adminID = adminIDIn;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            try
            {
                // Attempt to insert the admin ID - will break if it is null and be caught, in which case this is treated as an unauthenticated request
                SqlParameter adminIdParameter = new SqlParameter("@admin_id", adminID);
                string sessionContextCommandText = "EXEC sp_set_session_context @key=N'admin_id', @value = @admin_id ;";
            command.CommandText = sessionContextCommandText + command.CommandText;
            command.Parameters.Insert(0, adminIdParameter);
            }
            catch (NullReferenceException)
            {
                // This is an unauthenticated request - the row-level security will prevent access if it should be prevented, so nothing
                // should be done here and the original query should go ahead in the case that this request does not need to be authenticated
            }

            return result;
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            try
            {
                // Attempt to insert 
            SqlParameter adminIdParameter = new SqlParameter("@admin_id", adminID);
            string sessionContextCommandText = "EXEC sp_set_session_context @key=N'admin_id', @value = @admin_id ; ";
            command.CommandText = sessionContextCommandText + command.CommandText;
            command.Parameters.Insert(0, adminIdParameter);
            }
            catch (NullReferenceException)
            {
                // This is an unauthenticated request - the row-level security will prevent access if it should be prevented, so nothing
                // should be done here and the original query should go ahead in the case that this request does not need to be authenticated
            }

            return Task.FromResult(result);
        }
    }
}
