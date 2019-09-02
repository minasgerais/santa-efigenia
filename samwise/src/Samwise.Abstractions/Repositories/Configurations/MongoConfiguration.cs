using System;
using System.Text;

namespace Samwise.Abstractions.Repositories.Configurations
{
    public class MongoConfiguration<TRepository>
    {
        public MongoConfiguration(string mongoAdminDbN, string mongoUsername, string mongoPassword, string mongoConTcpIp, int mongoConnPort, string mongoDatabase)
        {
            MongoAdminDbN = mongoAdminDbN;
            MongoUsername = mongoUsername;
            MongoPassword = mongoPassword;
            MongoConTcpIp = mongoConTcpIp;
            MongoConnPort = mongoConnPort;
            MongoDatabase = mongoDatabase;

            Validar();
        }

        public string MongoAdminDbN { get; }
        public string MongoUsername { get; }
        public string MongoPassword { get; }
        public string MongoConTcpIp { get; }
        public int MongoConnPort { get; }
        public string MongoDatabase { get; }

        private void Validar()
        {
            StringBuilder erros = new StringBuilder();
            if (string.IsNullOrEmpty(MongoAdminDbN)) erros.AppendLine(nameof(MongoAdminDbN));
            if (string.IsNullOrEmpty(MongoUsername)) erros.AppendLine(nameof(MongoUsername));
            if (string.IsNullOrEmpty(MongoPassword)) erros.AppendLine(nameof(MongoPassword));
            if (string.IsNullOrEmpty(MongoConTcpIp)) erros.AppendLine(nameof(MongoConTcpIp));
            if (MongoConnPort == default) erros.AppendLine(nameof(MongoConnPort));
            if (string.IsNullOrEmpty(MongoDatabase)) erros.AppendLine(nameof(MongoDatabase));
            
            if(erros.Length != 0) throw new ArgumentException(erros.ToString());
        }
    }
}