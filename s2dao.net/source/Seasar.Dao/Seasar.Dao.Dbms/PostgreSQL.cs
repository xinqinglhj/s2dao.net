using System;

namespace Seasar.Dao.Dbms
{
	/// <summary>
	/// Postgres の概要の説明です。
	/// </summary>
	public class PostgreSQL : Standard
	{
		public override string Suffix
		{
			get { return "_postgre"; }
		}

		public override string GetSequenceNextValString(String sequenceName)
		{
			return "select nextval('" + sequenceName + "')";
		}

		public override KindOfDbms Dbms
		{
			get { return KindOfDbms.PostgreSQL; }
		}
	}
}