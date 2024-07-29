namespace IndFusion.Exxerpro.Domain.Models;

public interface IMachineOee
{
    double Availability { get; }
    double Capacity { get; }
    double DefectiveRate { get; }
    string Name { get; }
    double Oee { get; }
    double Performance { get; }
    double ProducedPieces { get; }
    double Quality { get; }
    double RejectedPieces { get; }

    double RunningTime
    {
        get;
        // non-negative
    }

    double StoppingTime
    {
        get;
        // non-negative
    }

    void SetInitialCondition(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime);
    string ToString();
    void UpdateInfo(int producedPieces, int rejectedPieces, double runningTime, double stoppingTime);
}