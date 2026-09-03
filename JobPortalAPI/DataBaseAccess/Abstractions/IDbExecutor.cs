using System.Data;

namespace JobPortalAPI.DataBaseAccess.Abstractions
{
    public interface IDbExecutor
    {
        Task<T> ExecuteScalarAsync<T>(
            string sql,
            object? parameters = null);

        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? parameters = null);

        Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? parameters = null);

        Task<T> QuerySingleAsync<T>(
            string sql,
            object? parameters = null);
    }
}