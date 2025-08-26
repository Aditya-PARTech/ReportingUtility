namespace ReportingUtility.Data
{
    public interface ICalculator<TInput, TResult>
    {
        TResult Calculate(TInput input);
    }
}