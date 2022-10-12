namespace AppTresCamadas.Business.Models.Shared
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        protected EntityBase() => Id = Guid.NewGuid();
    }
}
