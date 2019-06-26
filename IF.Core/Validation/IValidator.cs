namespace IF.Core.Validation
{
    public interface IValidator<T> 
    {
        void Validate(T command);
    }
}
