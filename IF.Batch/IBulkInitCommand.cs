namespace IF.Batch
{
    public interface IBulkInitCommand
    {

        
        string BulkName { get; set; }

        int SplitBy { get; set; }

        bool Force { get; set; }
    }
}
