using System;

namespace Redemption
{
	public interface ILoadable
	{
		float Priority { get; }

		bool LoadOnDedServer { get; }

		void Load();

		void Unload();
	}
}
