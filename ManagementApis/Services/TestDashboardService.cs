using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class TestDashboardService : IDashboardService
{
    public BurlingtonDashboardModel GetForBurlington()
    {
        var retVal = new BurlingtonDashboardModel
        {
            BridgeName = "Burlington",
            PaApproachSurfaceTemperature = new()
            {
                TableName = "TP08A_TP08A_W",
                ColumnName = "TP08AST",
                TimeOfPoint = DateTime.Now,
                PointValue = 61.84f,
                DataName = "PaApproachSurfaceTemperature",
                BridgeName = "Burlington"
            },
            AmbientAirTemperature = new()
            {
                TableName = "TP08A_TP08A_D",
                ColumnName = "TP08APT_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 234.4f,
                DataName = "AmbientAirTemperature",
                BridgeName = "Burlington"
            },
            NjApproachSurfaceTemperature = new()
            {
                TableName = "TP18A_TP18A_D",
                ColumnName = "TP18APT_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 68.33f,
                DataName = "NjApproachSurfaceTemperature",
                BridgeName = "Burlington"
            },
            WindSpeed = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 5.602f,
                DataName = "WindSpeed",
                BridgeName = "Burlington"
            },
            WindDirection = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Direction_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 277.8f,
                DataName = "WindDirection",
                BridgeName = "Burlington"
            },
            AirGap = new()
            {
                TableName = "TP08A_TP08A_AG",
                ColumnName = "Distance",
                TimeOfPoint = DateTime.Now,
                PointValue = 63.08f,
                DataName = "AirGap",
                BridgeName = "Burlington"
            },
            WindSpeedHistory = new()
            {
                TableName = "BB05A_BB05A_W",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
            AirGapHistory = new()
            {
                TableName = "BBMHB_BBMHBAG_Longterm",
                ColumnName = "Distance_raw",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
            AirTempHistory = new()
            {
                TableName = "BB04A_BB04A_D",
                ColumnName = "BB04APT_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
        };

        var startDate = DateTime.Now.AddHours(-48);
        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 0, 0);
        var random = new Random();
        for (var i = 0; i < 20; i++)
        {
            retVal.WindSpeedHistory.AddPoint(random.Next(200, 250), startDate.AddHours(i * 2));
            retVal.AirGapHistory.AddPoint(random.Next(60, 70), startDate.AddHours(i * 2));
            retVal.AirTempHistory.AddPoint(random.Next(50, 60), startDate.AddHours(i * 2));
        }

        return retVal;
    }

    public TaconyDashboardModel GetTaconyData()
    {
        var retVal = new TaconyDashboardModel
        {
            BridgeName = "Tacony",
            PaApproachSurfaceTemperature = new()
            {
                TableName = "TP08A_TP08A_W",
                ColumnName = "TP08AST",
                TimeOfPoint = DateTime.Now,
                PointValue = 61.84f,
                DataName = "PaApproachSurfaceTemperature",
                BridgeName = "Tacony"
            },
            AmbientAirTemperature = new()
            {
                TableName = "TP08A_TP08A_D",
                ColumnName = "TP08APT_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 234.4f,
                DataName = "AmbientAirTemperature",
                BridgeName = "Tacony"
            },
            NjApproachSurfaceTemperature = new()
            {
                TableName = "TP18A_TP18A_D",
                ColumnName = "TP18APT_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 68.33f,
                DataName = "NjApproachSurfaceTemperature",
                BridgeName = "Tacony"
            },
            WindSpeed = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 5.602f,
                DataName = "WindSpeed",
                BridgeName = "Tacony"
            },
            WindDirection = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Direction_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 277.8f,
                DataName = "WindDirection",
                BridgeName = "Tacony"
            },
            AirGap = new()
            {
                TableName = "TP08A_TP08A_AG",
                ColumnName = "Distance",
                TimeOfPoint = DateTime.Now,
                PointValue = 63.08f,
                DataName = "AirGap",
                BridgeName = "Tacony"
            },
            WindSpeedHistory = new()
            {
                TableName = "BB05A_BB05A_W",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
            AirGapHistory = new()
            {
                TableName = "BBMHB_BBMHBAG_Longterm",
                ColumnName = "Distance_raw",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
            AirTempHistory = new()
            {
                TableName = "BB04A_BB04A_D",
                ColumnName = "BB04APT_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Burlington",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
        };

        var startDate = DateTime.Now.AddHours(-48);
        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 0, 0);
        var random = new Random();
        for (var i = 0; i < 20; i++)
        {
            retVal.WindSpeedHistory.AddPoint(random.Next(200, 250), startDate.AddHours(i * 2));
            retVal.AirGapHistory.AddPoint(random.Next(60, 70), startDate.AddHours(i * 2));
            retVal.AirTempHistory.AddPoint(random.Next(50, 60), startDate.AddHours(i * 2));
        }

        return retVal;
    }

    public RiversideDashboardModel GetRiversideData()
    {
        var retVal = new RiversideDashboardModel
        {
            BridgeName = "Riverside",
            PaApproachSurfaceTemperature = new()
            {
                TableName = "TP08A_TP08A_W",
                ColumnName = "TP08AST",
                TimeOfPoint = DateTime.Now,
                PointValue = 61.84f,
                DataName = "PaApproachSurfaceTemperature",
                BridgeName = "Riverside"
            },
            AmbientAirTemperature = new()
            {
                TableName = "TP08A_TP08A_D",
                ColumnName = "TP08APT_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 234.4f,
                DataName = "AmbientAirTemperature",
                BridgeName = "Riverside"
            },
            WindSpeed = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 5.602f,
                DataName = "WindSpeed",
                BridgeName = "Riverside"
            },
            WindDirection = new()
            {
                TableName = "BB05A_BB05A_W_arch1",
                ColumnName = "WS_Wind_Direction_Avg_Avg",
                TimeOfPoint = DateTime.Now,
                PointValue = 277.8f,
                DataName = "WindDirection",
                BridgeName = "Riverside"
            },
            AirTempHistory = new()
            {
                TableName = "BB04A_BB04A_D",
                ColumnName = "BB04APT_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Riverside",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            },
            WindSpeedHistory = new()
            {
                TableName = "BB05A_BB05A_W",
                ColumnName = "WS_Wind_Speed_Avg_Avg",
                DataName = "WindSpeedHistory",
                BridgeName = "Riverside",
                MaxNumberOfPoints = 20,
                MaxInterval = null
            }
        };

        var startDate = DateTime.Now.AddHours(-48);
        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 0, 0);
        var random = new Random();
        for (var i = 0; i < 20; i++)
        {
            retVal.WindSpeedHistory.AddPoint(random.Next(200, 250), startDate.AddHours(i * 2));
            retVal.AirTempHistory.AddPoint(random.Next(50, 60), startDate.AddHours(i * 2));
        }

        return retVal;
    }
}