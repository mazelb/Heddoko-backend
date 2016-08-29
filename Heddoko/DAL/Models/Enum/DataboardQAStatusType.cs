using System;

namespace DAL.Models
{
    [Flags]
    public enum DataboardQAStatusType : long
    {
        None = 0,
        BootloaderProgrammed = 1,
        SDCardAssigned = 2,
        BrainMCUProgrammed = 4,
        Quintic1Programmed = 8,
        Quintic2Programmed = 16,
        Quintic3Programmed = 32,
        SerialNumberProgrammed = 64,
        BluetoothConnectionTested = 128,
        PowerButtonVerified = 256,
        ResetButtonVerified = 512,
        RecordButtonVerified = 1024,
        StreamingFrameTest = 2048,
        FullChargeCycleComplete = 4096,
        TestedAndReady = BootloaderProgrammed | SDCardAssigned | BrainMCUProgrammed | Quintic1Programmed | Quintic2Programmed 
                        | Quintic3Programmed | SerialNumberProgrammed | BluetoothConnectionTested | PowerButtonVerified 
                        | ResetButtonVerified | RecordButtonVerified | StreamingFrameTest | FullChargeCycleComplete
    }
}
