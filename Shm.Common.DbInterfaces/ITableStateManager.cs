using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface ITableStateManager
{
    /// <summary>
    /// Method to get table state, given its id
    /// </summary>
    /// <param name="tableName">Table Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An instance of <see cref="TableStateReturnModel"/></returns>
    Task<TableStateReturnModel?> GetAsync(string tableName, CancellationToken cancellationToken);

    /// <summary>
    /// Method to save the state for next iteration
    /// </summary>
    /// <param name="model">Parsed data model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SaveStateAsync(BatchDataModel model, CancellationToken cancellationToken);
}