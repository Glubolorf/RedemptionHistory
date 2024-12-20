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
			if (item.type == base.mod.ItemType("RedemptionTeller"))
			{
				Main.NewText("<Loreholder> Greetings, I am the Chalice of Alignment, and I believe any action can be redeemed.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
		}
	}
}
