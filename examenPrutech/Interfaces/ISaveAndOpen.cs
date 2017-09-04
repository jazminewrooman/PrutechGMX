using System.Threading.Tasks;

namespace GMX
{
    public interface ISaveAndOpen
    {
        Task OpenFile(string filename, byte[] bytes);

    }
}