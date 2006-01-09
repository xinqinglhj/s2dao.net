using System;
using Seasar.Dao.Attrs;

namespace Seasar.Dao.Tests.Impl
{
	/// <summary>
	/// IdentityTableAutoDao の概要の説明です。
	/// </summary>
    [Bean(typeof(IdentityTable))]
    public interface IdentityTableAutoDao
	{
        void Insert(IdentityTable identityTable);
	}
}
