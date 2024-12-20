using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption.Effects
{
	public class DefaultShader : ITrailShader
	{
		public string ShaderPass
		{
			get
			{
				return "DefaultPass";
			}
		}

		public void ApplyShader(Effect effect, Trail trail, List<Vector2> positions)
		{
			effect.CurrentTechnique.Passes[this.ShaderPass].Apply();
		}
	}
}
