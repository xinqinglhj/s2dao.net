using System;
using Seasar.Dao.Impl;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using NUnit.Framework;

namespace Seasar.Dao.Tests.Impl
{
	/// <summary>
	/// UpdateDynamicCommandTest の概要の説明です。
	/// </summary>
    [TestFixture]
    public class UpdateDynamicCommandTest
	{

        private const string PATH = "Tests.dicon";

        [Test]
        public void TestExecuteTx()
        {

            IS2Container container = S2ContainerFactory.Create(PATH);
            IDataSource dataSource = (IDataSource) container.GetComponent(typeof(IDataSource));

            UpdateDynamicCommand cmd = new UpdateDynamicCommand(dataSource,
                BasicCommandFactory.INSTANCE);
            //cmd.setSql("UPDATE emp SET ename = /*employee.ename*/'HOGE' WHERE empno = /*employee.empno*/1234");
            cmd.Sql = "UPDATE emp SET ename = /*Employee.Ename*/'HOGE' WHERE empno = /*Employee.Empno*/1234";
            cmd.ArgNames = new String[] { "Employee" };

            Employee emp = new Employee();
            emp.Empno=7788;
            emp.Ename="SCOTT";
            Assert.Ignore("BasicUpdateHandler:line56のGetCompleteSqlで返されるSQLはwhere empno = 'SCOTT'になってしまうし、line98に入るとValueTypesの問題が発生する");
            int count = (int) cmd.Execute(new Object[] { emp });
            Assert.AreEqual(1, count, "1");
        }
    }
}
