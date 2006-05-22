using System;

namespace Seasar.Dao.Tests.Interceptors
{
	/// <summary>
	/// Hoge の概要の説明です。
	/// </summary>
	public class Hoge
	{
        private string _val;
        private Hoge _parent;

		public Hoge()
		{
		}

        public string Val
        {
            set { _val = value; }
            get { return _val; }
        }

        public Hoge Parent
        {
            set { _parent = value; }
            get { return _parent; }
        }
	}
}
