using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper
{
	internal class MultiWand : ModItem
	{
		public Rectangle target
		{
			get
			{
				return new Rectangle((int)this.TopLeft.X, (int)this.TopLeft.Y, this.Width, this.Height);
			}
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Multistructure Wand");
			base.Tooltip.SetDefault("Select 2 points in the world, then right click to add a structure. Right click in your inventory when done to save.");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.rare = 1;
		}

		public override void RightClick(Player player)
		{
			base.item.stack++;
			if (this.StructureCache.Count > 1)
			{
				Saver.SaveMultistructureToFile(ref this.StructureCache, null);
				return;
			}
			Main.NewText("Not enough structures! If you want to save a single structure, use the normal structure wand instead!", Color.Red, false);
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2 && !this.SecondPoint)
			{
				Point16 topLeft = this.TopLeft;
				this.StructureCache.Add(Saver.SaveStructure(this.target));
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
				Main.NewText("Ready to add! Right click to add this structure, Right click in inventory to save all structures", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
				this.SecondPoint = false;
			}
			return true;
		}

		public bool SecondPoint;

		public Point16 TopLeft;

		public int Width;

		public int Height;

		internal List<TagCompound> StructureCache = new List<TagCompound>();
	}
}
