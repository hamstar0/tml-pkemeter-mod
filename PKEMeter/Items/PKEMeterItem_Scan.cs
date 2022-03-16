using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static int MaxScanTicks { get; private set; } = 45;



		////////////////

		public static bool CanScanAt( int screenX, int screenY, out bool foundInInventory ) {
			if( PKEScannable.Scannables == null ) {
				foundInInventory = false;
				return false;
			}

			//

			foundInInventory = false;
			bool canScan = false;

			foreach( PKEScannable data in PKEScannable.Scannables.Values ) {
				if( data.CanScan(screenX, screenY, out bool myFoundInInventory) ) {
					canScan = true;
					foundInInventory = myFoundInInventory;

					break;
				}
			}

			return canScan;
		}


		////////////////

		public static void RunScanAt( int screenX, int screenY ) {
			if( PKEScannable.Scannables.Count == 0 ) {
				return;
			}

			//

			var mymod = PKEMeterMod.Instance;
			IList<string> completedScans;

			if( !PKEMeterItem.StepScannableScannables(screenX, screenY, out completedScans) ) {
				return;
			}
			
			//
			
			foreach( string scanName in completedScans ) {
				PKEScannable.CompleteScan( scanName );
			}

			//

			if( mymod.PKEScanLoop.State != SoundState.Playing ) {
				mymod.PKEScanLoop.Play();
			}

			Timers.SetTimer( "PKEScanSoundLoopCutoff", 2, false, () => {
				mymod.PKEScanLoop.Stop();
				return false;
			} );

			//

			if( completedScans.Count > 0 ) {
				if( mymod.PKEScanDone.State != SoundState.Playing ) {
					mymod.PKEScanDone.Play();
				}
			}
		}


		////////////////

		public static bool StepScannableScannables( int screenX, int screenY, out IList<string> completedScans ) {
			bool hasScannables = false;

			completedScans = new List<string>();

			//

			foreach( (string name, PKEScannable scannable) in PKEScannable.Scannables ) {
				if( !scannable.CanScan(screenX, screenY, out _) ) {
					continue;
				}

				//

				hasScannables = true;

				//

				float addPerc = 1f / (float)PKEMeterItem.MaxScanTicks;

				if( (scannable.ScanPercent + addPerc) >= 1f ) {
					scannable.ScanPercent = 1f;

					completedScans.Add( name );
				} else {
					scannable.ScanPercent += addPerc;
				}
			}

			return hasScannables;
		}
	}
}