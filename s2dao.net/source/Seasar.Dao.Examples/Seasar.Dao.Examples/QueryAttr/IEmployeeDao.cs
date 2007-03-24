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

using System.Collections;
using Seasar.Dao.Attrs;

namespace Seasar.Dao.Examples.QueryAttr
{
    [Bean(typeof(Employee))]
    public interface IEmployeeDao
    {
        /// <summary>
        /// 従業員名の昇順に並び替えた全ての従業員を取得します。
        /// </summary>
        /// <returns>従業員のリスト</returns>
        [Query("order by ename asc")]
        IList GetAllListEnameAsc();

        /// <summary>
        /// 従業員名から従業員を取得します。
        /// </summary>
        /// <param name="ename">従業員名</param>
        /// <returns>従業員</returns>
        [Query("ename=/*ename*/")]
        Employee GetEmployeeByEname(string ename);
    }
}
