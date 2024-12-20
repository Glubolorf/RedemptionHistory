using System;

namespace Redemption
{
	public class SubworldCacheClass
	{
		public SubworldCacheClass(string mod, string modWorld, string field, bool? mybool, int? myint)
		{
			this.mod = mod;
			this.modwld = modWorld;
			this.field = field;
			this.mybool = mybool;
			this.myint = myint;
		}

		public string field;

		public bool? mybool;

		public int? myint;

		public string modwld;

		public string mod;
	}
}
