#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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

namespace Seasar.Dao.Examples.AutoDelete
{
    public interface IAutoDeleteLogic
    {
        void TestAutoDelete();
    }

    public class AutoDeleteLogicImpl : IAutoDeleteLogic
    {
        private readonly IEmployeeDao _employeeDao;

        public AutoDeleteLogicImpl(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        #region IAutoDeleteLogic o

        public void TestAutoDelete()
        {
            // ]ΖυΤ7499Μ]ΖυπmF
            int empno = 7499;
            Employee emp1 = _employeeDao.GetEmployeeByEmpno(empno);
            Console.WriteLine("]ΖυΤ[" + empno + "]Μ]ΖυF" + emp1.ToString());

            // ]ΖυΤ7499Μ]Ζυπν
            int ret = _employeeDao.DeleteEmployee(emp1);
            Console.WriteLine("DeleteEmployee\bhΜίθl:" + ret);

            // ]ΖυΤ7499Μ]ΖυπmF
            Employee emp2 = _employeeDao.GetEmployeeByEmpno(empno);
            if (emp2 == null)
            {
                Console.WriteLine("]ΖυΤ[" + empno + "]Μ]ΖυΝΆέ΅άΉρB");
            }

            throw new ForCleanupException();
        }

        #endregion
    }
}
