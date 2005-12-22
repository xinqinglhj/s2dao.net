#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.Data.OleDb;
using System.Data;
using Seasar.Extension.ADO;

namespace Seasar.Extension.ADO.Types
{
    public abstract class BaseValueType
    {
        private IDataSource dataSource;

        public BaseValueType(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public void BindValue(System.Data.IDbCommand cmd, string columnName, object value, DbType dbType)
        {
            if("MySqlCommand".Equals(cmd.GetType().Name))
            {
                columnName = "?" + columnName;
            }
            else
            {
                columnName = "@" + columnName;
            }

            IDataParameter parameter = dataSource.GetParameter(columnName, value == null ? DBNull.Value : value);
			if("OleDbCommand".Equals(cmd.GetType().Name) && dbType == DbType.String)
			{
				OleDbParameter oleDbParam = parameter as OleDbParameter;
				oleDbParam.OleDbType = OleDbType.VarChar;
			}
			else
			{
				parameter.DbType = dbType;
			}
            cmd.Parameters.Add(parameter);
        }
    }
}
