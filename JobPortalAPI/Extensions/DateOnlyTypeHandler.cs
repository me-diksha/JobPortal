using Dapper;
using System.Data;

namespace JobPortalAPI.Extensions
{
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }

        public override DateOnly Parse(object value)
        {
            return value switch
            {
                DateOnly d => d,                              // Npgsql returns DateOnly directly
                DateTime dt => DateOnly.FromDateTime(dt),     // fallback
                _ => DateOnly.Parse(value.ToString()!)        // last resort
            };
        }
    }
}
