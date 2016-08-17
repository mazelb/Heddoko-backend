namespace DAL.Models
{
    public enum BrainpackQAStatusType
    {
        None = 0,
        Buttons1mmOfRecess = 1,
        ButtonsReturnToInitialDepth = 2,
        PowerButtonWorking = 4,
        ResetButtonWorking = 8,
        RecordButtonWorking = 16,
        ChargingIndicatorLed = 32,
        StateIndicatorLed = 64,
        LedColors = 128,
        SettingsFileUpdated = 256,
        PairingWithIMUs = 512,
        SDCardIsElectronicallyLabeledWithBatteryPackId = 1028,
        AppPairsBrainpack = 2048,
        AppLocatesBrainpack = 4096,
        AppConnectsToBrainpack = 8192,
        AppDisconnectsFromBrainpack = 16384,
        StateChangeIdleToRecording = 32768,
        StateChangeRecordingToIdle = 65536,
        StateChangeRecordingToReset = 131072,
        StateChangeIdleToReset = 262144,
        StateChangeRecordingToError = 524288,
        RecoveryFromShutdown = 1048576,
        SetRecordingName = 2097152,
        RecordingFilenameSaved = 4194304,
        FullChargingCycle = 8388608,
    }
}
