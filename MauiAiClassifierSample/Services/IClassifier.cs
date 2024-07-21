using MauiAiClassifierSample.Models;

namespace MauiAiClassifierSample.Services
{
    public interface IClassifier
    {
        event EventHandler<ClassificationEventArgs> ClassificationCompleted;

        Task<List<Classification>> Classify(byte[] bytes);
    }

    public class ClassificationEventArgs : EventArgs
    {
        public List<Classification> Predictions { get; private set; }

        public ClassificationEventArgs(List<Classification> predictions)
        {
            Predictions = predictions;
        }
    }
}
