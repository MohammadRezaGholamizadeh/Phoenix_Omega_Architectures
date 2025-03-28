using System.ComponentModel.DataAnnotations;

namespace InfrastructureLayer.Configurations.DataAccessConfigurations
{
    public class DataAccessConfig : IValidatableObject
    {
        public string DBProvider { get; set; } = string.Empty;
        public string ConnectionString =>
            UserId != string.Empty
            ? $"server = {Server}; database = {DataBaseName}; User Id = {UserId} ; PassWord = {PassWord};"
            : $"server = {Server}; database = {DataBaseName}; Trusted_Connection = {Trusted_Connection}";
        public string Server { get; set; } = ".";
        public string DataBaseName { get; set; } = string.Empty;
        public bool Trusted_Connection { get; set; } = true;
        public string? UserId { get; set; } = string.Empty;
        public string? PassWord { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DBProvider))
            {
                yield return new ValidationResult(
                    $"{nameof(DataAccessConfig)}" +
                    $".{nameof(DBProvider)} is not configured",
                    new[] { nameof(DBProvider) });
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                yield return new ValidationResult(
                    $"{nameof(DataAccessConfig)}" +
                    $".{nameof(ConnectionString)} is not configured",
                    new[] { nameof(ConnectionString) });
            }
        }
    }
}
