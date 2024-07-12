﻿namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

public class RiversideDashboardModel
{
    public string BridgeName { get; set; } = default!;
    public DashboardPointDataModel PaApproachSurfaceTemperature { get; set; } = default!;
    public DashboardPointDataModel AmbientAirTemperature { get; set; } = default!;
    public DashboardPointDataModel WindSpeed { get; set; } = default!;
    public DashboardPointDataModel WindDirection { get; set; } = default!;
    public DashboardWindowDataModel WindSpeedHistory { get; set; } = default!;
    public DashboardWindowDataModel AirTempHistory { get; set; } = default!;
}