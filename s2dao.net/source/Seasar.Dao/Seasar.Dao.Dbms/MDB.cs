using System;

namespace Seasar.Dao.Dbms
{
	/// <summary>
	/// MDB の概要の説明です。
	/// </summary>
	public class MDB : Standard
	{
		public override string Suffix
		{
			get { return "_mdb"; }
		}

		public override string GetSequenceNextValString(String sequenceName)
		{
			return "select gen_id( " + sequenceName + ", 1 ) from RDB$DATABASE";
		}

		public override KindOfDbms Dbms
		{
			get { return KindOfDbms.MDB; }
		}
	}
}