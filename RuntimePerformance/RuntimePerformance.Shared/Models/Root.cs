using System.Runtime.Serialization;

namespace RuntimePerformance.Shared.Models
{
    [DataContract]
    public class Root<T>
    {
        [DataMember(Order = 1)]
        public List<T> Items { get; set; } = new List<T>();
        [DataMember(Order = 2)]
        public int ItemCount { get; set; }
    }
}