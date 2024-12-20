using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class CopyWand : ModItem
	{
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Structure Wand");
			base.Tooltip.SetDefault("Select 2 points in the world, then right click to save a structure");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.rare = 1;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2 && !this.SecondPoint)
			{
				Point16 topLeft = this.TopLeft;
				Saver.SaveToFile(new Rectangle((int)this.TopLeft.X, (int)this.TopLeft.Y, this.Width, this.Height), null);
			}
			else if (!this.SecondPoint)
			{
				this.TopLeft = Utils.ToPoint16(Main.MouseWorld / 16f);
				this.Width = 0;
				this.Height = 0;
				Main.NewText("Select Second Point", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
				this.SecondPoint = true;
			}
			else
			{
				Point16 bottomRight = Utils.ToPoint16(Main.MouseWorld / 16f);
				this.Width = (int)(bottomRight.X - this.TopLeft.X - 1);
				this.Height = (int)(bottomRight.Y - this.TopLeft.Y - 1);
				Main.NewText("Ready to save! Right click to save this structure...", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
				this.SecondPoint = false;
			}
			return true;
		}

		public bool SecondPoint;

		public Point16 TopLeft;

		public int Width;

		public int Height;
	}
}
