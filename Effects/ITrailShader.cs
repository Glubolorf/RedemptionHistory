using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption.Effects
{
	public interface ITrailShader
	{
		string ShaderPass { get; }

		void ApplyShader(Effect effect, Trail trail, List<Vector2> positions);
	}
}
