using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using PKEMeter.Items;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		private static PKETextMessage DefaultTextDisplay( out Func<string> redTooltipGetter ) {
			Color color = Color.Red * ( 0.5f + ( Main.rand.NextFloat() * 0.5f ) );
			string text = "";

			NPC bossNpc = Main.npc.FirstOrDefault(
				n => n?.active == true
				&& ( n.boss || n.type == NPCID.EaterofWorldsHead )
			);

			float priority = 0f;

			switch( bossNpc?.type ?? -1 ) {
			case -1:
				break;
			case NPCID.EyeofCthulhu:
				text = "WARNING - CLASS V FREEROAM CORPOREAL FLOATER";
				priority = 0.5f;
				break;
			case NPCID.KingSlime:
				text = "WARNING - CLASS V COMPOSITE ANIMATE SEMISOLID";
				priority = 0.5f;
				break;
			case NPCID.EaterofWorldsHead:
				text = "WARNING - CLASS VI LINEAR SEGMENTED NECROTIC ORGANIC";
				priority = 0.6f;
				break;
			case NPCID.BrainofCthulhu:
				text = "WARNING - CLASS VI TRANSDIM SWARMHOST FLOATER";
				priority = 0.6f;
				break;
			case NPCID.SkeletronHead:
				text = "WARNING - CLASS VI REACTIVE FREEROAM CORPOREAL POSSESSOR";
				priority = 0.6f;
				break;
			case NPCID.QueenBee:
				text = "WARNING - CLASS VI FREEROAM SWARMHOST ORGANIC";
				priority = 0.6f;
				break;
			case NPCID.WallofFlesh:
				text = "WARNING - CLASS VII VOLUMINOUS KINETIC ORGANIC AMALGAMATE";
				priority = 0.7f;
				break;
			case NPCID.TheDestroyer:
				text = "WARNING - CLASS VII LINEAR SEGMENTED CONSTRUCT";
				priority = 0.7f;
				break;
			case NPCID.Eyezor:
			case NPCID.Retinazer:
				text = "WARNING - CLASS VII PAIRED FREEROAM FLOATER CONSTRUCTS";
				priority = 0.7f;
				break;
			case NPCID.SkeletronPrime:
				text = "WARNING - CLASS VII FREEROAM MULTIFACET CONSTRUCT";
				priority = 0.7f;
				break;
			//case NPCID.QueenSlime:
			//	text = "WARNING - CLASS VII FREEROAM COMPOSITE ANIMATE SEMISOLID";
			//	priority = 0.7f;
			//	break;
			case NPCID.Plantera:
				text = "WARNING - CLASS VIII REACTIVE FLORAL CRAWLER";
				priority = 0.8f;
				break;
			case NPCID.Golem:
				text = "WARNING - CLASS VIII POWERED MULTIFACET CONSTRUCT";
				priority = 0.8f;
				break;
			case NPCID.DukeFishron:
				text = "WARNING - CLASS IX FREEROAM AQUATIC TRANSDIM ORGANIC";
				priority = 0.9f;
				break;
			case NPCID.CultistBoss:
				text = "WARNING - CLASS VIII FREEROAM TRANSDIM ELEVATED MORTAL";
				priority = 0.8f;
				break;
			//case NPCID.EmpressOfLight:
			//	text = "WARNING - CLASS IX FREEROAM LUMINOUS DEITY";
			//	priority = 0.9f;
			//	break;
			case NPCID.MoonLordCore:
				text = "WARNING - CLASS X FULLTORSO ULTRADIM DEITY REMNANT";
				priority = 1f;
				break;
			default:
				text = "WARNING - UNKNOWN CLASS V+ PKE-EMITTING ENTITY";
				priority = 0.501f;
				break;
			}

			redTooltipGetter = () => "DOMINANT ENTITIES";
			return new PKETextMessage( text, color, priority );
		}



		////////////////

		private void InitializeDefaultText() {
			if( this.TextSources == null ) {
				this.TextSources[ "Default" ] = (_, __, ___) => PKEMeterLogic.DefaultTextDisplay(
					out PKEMeterItem.RedLabelGetter
				);
			}
		}

		private void PostInitializeDefaultText() {
			Timers.SetTimer( 3, true, () => {
				this.TextScrollPos += 2;
				return true;
			} );
		}


		////////////////

		public (string text, Color color, int offset) GetText( Player player, Vector2 position ) {
			IDictionary<string, PKETextMessage> msgs = this.TextSources.ToDictionary(
				kv => kv.Key,
				kv => kv.Value.Invoke( player, position, this.GaugeSnapshot )
			);

			//

			KeyValuePair<string, PKETextMessage> priorityMsg;
			if( msgs.Count() > 0 ) {
				priorityMsg = msgs.Aggregate(
					( kvL, kvR ) => kvL.Value.Priority > kvR.Value.Priority ? kvL : kvR
				);
			} else {
				priorityMsg = PKETextMessage.EmptyMessage;
			}

			PKETextMessage existingMsg;
			if( !msgs.TryGetValue(this.CurrentMessageId, out existingMsg) ) {
				existingMsg = PKETextMessage.EmptyMessage.Value;
			}

			PKETextMessage newMessage = existingMsg;

			//

			if( priorityMsg.Key != this.CurrentMessageId ) {
				if( priorityMsg.Value.Priority > existingMsg.Priority || this.CurrentTextTickDuration <= 0 ) {
					this.CurrentTextTickDuration = 60 * 3;
					this.CurrentMessageId = priorityMsg.Key;
					newMessage = priorityMsg.Value;
				}
			}

			//

			this.CurrentTextTickDuration--;

			int textWid = priorityMsg.Value.Message.Length * 8;
			if( this.TextScrollPos > textWid ) {
				this.TextScrollPos = -6;
			}

			//

			return (newMessage.Message, newMessage.Color, this.TextScrollPos );
		}
	}
}
