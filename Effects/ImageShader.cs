using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Redemption.Effects
{
	public class ImageShader : ITrailShader
	{
		public string ShaderPass
		{
			get
			{
				return "BasicImagePass";
			}
		}

		public ImageShader(Texture2D image, Vector2 coordinateMultiplier, float strength = 1f, float yAnimSpeed = 0f)
		{
			this._coordMult = coordinateMultiplier;
			this._strength = strength;
			this._yAnimSpeed = yAnimSpeed;
			this._texture = image;
		}

		public ImageShader(Texture2D image, float xCoordinateMultiplier, float yCoordinateMultiplier, float strength = 1f, float yAnimSpeed = 0f) : this(image, new Vector2(xCoordinateMultiplier, yCoordinateMultiplier), strength, yAnimSpeed)
		{
		}

		public void ApplyShader(Effect effect, Trail trail, List<Vector2> positions)
		{
			this._xOffset -= this._coordMult.X;
			effect.Parameters["imageTexture"].SetValue(this._texture);
			effect.Parameters["coordOffset"].SetValue(new Vector2(this._xOffset, Main.GlobalTime * this._yAnimSpeed));
			effect.Parameters["coordMultiplier"].SetValue(this._coordMult);
			effect.Parameters["strength"].SetValue(this._strength);
			effect.CurrentTechnique.Passes[this.ShaderPass].Apply();
		}

		protected Vector2 _coordMult;

		protected float _xOffset;

		protected float _yAnimSpeed;

		protected float _strength;

		private readonly Texture2D _texture;
	}
}
