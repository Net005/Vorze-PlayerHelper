﻿namespace Cyclone2.windows
{
    using System;

    internal enum WmDeviceChange : uint
    {
        DBT_CONFIGCHANGECANCELED = 0x19,
        DBT_CONFIGCHANGED = 0x18,
        DBT_CUSTOMEVENT = 0x8006,
        DBT_DEVICEARRIVAL = 0x8000,
        DBT_DEVICEQUERYREMOVE = 0x8001,
        DBT_DEVICEQUERYREMOVEFAILED = 0x8002,
        DBT_DEVICEREMOVECOMPLETE = 0x8004,
        DBT_DEVICEREMOVEPENDING = 0x8003,
        DBT_DEVICETYPESPECIFIC = 0x8005,
        DBT_DEVNODES_CHANGED = 7,
        DBT_QUERYCHANGECONFIG = 0x17,
        DBT_USERDEFINED = 0xffff
    }
}

