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
using System.Text;
using System.Collections;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Framework.Util;

namespace Seasar.Dao.Dbms
{
	/// <summary>
	/// Oracle
	/// </summary>
	public class Oracle : Standard
	{
        public override string Suffix
        {
            get
            {
                return "_oracle";
            }
        }

        public override string GetSequenceNextValString(string sequenceName)
        {
            return "select " + sequenceName + ".nextval from dual";
        }

        public override KindOfDbms Dbms
        {
            get
            {
                return KindOfDbms.Oracle;
            }
        }


        protected override string CreateAutoSelectFromClause(IBeanMetaData beanMetaData)
        {
            StringBuilder buf = new StringBuilder(100);
            buf.Append("FROM ");
            string myTableName = beanMetaData.TableName;
            buf.Append(myTableName);
            StringBuilder whereBuf = new StringBuilder(100);
            for(int i = 0; i < beanMetaData.RelationPropertyTypeSize; ++i)
            {
                IRelationPropertyType rpt = beanMetaData.GetRelationPropertyType(i);
                IBeanMetaData bmd = rpt.BeanMetaData;
                buf.Append(", ");
                buf.Append(bmd.TableName);
                buf.Append(" ");
                string yourAliasName = rpt.PropertyName;
                buf.Append(yourAliasName);
                for(int j = 0; j < rpt.KeySize; ++j)
                {
                    whereBuf.Append(myTableName);
                    whereBuf.Append(".");
                    whereBuf.Append(yourAliasName);
                    whereBuf.Append(".");
                    whereBuf.Append(rpt.GetYourKey(j));
                    whereBuf.Append("(+)");
                    whereBuf.Append(" AND ");
                }
            }
            if(whereBuf.Length > 0)
            {
                whereBuf.Length = whereBuf.Length - 5;
                buf.Append(" WHERE ");
                buf.Append(whereBuf);
            }
            return buf.ToString();
        }


		public Oracle(IDataSource dataSource, IDbConnection cn)
		{
            IList tableSet = GetTableSet(dataSource, cn);

            IDictionary primaryKeys = new Hashtable(CaseInsensitiveHashCodeProvider.Default,
                CaseInsensitiveComparer.Default);
            IDictionary columns = new Hashtable(CaseInsensitiveHashCodeProvider.Default,
                CaseInsensitiveComparer.Default);
            IEnumerator tables = tableSet.GetEnumerator();

            while(tables.MoveNext())
            {
                string tableName = tables.Current as String;
                primaryKeys[tableName] = GetPrimaryKeySet(
                    dataSource, cn, tableName);
                columns[tableName] = GetColumnSet(dataSource, cn, tableName);
            }
            DatabaseMetaDataImpl dbMetaData = new DatabaseMetaDataImpl();
            dbMetaData.TableSet = tableSet;
            dbMetaData.PrimaryKeys = primaryKeys;
            dbMetaData.Columns = columns;
            this.dbMetadata = dbMetaData;
		}

        protected IList GetTableSet(IDataSource dataSource, IDbConnection cn)
        {
            IList list = new CaseInsentiveSet();
            string sql = @"select TNAME from TAB";
            using(IDbCommand cmd = dataSource.GetCommand(sql, cn))
            {
                DataSourceUtil.SetTransaction(dataSource, cmd);
                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(reader["TNAME"]);
                    }
                }
            }
			
            return list;
        }

        protected IList GetPrimaryKeySet(IDataSource dataSource, IDbConnection cn, string tableName)
        {

            IList list = new CaseInsentiveSet();
            string sql = @"select column_name from user_constraints A, user_cons_columns B
                    where A.constraint_name=B.constraint_name and A.constraint_type='P'
                    and A.table_name=:table_name";
            using(IDbCommand cmd = dataSource.GetCommand(sql, cn))
            {
                cmd.Parameters.Add(dataSource.GetParameter(":table_name", tableName));
                DataSourceUtil.SetTransaction(dataSource, cmd);
                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(reader["column_name"]);
                    }
                }
            }
            return list;
        }

        protected IList GetColumnSet(IDataSource dataSource, IDbConnection cn, string tableName)
        {
            IList list = new CaseInsentiveSet();
            string sql = @"select COLNO,CNAME from COL
                where TNAME=:TNAME order by COLNO asc";
            using(IDbCommand cmd = dataSource.GetCommand(sql, cn))
            {
                cmd.Parameters.Add(dataSource.GetParameter(":TNAME", tableName));
                DataSourceUtil.SetTransaction(dataSource, cmd);
                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(reader["CNAME"]);
                    }
                }
            }
            return list;
        }
	}
}
