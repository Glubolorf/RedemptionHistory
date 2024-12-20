using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RedeItem : GlobalItem
	{
		public override void OnCraft(Item item, Recipe recipe)
		{
			if (item.type == base.mod.ItemType("Loreholder"))
			{
				Main.NewText("<Loreholder> Who awakens me from my slumber?", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
		}
	}
}
