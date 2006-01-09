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
using Seasar.Dao.Dbms;
using Seasar.Dao.Impl;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Util;
using NUnit.Framework;

namespace Seasar.Dao.Tests.Impl
{
    /// <summary>
	/// SelectDynamicCommandTest の概要の説明です。
	/// </summary>
    [TestFixture]
    public class SelectDynamicCommandTest
	{
        private const string PATH = "Tests.dicon";

        [Test]
        public void TestExecuteTx()
        {

            IS2Container container = S2ContainerFactory.Create(PATH);
            IDataSource dataSource = (IDataSource) container.GetComponent(typeof(IDataSource));

            IDbConnection cn = DataSourceUtil.GetConnection(dataSource);
            IDbms dbms = new MSSQLServer(dataSource,cn);

            SelectDynamicCommand cmd = new SelectDynamicCommand(dataSource,
                BasicCommandFactory.INSTANCE,
                new BeanMetaDataDataReaderHandler(new BeanMetaDataImpl(
						typeof(Employee), dbms.DatabaseMetaData, dbms)),
						BasicDataReaderFactory.INSTANCE);

            cmd.Sql = "SELECT * FROM emp WHERE empno = /*empno*/1234";
            
            Assert.Ignore("BasicHandler.BindArgsのargsに入っていかない");

            Employee emp = (Employee) cmd.Execute(new Object[] { 7788 });
            Assert.IsNotNull(emp, "1");


        }
    }
}
