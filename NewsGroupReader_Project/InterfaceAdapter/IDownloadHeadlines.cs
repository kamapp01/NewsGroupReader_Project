namespace MVVM_ICOM_INOTIFY.InterfaceAdapter;

public interface IDownloadHeadlines
{
    List<string> DownloadNewsHeadlines(string headline);
}