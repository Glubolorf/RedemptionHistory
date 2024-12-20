using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.DruidDamageClass.SeedBags;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.Items
{
	public class RedeItem : GlobalItem
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			string lifeTimeType = "lifetime";
			if (item.modItem is DruidStave)
			{
				lifeTimeType = "guardian time";
			}
			if (item.modItem is DruidSeedBag)
			{
				lifeTimeType = "plant lifetime";
			}
			if (this.prefixLifetimeModifier != 1f && (item.modItem is DruidSeedBag || item.modItem is DruidStave))
			{
				TooltipLine line = new TooltipLine(base.mod, "PrefixLifeTime", string.Concat(new object[]
				{
					(this.prefixLifetimeModifier > 1f) ? "+" : "",
					(int)(this.prefixLifetimeModifier * 100f) - 100,
					"% ",
					lifeTimeType
				}));
				line.isModifier = true;
				if (this.prefixLifetimeModifier < 1f)
				{
					line.isModifierBad = true;
				}
				tooltips.Add(line);
				line.text = string.Concat(new object[]
				{
					(this.prefixLifetimeModifier > 1f) ? "+" : "",
					(int)(this.prefixLifetimeModifier * 100f) - 100,
					"% ",
					lifeTimeType
				});
			}
			if (this.druidTag)
			{
				int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("ItemName"));
				if (tooltipLocation != -1 && !RedeConfigClient.Instance.NoDruidClassTag)
				{
					tooltips.Insert(tooltipLocation + 1, new TooltipLine(base.mod, "IsDruid", "[c/91dc16:---Druid Class---]"));
				}
			}
			foreach (TooltipLine line2 in tooltips)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					if (this.redeRarity == 1)
					{
						line2.overrideColor = new Color?(new Color(0, 255, 200));
					}
					else if (this.redeRarity == 2)
					{
						line2.overrideColor = new Color?(RedeColor.SoullessColour);
					}
					else if (this.redeRarity == 3)
					{
						line2.overrideColor = new Color?(RedeColor.NebColour);
					}
					else if (this.redeRarity == 4)
					{
						line2.overrideColor = new Color?(RedeColor.GirusTier);
					}
					else if (this.redeRarity == 5)
					{
						line2.overrideColor = new Color?(new Color(0, 120, 255));
					}
					else if (this.redeRarity == 6)
					{
						line2.overrideColor = new Color?(new Color(170, 0, 255));
					}
					else if (this.redeRarity == 7)
					{
						line2.overrideColor = new Color?(new Color(255, 195, 0));
					}
				}
			}
		}

		public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if (item.modItem is DruidDamageItem && item.damage != 0 && !item.accessory && !item.vanity && item.useStyle != 0)
			{
				switch (Main.rand.Next(16))
				{
				case 0:
					return (int)base.mod.PrefixType("Old");
				case 1:
					return (int)base.mod.PrefixType("Wild");
				case 2:
					return (int)base.mod.PrefixType("Blighted");
				case 3:
					return (int)base.mod.PrefixType("Dry");
				case 4:
					return (int)base.mod.PrefixType("Fruitful");
				case 5:
					return (int)base.mod.PrefixType("Lively");
				case 6:
					return (int)base.mod.PrefixType("Prickly");
				case 7:
					return (int)base.mod.PrefixType("Rotten");
				case 8:
					return (int)base.mod.PrefixType("Blooming");
				case 9:
					return (int)base.mod.PrefixType("Enchanted");
				case 10:
					return (int)base.mod.PrefixType("Mesmerizing");
				case 11:
					return (int)base.mod.PrefixType("Forgotten");
				case 12:
					return (int)base.mod.PrefixType("Blessed");
				case 13:
					return (int)base.mod.PrefixType("Exotic");
				case 14:
					return (int)base.mod.PrefixType("Mother Nature's");
				case 15:
					return (int)base.mod.PrefixType("Dryad's");
				}
			}
			return base.ChoosePrefix(item, rand);
		}

		public override bool NewPreReforge(Item item)
		{
			this.prefixLifetimeModifier = 1f;
			return base.NewPreReforge(item);
		}

		public override void NetSend(Item item, BinaryWriter writer)
		{
			writer.Write(this.prefixLifetimeModifier);
		}

		public override void NetReceive(Item item, BinaryReader reader)
		{
			this.prefixLifetimeModifier = reader.ReadSingle();
		}

		public override void OnCraft(Item item, Recipe recipe)
		{
			if (item.type == base.mod.ItemType("Loreholder"))
			{
				Main.NewText("<Loreholder> Who awakens me from my slumber?", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
			if (item.type == base.mod.ItemType("RedemptionTeller"))
			{
				Main.NewText("<Chalice of Alignment> Greetings, I am the Chalice of Alignment, and I believe any action can be redeemed.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
		}

		public float prefixLifetimeModifier = 1f;

		public int redeRarity;

		public bool druidTag;
	}
}
