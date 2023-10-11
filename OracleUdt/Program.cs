using Oracle.ManagedDataAccess.Client;

namespace ConsoleApp
{
    class Program
    {
        private static async Task<SdoPoint> GetSdoPoint(OracleDataReader reader, int ordinal)
        {
            return await reader.IsDBNullAsync(ordinal, _cts.Token).ConfigureAwait(false)
                ? SdoPoint.Null
                : await reader.GetFieldValueAsync<SdoPoint>(ordinal, _cts.Token).ConfigureAwait(false);
        }

        private static readonly CancellationTokenSource _cts = new();
        private static string ConnectionString = string.Empty;

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Cancel event triggered");
                _cts.Cancel();
                eventArgs.Cancel = true;
            };

            if (args.Length < 1)
            {
                Console.WriteLine("Please give connection string as parameter");
                return;
            }
            ConnectionString = args[0];

            try
            {
                var dbConnection = new OracleConnection(ConnectionString)
                {
                    BindByName = true
                };
                using var cmd = dbConnection.CreateCommand();
                cmd.CommandText = "select SDO_POINT from odp_sample_sdo_geo_obj_tab";
                await dbConnection.OpenAsync().ConfigureAwait(false);
                using var reader = await cmd.ExecuteReaderAsync(_cts.Token).ConfigureAwait(false);
                while (await reader.ReadAsync(_cts.Token).ConfigureAwait(false))
                {
                    var geom = await GetSdoPoint(reader, 0).ConfigureAwait(false);
                    Console.WriteLine(geom.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}