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
using Seasar.Dao.Attrs;

namespace Seasar.Dao.Tests.Impl
{
	public class EmployeeSearchCondition
	{
        private Department department;

        private string job;

        private string dname;

        private string orderByString;
	        
        public Department Department
        {
            set { department = value; }
            get { return department; }
        }

        [Column("dname_0")]
        public string Dname
        {
            set { dname = value; }
            get { return dname; }
        }

        public string Job
        {
            set { job = value; }
            get { return job; }
        }

        public string OrderByString
        {
            set { orderByString = value; }
            get { return orderByString; }
        }	
	}
}
