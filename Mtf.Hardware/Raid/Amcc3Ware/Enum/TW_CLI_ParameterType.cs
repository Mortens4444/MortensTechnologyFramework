using System.ComponentModel;

namespace Mtf.Hardware.Raid.Amcc3Ware.Enum
{
	public enum TW_CLI_ParameterType : byte
	{
	    [Description("/c{0} add rebuild={1}:{2}:{3}")]
		AddNewRebuildTask,
	    [Description("/c{0} add selftest={1}:{2}")]
		AddNewSelfTestTask,
	    [Description("/c{0} add verify={1}:{2}:{3}")]
		AddNewVerifyTask,
	    [Description("show rebuild")]
		AllRebuildSchedulesForThe9000Controllers,
	    [Description("show selftest")]
		AllSelfTestSchedulesForThe9000Controllers,
	    [Description("show verify")]
		AllVerifySchedulesForThe9000Controllers,
	    [Description("/c{0}/bbu {1}")]
		BBUEnableOrDisable,
	    [Description("/c{0}/bbu show")]
		BBUShow,
	    [Description("/c{0}/bbu show all")]
		BBUShowAll,
	    [Description("/c{0}/bbu show cap")]
		BBUShowBatteryCapacityInHours,
	    [Description("/c{0}/bbu show batinst")]
		BBUShowBatteryInstallationDate,
	    [Description("/c{0}/bbu show bootloader")]
		BBUShowBootLoader,
	    [Description("/c{0}/bbu show fw")]
		BBUShowFirmware,
	    [Description("/c{0}/bbu show lasttest")]
		BBUShowLastTest,
	    [Description("/c{0}/bbu show pcb")]
		BBUShowPCB,
	    [Description("/c{0}/bbu show serial")]
		BBUShowSerial,
	    [Description("/c{0}/bbu show status")]
		BBUShowStatus,
	    [Description("/c{0}/bbu show temp")]
		BBUShowTemp,
	    [Description("/c{0}/bbu show volt")]
		BBUShowVolt,
	    [Description("/c{0}/bbu test")]
		BBUTest,
	    [Description("/c{0}/bbu test quiet")]
		BBUTestQuiet,
	    [Description("/c{0} commit")]
		CleanUpForShutdown,
	    [Description("show ver")]
		CLI_and_API_version,
	    [Description("/c{0} show diag")]
		ControllerDiagnostics,
	    [Description("/c{0} show rebuild")]
		CurrentRebuildBackgroundTaskSchedule,
	    [Description("/c{0} show selftest")]
		CurrentSelfTestBackgroundTaskSchedule,
	    [Description("/c{0} show verify")]
		CurrentVerifyBackgroundTaskSchedule,
	    [Description("/c{0} del rebuild={1}")]
		DeleteRebuild,
	    [Description("/c{0} del selftest={1}")]
		DeleteSelfTest,
	    [Description("/c{0} del verify={1}")]
		DeleteVerify,
	    /// <summary>
	    /// show diag > diag.txt redirect is recommanded, because of not human readable characters
	    /// </summary>
	    [Description("show diag")]
		DiagnosticInformationOfAllControllers,
	    [Description("/c{0} set rebuild=disable")]
		DisableRebuildSchedulesOnController,
	    [Description("/c{0} set selftest=disable")]
		DisableSelfTestSchedulesOnController,
	    [Description("/c{0} set selftest=disable task=SMART")]
		DisableSMARTSelfTestSchedulesOnController,
	    [Description("/c{0} set selftest=disable task=UDMA")]
		DisableUDMASelfTestSchedulesOnController,
	    [Description("/c{0} set verify=disable")]
		DisableVerifySchedulesOnController,
	    [Description("/c{0} set rebuild=enable")]
		EnableRebuildSchedulesOnController,
	    [Description("/c{0} set selftest=enable")]
		EnableSelfTestSchedulesOnController,
	    [Description("/c{0} set selftest=enable task=SMART")]
		EnableSMARTSelfTestSchedulesOnController,
	    [Description("/c{0} set selftest=enable task=UDMA")]
		EnableUDMASelfTestSchedulesOnController,
	    [Description("/c{0} set verify=enable")]
		EnableVerifySchedulesOnController,
	    [Description("show")]
		GeneralSummaryOfAllDetectedControllers,
	    [Description("info")]
		Info,
	    [Description("info c{0}")]
		InfoContoller,
	    [Description("info c{0} u{1}")]
		InfoContollerUnit,
	    [Description("/c{0} set exportjbod=on")]
		JBODExportPolicyOn,
	    [Description("/c{0} set exportjbod=off")]
		JBODExportPolicyOff,
	    [Description("/c{0} {1} mediascan")]
		MediaScan,
	    [Description("/c{0}/p{1} export")]
		PortExport,
	    [Description("/c{0}/p{1} export noscan")]
		PortExportNoScan,
	    [Description("/c{0}/p{1} export noscan quiet")]
		PortExportNoScanQuiet,
	    [Description("/c{0}/p{1} export quiet")]
		PortExportQuiet,
	    [Description("/c{0}/p{1} show identify")]
		PortIdentify,
	    [Description("/c{0}/p{1} show lspeed")]
		PortLSpeed,
	    [Description("/c{0}/p{1} show")]
		PortShow,
	    [Description("/c{0}/p{1} show all")]
		PortShowAll,
	    [Description("/c{0}/p{1} show capacity")]
		PortShowCapacity,
	    [Description("/c{0}/p{1} show firmware")]
		PortShowFirmware,
	    [Description("/c{0}/p{1} show model")]
		PortShowModel,
	    [Description("/c{0}/p{1} show serial")]
		PortShowSerial,
	    [Description("/c{0}/p{1} show smart")]
		PortShowSMART,
	    [Description("/c{0}/p{1} show status")]
		PortShowStatus,
	    [Description("/c{0} rescan noscan")]
		RescanAllPortsAndReconstituteAllUnitsDoNotInformOS,
	    [Description("/c{0} rescan")]
		RescanAllPortsAndReconstituteAllUnits,
	    [Description("/c{0} set autocarve={1}")]
		SetAutoCarvePolicy,
	    [Description("/c{0} set ondegrade={1}")]
		SetControllerBasedCachePolicy,
	    [Description("/c{0} set rebuild={1}")]
		SetRebuildPrioritySchedulesOnController,
	    [Description("/c{0} set spinup={1}")]
		SetControllerBasedDiskSpinUpPolicy,
	    [Description("/c{0} set stagger={1}")]
		SetControllerBasedDiskSpinUpStaggerTimePolicy,
	    [Description("/c{0} set verify={1}")]
		SetVerifyPrioritySchedulesOnController,
	    [Description("show AENs")]
		ShowAENMessagesOfAllControllers, // same as ShowAlarmMessagesOfAllControllers
	    [Description("show AENs reverse")]
		ShowAENMessagesOfAllControllersRecentMessagesFirst, // same as ShowAlarmMessagesOfAllControllersRecentMessagesFirst
	    [Description("show alarms")]
		ShowAlarmMessagesOfAllControllers, // same as ShowAENMessagesOfAllControllers
	    [Description("show alarms reverse")]
		ShowAlarmMessagesOfAllControllersRecentMessagesFirst, // same as ShowAENMessagesOfAllControllersRecentMessagesFirst
	    [Description("/c{0} show AENs")]
		ShowAENMessagesOfSpecifiedController, // same as ShowAlarmMessagesOfSpecifiedController
	    [Description("/c{0} show AENs reverse")]
		ShowAENMessagesOfSpecifiedControllerRecentMessagesFirst, // same as ShowAlarmMessagesOfSpecifiedControllerRecentMessagesFirst
	    [Description("/c{0} show alarms")]
		ShowAlarmMessagesOfSpecifiedController, // same as ShowAENMessagesOfSpecifiedController
	    [Description("/c{0} show alarms reverse")]
		ShowAlarmMessagesOfSpecifiedControllerRecentMessagesFirst, // same as ShowAENMessagesOfSpecifiedControllerRecentMessagesFirst
	    [Description("/c{0} show all")]
		ShowAllAttributes,
	    [Description("/c{0} show driver model firmware memory bios monitor serial pcb pchip achip numports numunits numdrives exportjbod ondegrade spinup autocarve stagger")]
		ShowAttributes,
	    /// <summary>
	    /// Not Implemented
	    /// </summary>
	    [Description("/c{0} show allunitstatus unitstatus drivestatus")]
		ShowAttributes_2,
	    [Description("show events")]
	    ShowEvents,
	    [Description("show events reverse")]
	    ShowEventsReverse,
	    [Description("info c{0} unitstatus")]
	    UnitStatus,
	    [Description("info c{0} allunitstatus")]
	    AllUnitStatus,
	    [Description("focus c{0}")]
	    FocusController,
	    [Description("/c{0} show carvesize")]
	    ShowCarveSize,
	    [Description("/c{0} show ctlbus")]
	    ShowControllerBus,
	    [Description("/c{0} show autorebuild")]
	    ShowAutoRebuild,
	    [Description("/c{0} show dpmstat type=inst")]
	    ShowDpmStatInst,
	    [Description("/c{0} show dpmstat type=ra")]
	    ShowDpmStatRa,
	    [Description("/c{0} show dpmstat type=ext")]
	    ShowDpmStatExt,
	    [Description("/c{0}/u{1} show identify")]
	    ShowIdentify,
	    [Description("/c{0}/u{1} start rebuild disk=p<p:-p...>[ignoreECC]")]
	    ShowRebuildDisk,
	    [Description("/c{0}/u{1} migrate type=RaidType [disk=p<p:-p] [exclude=p:-p] [group=3|4|5|6] [strip=Stripe] [noscan] [nocache] [autoverify]")]
	    MigrateUnit,
	    [Description("/c{0}/p{1} set identify=on")]
	    SetPortIdentityOn,
	    [Description("/c{0}/p{1} set identify=off")]
	    SetPortIdentityOff,
	    [Description("/c{0}/e{1}/slot{2} show")]
	    ShowSlot,
	    [Description("/c{0}/e{1}/slot{2} show identify")]
	    ShowSlotIdentify,
	    [Description("/c{0}/e{1}/slot{2} set identify=on")]
	    SetSlotIdentifyOn,
	    [Description("/c{0}/e{1}/slot{2} set identify=off")]
	    SetSlotIdentifyOff,
	    [Description("/c{0}/e{1}/fan{2} show")]
	    ShowFan,
	    [Description("/c{0}/e{1}/fan{2} show identify")]
	    ShowFanIdentify,
	    [Description("/c{0}/e{1}/fan{2} set identify=on")]
	    SetFanIdentifyOn,
	    [Description("/c{0}/e{1}/fan{2} set identify=off")]
	    SetFanIdentifyOff,
	    [Description("/c{0}/e{1}/temp{2} show")]
	    ShowTemp,
	    [Description("/c{0}/u{1} show status")]
		ShowUnitStatus,
	    [Description("/c{0} show")]
		SummaryInformationAboutTheSpecifiedController,
	    [Description("/c{0}/u{1} show")]
		SummaryInformationAboutTheSpecifiedUnit,
	    [Description("/c{0}/u{1} show all")]
		UnitAll,
	    [Description("/c{0}/u{1} del")]
		UnitDelete,
	    [Description("/c{0}/u{1} del noscan")]
		UnitDeleteNoScan,
	    [Description("/c{0}/u{1} del noscan quiet")]
		UnitDeleteNoScanQuiet,
	    [Description("/c{0}/u{1} del quiet")]
		UnitDeleteQuiet,
	    [Description("/c{0}/u{1} export")]
		UnitExport,
	    [Description("/c{0}/u{1} export noscan")]
		UnitExportNoScan,
	    [Description("/c{0}/u{1} export noscan quiet")]
		UnitExportNoScanQuiet,
	    [Description("/c{0}/u{1} export quiet")]
		UnitExportQuiet,
	    [Description("/c{0}/u{1} flush")]
		UnitFlush,
	    [Description("/c{0}/u{1} show initializestatus")]
		UnitInitializeStatus,
	    [Description("/c{0}/u{1} show name")]
		UnitName,
	    [Description("/c{0}/u{1} pause rebuild")]
		UnitPauseRebuild,
	    [Description("/c{0}/u{1} show rebuildstatus")]
		UnitRebuildStatus,
	    [Description("/c{0}/u{1} resume rebuild")]
		UnitResumeRebuild,
	    [Description("/c{0}/u{1} show serial")]
		UnitSerial,
	    [Description("/c{0}/u{1} set autorecovery {2}")]
		UnitSetAutoRecovery,
	    [Description("/c{0}/u{1} set cache={2}")]
		UnitSetCache,
	    [Description("/c{0}/u{1} set cache={2} quiet")]
		UnitSetCacheQuiet,
	    [Description("/c{0}/u{1} set ignoreECC={2}")]
		UnitSetIgnoreECC,
	    [Description("/c{0}/u{1} set name={2}")]
		UnitSetName,
	    [Description("/c{0}/u{1} start verify")]
		UnitStartVerify,
	    [Description("/c{0}/u{1} stop verify")]
		UnitStopVerify,
	    [Description("/c{0}/u{1} show verifystatus")]
		UnitVerifyStatus,
	    [Description("/c{0}/u{1} show volumes")]
		UnitVolumes,
	    [Description("/c{0} flush")]
		WriteAllCachedDataToDisk
	}
}
