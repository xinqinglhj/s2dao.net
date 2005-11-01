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
using System.Data;
using Seasar.Extension.ADO;
using Seasar.Framework.Log;
using Seasar.Framework.Util;

namespace Seasar.Extension.ADO.Impl 
{
    public class BasicUpdateHandler : BasicHandler, IUpdateHandler
    {
        private static Logger logger = Logger.GetLogger(typeof(BasicUpdateHandler));

        public BasicUpdateHandler()
        {
        }

        public BasicUpdateHandler(IDataSource dataSource, string sql)
            : base(dataSource, sql)
        {
        }

        public BasicUpdateHandler(IDataSource dataSource, string sql,
            ICommandFactory commandFactory)
            : base(dataSource, sql, commandFactory)
        {
        }

        #region IUpdateHandler �����o

        public int Execute(object[] args)
        {
            return Execute(args, Type.GetTypeArray(args), new string[args.Length]);
        }

        public int Execute(object[] args, Type[] argTypes, string[] argNames)
        {
            if(logger.IsDebugEnabled)
                logger.Debug(GetCompleteSql(args));
            IDbConnection connection = Connection;
            try
            {
                return Execute(connection, args, argTypes, argNames);
            }
            finally
            {
                DataSourceUtil.CloseConnection(this.DataSource, connection);
            }
        }

        protected int Execute(IDbConnection connection, object[] args, Type[] argTypes,
            string[] argNames)
        {
            IDbCommand cmd = DataSource.GetCommand(Sql, connection);
            try
            {
                BindArgs(cmd, args, argTypes, argNames);
                return CommandUtil.ExecuteNonQuery(this.DataSource, cmd);
            }
            finally
            {
                CommandUtil.Close(cmd);
            }
        }

        #endregion
    }
}
