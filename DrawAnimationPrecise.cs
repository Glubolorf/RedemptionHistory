using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Redemption
{
	public class DrawAnimationPrecise : DrawAnimation
	{
		public DrawAnimationPrecise(int ticksperframe, int frameCount, int w, int h, bool v = true, int offX = 0, int offY = 2)
		{
			this.Width = w;
			this.Height = h;
			this.vertical = v;
			this.offsetX = offX;
			this.offsetY = offY;
			this.Frame = 0;
			this.FrameCounter = 0;
			this.FrameCount = frameCount;
			this.TicksPerFrame = ticksperframe;
		}

		public override void Update()
		{
			int num = this.FrameCounter + 1;
			this.FrameCounter = num;
			if (num >= this.TicksPerFrame)
			{
				this.FrameCounter = 0;
				num = this.Frame + 1;
				this.Frame = num;
				if (num >= this.FrameCount)
				{
					this.Frame = 0;
				}
			}
		}

		public override Rectangle GetFrame(Texture2D texture)
		{
			return new Rectangle(this.vertical ? 0 : ((this.Width + this.offsetX) * this.Frame), this.vertical ? ((this.Height + this.offsetY) * this.Frame) : 0, this.Width, this.Height);
		}

		private int Width;

		private int Height;

		private int offsetX;

		private int offsetY = 2;

		private bool vertical = true;
	}
}
