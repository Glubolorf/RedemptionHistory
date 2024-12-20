using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace Redemption
{
	public class TileRunner
	{
		public TileRunner(Vector2 pos, Vector2 speed, Point16 hRange, Point16 vRange, double strength, int steps, ushort type, bool addTile, bool overRide)
		{
			this.pos = pos;
			if (speed.X == 0f && speed.Y == 0f)
			{
				this.speed = new Vector2((float)WorldGen.genRand.Next((int)hRange.X, (int)(hRange.Y + 1)) * 0.1f, (float)WorldGen.genRand.Next((int)vRange.X, (int)(vRange.Y + 1)) * 0.1f);
			}
			else
			{
				this.speed = speed;
			}
			this.hRange = hRange;
			this.vRange = vRange;
			this.strength = strength;
			this.str = strength;
			this.steps = steps;
			this.stepsLeft = steps;
			this.type = type;
			this.addTile = addTile;
			this.overRide = overRide;
		}

		public void Start()
		{
			while (this.str > 0.0 && this.stepsLeft > 0)
			{
				this.str = this.strength * (double)this.stepsLeft / (double)this.steps;
				this.PreUpdate();
				int num = (int)Math.Max((double)this.pos.X - this.str * 0.5, 1.0);
				int b = (int)Math.Min((double)this.pos.X + this.str * 0.5, (double)(Main.maxTilesX - 1));
				int c = (int)Math.Max((double)this.pos.Y - this.str * 0.5, 1.0);
				int d = (int)Math.Min((double)this.pos.Y + this.str * 0.5, (double)(Main.maxTilesY - 1));
				for (int i = num; i < b; i++)
				{
					for (int j = c; j < d; j++)
					{
						Tile tile = Main.tile[i, j];
						if (this.ValidTile(tile) && (double)(Math.Abs((float)i - this.pos.X) + Math.Abs((float)j - this.pos.Y)) < this.strength * this.StrengthRange())
						{
							if (this.type == 0)
							{
								tile.active(false);
							}
							else
							{
								if (this.overRide || !tile.active())
								{
									tile.type = this.type;
								}
								if (this.addTile)
								{
									tile.active(true);
									tile.liquid = 0;
									tile.lava(false);
								}
							}
						}
					}
				}
				this.str += 50.0;
				while (this.str > 50.0)
				{
					this.pos += this.speed;
					this.stepsLeft--;
					this.str -= 50.0;
					this.speed.X = this.speed.X + (float)WorldGen.genRand.Next((int)this.hRange.X, (int)(this.hRange.Y + 1)) * 0.05f;
					this.speed.Y = this.speed.Y + (float)WorldGen.genRand.Next((int)this.vRange.X, (int)(this.vRange.Y + 1)) * 0.05f;
				}
				this.speed = Vector2.Clamp(this.speed, new Vector2(-1f, -1f), new Vector2(1f, 1f));
				this.PostUpdate();
			}
		}

		public virtual double StrengthRange()
		{
			return 0.5 + (double)WorldGen.genRand.Next(-10, 11) * 0.0075;
		}

		public virtual void PreUpdate()
		{
		}

		public virtual bool ValidTile(Tile tile)
		{
			return true;
		}

		public virtual void PostUpdate()
		{
		}

		public Vector2 pos;

		public Vector2 speed;

		public Point16 hRange;

		public Point16 vRange;

		public double strength;

		public double str;

		public int steps;

		public int stepsLeft;

		public ushort type;

		public bool addTile;

		public bool overRide;
	}
}
