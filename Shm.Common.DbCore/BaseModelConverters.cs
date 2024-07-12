using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore;

public static class BaseModelConverters
{
    /// <summary>
    /// Method to update database model with common properties
    /// </summary>
    /// <typeparam name="TDbModel">DB model type</typeparam>
    /// <param name="dbModel">Database model</param>
    /// <param name="requestContext">Request context</param>
    public static void AddCreateModelProperties<TDbModel>(this TDbModel dbModel, PerRequestContext requestContext)
        where TDbModel : DbEntityBase
    {
        dbModel.ItemCreatedById = requestContext.UserContext.UserId;
        dbModel.DateCreated = DateTimeOffset.UtcNow;
        dbModel.ItemLastModifiedById = requestContext.UserContext.UserId;
        dbModel.DateLastModified = DateTimeOffset.UtcNow;
        dbModel.IsDeleted = false;
    }

    /// <summary>
    /// Method to update database model with common properties
    /// </summary>
    /// <typeparam name="TDbModel">DB model type</typeparam>
    /// <param name="dbModel">Database model</param>
    /// <param name="requestContext">Request context</param>
    public static void AddUpdateModelProperties<TDbModel>(this TDbModel dbModel, PerRequestContext requestContext)
        where TDbModel : DbEntityBase
    {
        dbModel.ItemLastModifiedById = requestContext.UserContext.UserId;
        dbModel.DateLastModified = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Method to update return model with common properties
    /// </summary>
    /// <typeparam name="TReturnModel">Return model type</typeparam>
    /// <typeparam name="TDbModel">Database model type</typeparam>
    /// <param name="retVal">Return model</param>
    /// <param name="dbModel">Database model</param>
    public static void AddReturnModelProperties<TDbModel, TReturnModel>(this TReturnModel retVal, TDbModel dbModel)
        where TReturnModel : CommonReturnModel
        where TDbModel : DbEntityBase
    {
        retVal.ItemCreatedById = dbModel.ItemCreatedById;
        retVal.DateCreated = dbModel.DateCreated;
        retVal.ItemLastModifiedById = dbModel.ItemLastModifiedById;
        retVal.DateLastModified = dbModel.DateLastModified;
        retVal.IsDeleted = dbModel.IsDeleted;
    }
}